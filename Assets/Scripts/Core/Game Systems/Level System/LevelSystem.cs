using Core.Game_Systems.Player_Input;
using Core.Game_Systems.Task_System;
using Core.Game_Systems.UI_System;
using Core.Managers;
using UnityEngine;

namespace Core.Game_Systems.Level_System
{
    public class LevelSystem : MonoBehaviour
    {
        public static LevelSystem Instance { get; private set; }
        public UISystem UISystem => uiSystem;
        public PlayerInputSystem InputSystem => inputSystem;
        public TaskSystem TaskSystem => taskSystem;

        [SerializeField] private UISystem uiSystem;
        [SerializeField] private PlayerInputSystem inputSystem;
        [SerializeField] private TaskSystem taskSystem;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            EventManager.AddListener<LevelCompletedEvent>(OnLevelCompleted);
        }

        private void Start()
        {
            uiSystem.HideAllPages();
            uiSystem.ShowPage(PageID.LevelHUD);
        }

        private void OnLevelCompleted(LevelCompletedEvent levelCompletedEvent)
        {
            inputSystem.DisableAllInput();
            uiSystem.HideAllPages();
            uiSystem.ShowPage(PageID.LevelComplete);
        }
    }

    public class LevelCompletedEvent : GameEvent
    {
        
    }
}