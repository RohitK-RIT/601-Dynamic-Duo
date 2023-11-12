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
        [SerializeField] private float ChainMovementDelay = 0.05f;
        private bool _player1NotMoving, _player2NotMoving;

        private Vector2Int Player1GridPos
        {
            get => _player1GridPos;

            set
            {
                SetGridHighlight(_player1GridPos, _player1GridPos.Equals(_player2GridPos) ? _player2Color : Color.clear);
                _player1GridPos = new Vector2Int(Mathf.Clamp(value.x, 0, 2), Mathf.Clamp(value.y, 0, 3));
                Debug.Log($"New clamped player 1 pos ({_player1GridPos.x},{_player1GridPos.y})");
                SetGridHighlight(_player1GridPos, Color.cyan);
            }
        }

        private Vector2Int Player2GridPos
        {
            get => _player2GridPos;

            set
            {
                SetGridHighlight(_player2GridPos, _player1GridPos.Equals(_player2GridPos) ? _player1Color : Color.clear, false);
                _player2GridPos = new Vector2Int(Mathf.Clamp(value.x, 0, 2), Mathf.Clamp(value.y, 0, 3));
                Debug.Log($"Player 2 new Pos ({_player2GridPos.x},{_player2GridPos.y})");
                SetGridHighlight(_player2GridPos, Color.magenta, false);
            }
        }

        private void SetGridHighlight(Vector2Int gridPos, Color color, bool isPlayer1 = true)
        {
            Panel panel;
            if (gridPos.y == 3)
            {
                var playerPanels = isPlayer1 ? _player1Panels : _player2Panels;
                panel = playerPanels[gridPos.x];
            }
            else
                panel = _gameGrid[gridPos.x, gridPos.y];

            panel.SetHighlightColor(color);
        }


        #region Initialization

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

        #endregion

        #region Update

        private void Update()
        {
            if (!Player1Input || !Player2Input)
            {
                Debug.LogError("Player Input not found");
                Destroy(gameObject);
                return;
            }

            try
            {
                ProcessPlayer1Input();
            }
            catch (Exception e)
            {
                Debug.LogError($"Player 1 error: {e}");
            }

            try
            {
                ProcessPlayer2Input();
            }
            catch (Exception e)
            {
                Debug.LogError($"Player 2 error: {e}");
            }

            if (!CheckAnswer())
                return;

            IsCompleted = true;
            Destroy(gameObject);
        }

        private void ProcessPlayer1Input()
        {
            // Check if player can do movement
            var playerMovement = Vector2Int.CeilToInt(Player1Input.GetMovementDirection());
            if ((DateTime.Now - _player1MoveTime).Seconds >= ChainMovementDelay || _player1NotMoving)
            {
                if (playerMovement.sqrMagnitude > 0)
                {
                    _player1MoveTime = DateTime.Now;
                    var newPlayer1GridPos = Player1GridPos + playerMovement * (Vector2Int.down + Vector2Int.right);
                    Debug.Log($"New unclamped player 1 pos {newPlayer1GridPos}");
                    Player1GridPos = newPlayer1GridPos;
                }
            }

            _player1NotMoving = playerMovement == Vector2Int.zero;

            var currentPanel = _player1GridPos.y == 3 ? _player1Panels[_player1GridPos.x] : _gameGrid[_player1GridPos.x, _player1GridPos.y];

            if (Player1Input.IsInteractionButtonPressed())
            {
                if (_player1SelectedPanel != null)
                {
                    if (_player1SelectedPanel != currentPanel)
                    {
                        // Shift the color of the selected panel
                        int index;
                        if (_player1SelectedPanel[1].color != Color.clear)
                            index = 1;
                        else if (_player1SelectedPanel[0].color != Color.clear)
                            index = 0;
                        else
                            return;

                        currentPanel[index].color = _player1SelectedPanel[index].color;
                        _player1SelectedPanel[index].color = Color.clear;
                    }

                    _player1SelectedPanel.SetHighlightColor(Color.clear);
                    _player1SelectedPanel.Transform.localScale = Vector3.one;
                    _player1SelectedPanel = null;
                }
                else
                {
                    // Select the box
                    _player1SelectedPanel = currentPanel;
                    _player1SelectedPanel.Transform.localScale *= 1.1f;
                }
            }
            else if (_player1SelectedPanel != null && _player1SelectedPanel != currentPanel)
            {
                _player1SelectedPanel.SetHighlightColor((_player1Color + Color.black) / 2);
            }
        }

        private void ProcessPlayer2Input()
        {
            var playerMovement = Vector2Int.CeilToInt(Player2Input.GetMovementDirection());
            if ((DateTime.Now - _player2MoveTime).Seconds >= ChainMovementDelay || _player2NotMoving)
            {
                if (playerMovement.sqrMagnitude > 0)
                {
                    _player2MoveTime = DateTime.Now;
                    Player2GridPos += playerMovement * (Vector2Int.down + Vector2Int.right);
                }
            }

            _player2NotMoving = playerMovement == Vector2Int.zero;

            if (Player2Input.IsInteractionButtonPressed())
            {
                var currentPanel = _player2GridPos.y == 3 ? _player2Panels[_player2GridPos.x] : _gameGrid[_player2GridPos.x, _player2GridPos.y];
                if (_player2SelectedPanel != null)
                {
                    if (_player2SelectedPanel != currentPanel)
                    {
                        // Shift the color of the selected panel
                        int index;
                        if (_player2SelectedPanel[1].color != Color.clear)
                            index = 1;
                        else if (_player2SelectedPanel[0].color != Color.clear)
                            index = 0;
                        else
                            return;

                        currentPanel[index].color = _player2SelectedPanel[index].color;
                        _player2SelectedPanel[index].color = Color.clear;
                    }

                    _player2SelectedPanel.SetHighlightColor(Color.clear);
                    _player2SelectedPanel.Transform.localScale = Vector3.one;
                    _player2SelectedPanel = null;
                }
                else
                {
                    // Select the box
                    _player2SelectedPanel = currentPanel;
                    _player2SelectedPanel.Transform.localScale *= 1.1f;
                }
            }
            else if (_player2SelectedPanel != null && _player2SelectedPanel.index != _player2GridPos)
            {
                _player2SelectedPanel.SetHighlightColor(_player2Color + Color.black);
            }
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

        #endregion
    }

    public class Panel
    {
        public readonly Vector2Int index;

        private readonly Image _highlight;
        private readonly Image _smallPanelImage;
        private readonly Image _bigPanelImage;

        public GameObject GameObject { get; }
        public Transform Transform { get; }

        public bool IsEmpty => _smallPanelImage.color == Color.clear && _bigPanelImage.color == Color.clear;

        public Panel(Vector2Int index, Transform panel)
        {
            this.index = index;

            Transform = panel;
            GameObject = panel.gameObject;

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