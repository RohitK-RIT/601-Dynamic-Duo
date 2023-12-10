using System;
using System.Collections;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mini_Games.Boxer
{
    public class BoxerInputListener : MiniGameInputListener
    {
        public event Action<Vector2Int> OnPlayerNavigate;
        public event Action OnPlayerInteract;

        private Vector2Int _navigationDirection;
        private bool _navigating;

        public BoxerInputListener(PlayerID playerID, ActionMap actionMap, MonoBehaviour owner) : base(playerID, actionMap, owner)
        {
        }

        protected override void RegisterEvents()
        {
            base.RegisterEvents();
            Map["Navigation"].performed += PlayerNavigate;
            Map["Navigation"].canceled += PlayerNavigateCanceled;
            Map["Interact"].performed += BeginInteraction;
        }

        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Map["Navigation"].performed -= PlayerNavigate;
            Map["Navigation"].canceled -= PlayerNavigateCanceled;
            Map["Interact"].performed -= BeginInteraction;
        }

        private void PlayerNavigate(InputAction.CallbackContext context)
        {
            _navigationDirection = Vector2Int.CeilToInt(context.ReadValue<Vector2>());
            _navigating = true;
            StartCoroutine(MovementCoroutine());
        }

        private IEnumerator MovementCoroutine()
        {
            while (_navigating)
            {
                OnPlayerNavigate?.Invoke(_navigationDirection);
                yield return null;
            }
        }

        private void PlayerNavigateCanceled(InputAction.CallbackContext context)
        {
            _navigating = false;
        }

        private void BeginInteraction(InputAction.CallbackContext context)
        {
            OnPlayerInteract?.Invoke();
        }
    }
}