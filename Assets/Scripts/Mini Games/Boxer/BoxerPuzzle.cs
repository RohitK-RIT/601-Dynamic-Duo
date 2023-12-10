using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mini_Games.Boxer
{
    public class BoxerPuzzle : MiniGame
    {
        [SerializeField] private Transform answerPanel, gamePanel;
        [SerializeField] private Transform player1Pool, player2Pool;
        [SerializeField] private Color player1Color = Color.cyan, player1SelectedColor = Color.cyan, player2Color = Color.magenta, player2SelectedColor = Color.magenta;

        private BoxerInputListener _p1InputListener, _p2InputListener;
        private readonly Color[] _randomColors = { Color.red, Color.blue, Color.green, Color.yellow };

        private Panel[,] _answerGrid, _gameGrid;
        private Panel[] _p1Panels, _p2Panels;
        private Vector2Int _p1GridPos, _p2GridPos;
        private Vector3Int _p1SelectedPanelPos, _p2SelectedPanelPos;
        private Panel _p1SelectedPanel, _p2SelectedPanel;

        private readonly Dictionary<Vector3Int, Color> _answerDictionary = new()
        {
            { new Vector3Int(0, 0, 0), Color.yellow },
            { new Vector3Int(1, 0, 0), Color.yellow },
            { new Vector3Int(2, 0, 1), Color.blue },
            { new Vector3Int(0, 1, 1), Color.green },
            { new Vector3Int(1, 2, 1), Color.yellow },
            { new Vector3Int(2, 2, 0), Color.yellow }
        };

        private DateTime _p1MoveTime, _p2MoveTime;
        private bool _p1NotMoving, _p2NotMoving;

        private const float ChainMovementDelay = 0.1f;

        private Panel P1CurrentPanel => GetPanel(_p1GridPos);
        private Panel P2CurrentPanel => GetPanel(_p1GridPos);

        private Vector2Int P1GridPos
        {
            get => _p1GridPos;

            set
            {
                SetGridHighlight(P1GridPos, Color.clear);
                _p1GridPos = new Vector2Int(Mathf.Clamp(value.x, 0, 3), Mathf.Clamp(value.y, 0, 2));
            }
        }

        private Vector2Int P2GridPos
        {
            get => _p2GridPos;

            set
            {
                SetGridHighlight(_p2GridPos, Color.clear);
                _p2GridPos = new Vector2Int(Mathf.Clamp(value.x, 1, 4), Mathf.Clamp(value.y, 0, 2));
            }
        }

        private Panel GetPanel(Vector2Int gridPos)
        {
            var panel = gridPos.x switch
            {
                0 => _p1Panels[gridPos.y],
                4 => _p2Panels[gridPos.y],
                _ => _gameGrid[gridPos.x - 1, gridPos.y]
            };

            return panel;
        }

        private void SetGridHighlight(Vector2Int gridPos, Color color)
        {
            var panel = GetPanel(gridPos);
            panel.SetHighlightColor(color);
        }


        #region Initialization

        protected override void OnEnable()
        {
            base.OnEnable();

            // _answerGrid = GetGrid(answerPanel);
            _gameGrid = GetGrid(gamePanel);

            StartGame();
            EnableInput();
        }

        protected override void OnDisable()
        {
            DisableInput();
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            DisableInput();
            base.OnDestroy();
        }

        private void EnableInput()
        {
            _p1InputListener ??= new BoxerInputListener(PlayerID.Player1, actionMap, this);
            _p2InputListener ??= new BoxerInputListener(PlayerID.Player2, actionMap, this);

            _p1InputListener.TryEnable();
            _p2InputListener.TryEnable();

            _p1InputListener.OnPlayerNavigate += ProcessP1NavInput;
            _p2InputListener.OnPlayerNavigate += ProcessP2NavInput;

            _p1InputListener.OnPlayerInteract += ProcessP1InteractionInput;
            _p2InputListener.OnPlayerInteract += ProcessP2InteractionInput;

            _p1InputListener.OnBackPressed += CloseGame;
            _p2InputListener.OnBackPressed += CloseGame;
        }

        private void DisableInput()
        {
            _p1InputListener.Disable();
            _p2InputListener.Disable();

            _p1InputListener.OnPlayerNavigate -= ProcessP1NavInput;
            _p2InputListener.OnPlayerNavigate -= ProcessP2NavInput;

            _p1InputListener.OnPlayerInteract -= ProcessP1InteractionInput;
            _p2InputListener.OnPlayerInteract -= ProcessP2InteractionInput;

            _p1InputListener.OnBackPressed -= CloseGame;
            _p2InputListener.OnBackPressed -= CloseGame;
        }

        private Panel[,] GetGrid(Transform panelTransform)
        {
            var gridData = new Panel[3, 3];

            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                gridData[j, i] = new Panel(panelTransform.GetChild(i * 3 + j));

            return gridData;
        }

        private void StartGame()
        {
            // ShowAnswer();
            InitPlayerPool();
            P1GridPos = new Vector2Int(Random.Range(1, 4), Random.Range(0, 3));
            P2GridPos = new Vector2Int(Random.Range(1, 4), Random.Range(0, 3));
        }

        private void CreateAnswer()
        {
            // Uncomment this
            // _answerDictionary = new Dictionary<Vector3Int, Color>();
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
                Debug.Log($"{randomPoint}\t{randomColor.ToString()}");
            }
        }

        private void ShowAnswer()
        {
            foreach (var pair in _answerDictionary)
            {
                _answerGrid[pair.Key.x, pair.Key.y][pair.Key.z].color = pair.Value;
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
            _p1Panels = InitPlayerPool(player1Pool);
            _p2Panels = InitPlayerPool(player2Pool, false);
        }

        private Panel[] InitPlayerPool(Transform playerPool, bool isPlayer1 = true)
        {
            var playerPanels = new Panel[3];

            for (var i = 0; i < 3; i++)
            {
                playerPanels[i] = new Panel(playerPool.GetChild(i + 1));
                var answer = _answerDictionary.ElementAt(i + (isPlayer1 ? 0 : 3));

                playerPanels[i][answer.Key.z].color = answer.Value;
            }

            return playerPanels;
        }

        #endregion

        #region Update

        private void Update()
        {
            if (_p1SelectedPanel != null && P1CurrentPanel != _p1SelectedPanel)
                _p1SelectedPanel.SetHighlightColor(player1SelectedColor);

            if (_p2SelectedPanel != null && P2CurrentPanel != _p2SelectedPanel)
                _p2SelectedPanel.SetHighlightColor(player2SelectedColor);

            GetPanel(P1GridPos).SetHighlightColor(player1Color);
            GetPanel(P2GridPos).SetHighlightColor(player2Color);

            if (!IsAnswerAchieved) return;

            IsCompleted = true;
            CloseGame();
            Destroy(gameObject);
        }


        private void ProcessP1NavInput(Vector2Int playerNavInput)
        {
            // Check if player can do movement
            if ((DateTime.Now - _p1MoveTime).Seconds >= ChainMovementDelay || _p1NotMoving)
            {
                if (playerNavInput.sqrMagnitude > 0)
                {
                    _p1MoveTime = DateTime.Now;
                    P1GridPos += playerNavInput * (Vector2Int.right + Vector2Int.down);
                }
            }

            _p1NotMoving = playerNavInput == Vector2Int.zero;
        }

        private void ProcessP1InteractionInput()
        {
            var currentPanel = GetPanel(P1GridPos);
            if (_p1SelectedPanel != null)
            {
                if (_p1SelectedPanel != currentPanel)
                {
                    // Shift the color of the selected panel
                    int index;
                    if (_p1SelectedPanel[1].color != Color.clear)
                        index = 1;
                    else if (_p1SelectedPanel[0].color != Color.clear)
                        index = 0;
                    else
                        return;

                    (currentPanel[index].color, _p1SelectedPanel[index].color) = (_p1SelectedPanel[index].color, currentPanel[index].color);
                    _p1SelectedPanel.SetHighlightColor(Color.clear);
                }

                _p1SelectedPanel.Transform.localScale = Vector3.one;
                _p1SelectedPanel = null;
            }
            else if (!currentPanel.IsEmpty)
            {
                // Select the box
                _p1SelectedPanel = currentPanel;
                _p1SelectedPanel.Transform.localScale *= 1.1f;
            }
        }

        private void ProcessP2NavInput(Vector2Int playerNavInput)
        {
            if ((DateTime.Now - _p2MoveTime).Seconds >= ChainMovementDelay || _p2NotMoving)
            {
                if (playerNavInput.sqrMagnitude > 0)
                {
                    _p2MoveTime = DateTime.Now;
                    P2GridPos += playerNavInput * (Vector2Int.right + Vector2Int.down);
                }
            }

            _p2NotMoving = playerNavInput == Vector2Int.zero;
        }

        private void ProcessP2InteractionInput()
        {
            var currentPanel = GetPanel(P2GridPos);
            if (_p2SelectedPanel != null)
            {
                if (_p2SelectedPanel != currentPanel)
                {
                    // Shift the color of the selected panel
                    int index;
                    if (_p2SelectedPanel[1].color != Color.clear)
                        index = 1;
                    else if (_p2SelectedPanel[0].color != Color.clear)
                        index = 0;
                    else
                        return;

                    currentPanel[index].color = _p2SelectedPanel[index].color;
                    _p2SelectedPanel[index].color = Color.clear;
                    _p2SelectedPanel.SetHighlightColor(Color.clear);
                }

                _p2SelectedPanel.Transform.localScale = Vector3.one;
                _p2SelectedPanel = null;
            }
            else if (!currentPanel.IsEmpty)
            {
                // Select the box
                _p2SelectedPanel = currentPanel;
                _p2SelectedPanel.Transform.localScale *= 1.1f;
            }
        }

        private bool IsAnswerAchieved =>
            !(from answer in _answerDictionary let point = answer.Key where _gameGrid[point.x, point.y][point.z].color != answer.Value select answer).Any();

        #endregion
    }
}