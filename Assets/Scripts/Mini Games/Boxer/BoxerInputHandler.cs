using System;
using System.Collections;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mini_Games.Boxer
{
    public class BoxerInputHandler : PlayerInputHandler
    {
        public event Action<Vector2Int> OnPlayerNavigate;
        public event Action OnPlayerInteract;

        private Vector2Int _navigationDirection;
        private bool _navigating;

        public BoxerInputHandler(PlayerID playerID, MonoBehaviour owner) : base(playerID, owner)
        {
        }

        protected override void RegisterEvents()
        {
            switch (PlayerID)
            {
                case PlayerID.Player1:
                    Player1Input.Boxer.Navigation.performed += PlayerNavigate;
                    Player1Input.Boxer.Navigation.canceled += PlayerNavigateCanceled;
                    Player1Input.Boxer.Interact.performed += BeginInteraction;
                    break;
                case PlayerID.Player2:
                    Player2Input.Boxer.Navigation.performed += PlayerNavigate;
                    Player2Input.Boxer.Navigation.canceled += PlayerNavigateCanceled;
                    Player2Input.Boxer.Interact.performed += BeginInteraction;
                    break;
                default:
                    return;
            }
        }

        protected override void UnregisterEvents()
        {
            switch (PlayerID)
            {
                case PlayerID.Player1:
                    Player1Input.Boxer.Navigation.performed -= PlayerNavigate;
                    Player1Input.Boxer.Navigation.canceled -= PlayerNavigateCanceled;
                    Player1Input.Boxer.Interact.performed -= BeginInteraction;
                    break;
                case PlayerID.Player2:
                    Player2Input.Boxer.Navigation.performed -= PlayerNavigate;
                    Player2Input.Boxer.Navigation.canceled -= PlayerNavigateCanceled;
                    Player2Input.Boxer.Interact.performed -= BeginInteraction;
                    break;
                default:
                    return;
            }
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