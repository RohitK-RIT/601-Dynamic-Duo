using System;
using UnityEngine;

namespace Mini_Games
{
    public abstract class MiniGame : MonoBehaviour
    {
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