using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Mini_Games.Boxer
{
    public class Boxer : MiniGame
    {
        [SerializeField] private Transform answerPanel, gamePanel;
        [SerializeField] private Transform player1Pool, player2Pool;

        private readonly Color[] _randomColors = { Color.red, Color.blue, Color.green, Color.yellow };
        private readonly Color _player1Color = Color.cyan, _player2Color = Color.magenta;

        private Panel[,] _answerGrid, _gameGrid;
        private Panel[] _player1Panels, _player2Panels;
        private Vector2Int _player1GridPos, _player2GridPos;
        private Vector3Int _player1SelectedPanelPos, _player2SelectedPanelPos;
        private Dictionary<Vector3Int, Color> _answerDictionary;
        private Panel _player1SelectedPanel, _player2SelectedPanel;

        private DateTime _player1MoveTime, _player2MoveTime;
        private const float ChainMovementDelay = 0.25f;

        private Vector2Int Player1GridPos
        {
            get => _player1GridPos;

            set
            {
                SetGridHighlight(_player1GridPos, _player1GridPos.Equals(_player2GridPos) ? _player2Color : Color.clear);
                _player1GridPos = new Vector2Int(Mathf.Clamp(value.x, 0, 3), Mathf.Clamp(value.y, 0, 4));
                SetGridHighlight(_player1GridPos, Color.cyan);
            }
        }

        private Vector2Int Player2GridPos
        {
            get => _player2GridPos;

            set
            {
                SetGridHighlight(_player2GridPos, _player1GridPos.Equals(_player2GridPos) ? _player1Color : Color.clear, false);
                _player2GridPos = new Vector2Int(Mathf.Clamp(value.x, 0, 3), Mathf.Clamp(value.y, 0, 4));
                SetGridHighlight(_player2GridPos, Color.magenta, false);
            }
        }

        private void SetGridHighlight(Vector2Int gridPos, Color color, bool isPlayer1 = true)
        {
            var panel = gridPos.y == 3 ? (isPlayer1 ? _player1Panels : _player2Panels)[gridPos.x] : _gameGrid[gridPos.x, gridPos.y];
            panel.SetHighlightColor(color);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _answerGrid = GetGrid(answerPanel);
            _gameGrid = GetGrid(gamePanel);

            StartGame();
        }

        private Panel[,] GetGrid(Transform panelTransform)
        {
            var gridData = new Panel[3, 3];

            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                gridData[j, i] = new Panel(new Vector2Int(j, i), panelTransform.GetChild(i * 3 + j));

            return gridData;
        }

        private void StartGame()
        {
            CreateAnswer();
            InitPlayerPool();
            Player1GridPos = Vector2Int.zero;
            Player2GridPos = Vector2Int.zero;
        }

        private void CreateAnswer()
        {
            _answerDictionary = new Dictionary<Vector3Int, Color>();
            for (var i = 0; i < 6;)
            {
                // Get a random point in the grid.
                var randomPoint = new Vector3Int(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 2));
                if (_answerDictionary.ContainsKey(randomPoint))
                    continue;

                i++;

                var checkingPoint = randomPoint;
                checkingPoint.z = checkingPoint.z == 0 ? 1 : 0;

                //Assign random color to them.
                var colorToExclude = Color.clear;
                if (_answerDictionary.ContainsKey(checkingPoint))
                    colorToExclude = _answerGrid[checkingPoint.x, checkingPoint.y][checkingPoint.z].color;

                var randomColor = GetRandomColor(colorToExclude);

                _answerDictionary.Add(randomPoint, randomColor);
                // Display the pattern.
                _answerGrid[randomPoint.x, randomPoint.y][randomPoint.z].color = randomColor;
            }
        }

        private Color GetRandomColor(Color excludedColor)
        {
            var selectedColor = _randomColors[Random.Range(0, _randomColors.Length)];
            while (excludedColor == selectedColor)
            {
                selectedColor = _randomColors[Random.Range(0, _randomColors.Length)];
            }

            return selectedColor;
        }

        private void InitPlayerPool()
        {
            _player1Panels = InitPlayerPool(player1Pool);
            _player2Panels = InitPlayerPool(player2Pool, false);
        }

        private Panel[] InitPlayerPool(Transform playerPool, bool isPlayer1 = true)
        {
            var playerPanels = new Panel[3];

            for (var i = 0; i < 3; i++)
            {
                playerPanels[i] = new Panel(new Vector2Int(i, 4), playerPool.GetChild(i + 1));
                var answer = _answerDictionary.ElementAt(i + (isPlayer1 ? 0 : 3));

                playerPanels[i][answer.Key.z].color = answer.Value;
            }

            return playerPanels;
        }

        private void Update()
        {
            if (player1Input && player2Input)
                ProcessPlayerInput();

            if (!CheckAnswer())
                return;

            isCompleted = true;
            Destroy(gameObject);
        }

        private bool CheckAnswer()
        {
            foreach (var answer in _answerDictionary)
            {
                var point = answer.Key;
                if (_gameGrid[point.x, point.y][point.z].color != answer.Value)
                    return false;
            }

            return true;
        }

        private void ProcessPlayerInput()
        {
            if ((DateTime.Now - _player1MoveTime).Seconds >= ChainMovementDelay)
            {
                var playerMovement = Vector2Int.CeilToInt(player1Input.GetMovementDirection());
                if (playerMovement.sqrMagnitude > 0)
                {
                    _player1MoveTime = DateTime.Now;
                    Player1GridPos += playerMovement * (Vector2Int.down + Vector2Int.right);
                }
            }

            if (player1Input.IsInteractionButtonPressed())
            {
                var currentPanel = _player1GridPos.y == 3 ? _player1Panels[_player1GridPos.x] : _gameGrid[_player1GridPos.x, _player1GridPos.y];
                if (_player1SelectedPanel != null && _player1SelectedPanel.index != _player1GridPos)
                {
                    // Shift the color of the selected panel
                    if (_player1SelectedPanel[1].color != Color.clear)
                    {
                        currentPanel[1].color = _player1SelectedPanel[1].color;
                        _player1SelectedPanel[1].color = Color.clear;
                        _player1SelectedPanel.SetHighlightColor(Color.clear);
                        _player1SelectedPanel = null;
                    }
                    else if (_player1SelectedPanel[0].color != Color.clear)
                    {
                        currentPanel[0].color = _player1SelectedPanel[0].color;
                        _player1SelectedPanel[0].color = Color.clear;
                        _player1SelectedPanel.SetHighlightColor(Color.clear);
                        _player1SelectedPanel = null;
                    }
                }
                else
                {
                    // Select the box
                    _player1SelectedPanel = currentPanel;
                }
            }
            else if (_player1SelectedPanel != null && _player1SelectedPanel.index != _player1GridPos)
            {
                _player1SelectedPanel.SetHighlightColor(_player1Color + Color.black);
            }

            if ((DateTime.Now - _player2MoveTime).Seconds >= ChainMovementDelay)
            {
                var playerMovement = Vector2Int.CeilToInt(player2Input.GetMovementDirection());
                if (playerMovement.sqrMagnitude > 0)
                {
                    _player2MoveTime = DateTime.Now;
                    Player2GridPos += playerMovement * (Vector2Int.down + Vector2Int.right);
                }
            }


            if (player2Input.IsInteractionButtonPressed())
            {
                var currentPanel = _player2GridPos.y == 3 ? _player2Panels[_player2GridPos.x] : _gameGrid[_player2GridPos.x, _player2GridPos.y];
                if (_player2SelectedPanel != null && _player2SelectedPanel.index != _player2GridPos)
                {
                    // Shift the color of the selected panel
                    if (_player2SelectedPanel[1].color != Color.clear)
                    {
                        currentPanel[1].color = _player2SelectedPanel[1].color;
                        _player2SelectedPanel[1].color = Color.clear;
                        _player2SelectedPanel.SetHighlightColor(Color.clear);
                        _player2SelectedPanel = null;
                    }
                    else if (_player2SelectedPanel[0].color != Color.clear)
                    {
                        currentPanel[0].color = _player2SelectedPanel[0].color;
                        _player2SelectedPanel[0].color = Color.clear;
                        _player2SelectedPanel.SetHighlightColor(Color.clear);
                        _player2SelectedPanel = null;
                    }
                }
                else
                {
                    // Select the box
                    _player2SelectedPanel = currentPanel;
                }
            }
            else if (_player2SelectedPanel != null && _player2SelectedPanel.index != _player2GridPos)
            {
                _player2SelectedPanel.SetHighlightColor(_player2Color + Color.black);
            }
        }
    }

    public class Panel
    {
        public readonly Vector2Int index;

        private readonly Image _highlight;
        private readonly Image _smallPanelImage;
        private readonly Image _bigPanelImage;

        public Panel(Vector2Int index, Transform panel)
        {
            this.index = index;

            _highlight = panel.GetChild(0).GetComponent<Image>();
            _bigPanelImage = panel.GetChild(2).GetComponent<Image>();
            _smallPanelImage = panel.GetChild(3).GetComponent<Image>();

            SetHighlightColor(Color.clear);
        }

        public void SetHighlightColor(Color color)
        {
            _highlight.color = color;
        }

        public void SetColor(int panelIndex, Color color)
        {
            this[panelIndex].color = color;
        }

        public Image this[int i] => i == 0 ? _bigPanelImage : _smallPanelImage;
    }
}