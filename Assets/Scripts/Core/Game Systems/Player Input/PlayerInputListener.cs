using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Game_Systems.Player_Input
{
    public abstract class PlayerInputListener
    {
        public PlayerID PlayerID { get; }
        public ActionMap ActionMap { get; }
        private MonoBehaviour Component { get; }
        public GameObject GameObject { get; }

        protected InputActionMap Map;

        private bool _active;
        public bool IsValid => Map != null;

        public bool Active
        {
            get => IsValid && _active && Map.enabled;
            private set
            {
                _active = value;
                if (_active)
                    RegisterEvents();
                else
                    UnregisterEvents();
            }
        }

        protected PlayerInputListener(PlayerID playerID, ActionMap actionMap, MonoBehaviour owner)
        {
            Component = owner;
            GameObject = owner.gameObject;
            
            PlayerID = playerID;
            ActionMap = actionMap;
            
            new PlayerInputHandlerRegistrationEvent(this, playerID, actionMap, map => { Map = map; }).Raise();
        }

        public void TryEnable()
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