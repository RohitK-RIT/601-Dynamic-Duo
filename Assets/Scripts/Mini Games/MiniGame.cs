using System;
using Core.Game_Systems.Player_Input;
using UnityEngine;

namespace Mini_Games
{
    public abstract class MiniGame : MonoBehaviour
    {
        [SerializeField] protected ActionMap actionMap;
        public event Action<bool> OnClosed;
        protected bool IsCompleted;

        public static bool IsOpen { get; private set; }

        protected virtual void OnEnable()
        {
            IsCompleted = false;
            IsOpen = true;
        }

        protected virtual void OnDisable()
        {
            OnReset();
        }

        protected void CloseGame()
        {
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            OnReset();
        }

        private void OnReset()
        {
            OnClosed?.Invoke(IsCompleted);
            IsOpen = false;
        }
    }
}