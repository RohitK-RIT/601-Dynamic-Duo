using System;
using System.Collections;
using UnityEngine;

namespace Core.Game_Systems.Player_Input
{
    public abstract class PlayerInputHandler
    {
        internal bool IsActive;
        public PlayerID PlayerID { get; }
        protected readonly Player1Input Player1Input;
        protected readonly Player2Input Player2Input;

        private bool _active;

        protected MonoBehaviour Component { get; }
        protected GameObject GameObject { get; }
        protected Transform Transform { get; }

        public bool Active
        {
            get => _active;
            private set
            {
                _active = value;
                if (_active)
                    RegisterEvents();
                else
                    UnregisterEvents();
            }
        }

        protected PlayerInputHandler(PlayerID playerID, MonoBehaviour owner)
        {
            PlayerID = playerID;

            switch (PlayerID)
            {
                case PlayerID.Player1:
                    Player1Input = new Player1Input();
                    Player1Input.Enable();
                    break;
                case PlayerID.Player2:
                    Player2Input = new Player2Input();
                    Player2Input.Enable();
                    break;
                default:
                    return;
            }

            Component = owner;
            GameObject = owner.gameObject;
            Transform = owner.transform;
            
            new PlayerInputHandlerRegistrationEvent(this).Raise();
        }

        public void Enable()
        {
            new PlayerInputAccessRequest(this, successful => Active = successful).Raise();
        }

        public void Disable()
        {
            Active = false;
        }

        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();

        protected Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return Component.StartCoroutine(enumerator);
        }

        protected void StopCoroutine(Coroutine coroutine)
        {
            Component.StopCoroutine(coroutine);
        }
    }
}