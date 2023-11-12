using System;
using System.Collections;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public class CharacterInputHandler : PlayerInputHandler
    {
        public event Action<Vector2> OnPlayerMoved;
        public event Action OnPlayerInteract;

        private bool _moving;
        private Vector2 _movementDirection;

        public CharacterInputHandler(PlayerID playerID, MonoBehaviour owner) : base(playerID, owner)
        {
        }

        protected override void RegisterEvents()
        {
            switch (PlayerID)
            {
                case PlayerID.Player1:
                    Player1Input.Level.Movement.performed += OnMovement;
                    Player1Input.Level.Movement.canceled += OnMovementCanceled;
                    Player1Input.Level.BeginInteraction.performed += OnBeginInteraction;
                    break;
                case PlayerID.Player2:
                    Player2Input.Level.Movement.performed += OnMovement;
                    Player2Input.Level.Movement.canceled += OnMovementCanceled;
                    Player2Input.Level.BeginInteraction.performed += OnBeginInteraction;
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
                    Player1Input.Level.Movement.performed -= OnMovement;
                    Player1Input.Level.Movement.canceled -= OnMovementCanceled;
                    Player1Input.Level.BeginInteraction.performed -= OnBeginInteraction;
                    break;
                case PlayerID.Player2:
                    Player2Input.Level.Movement.performed -= OnMovement;
                    Player2Input.Level.Movement.canceled -= OnMovementCanceled;
                    Player2Input.Level.BeginInteraction.performed -= OnBeginInteraction;
                    break;
                default:
                    return;
            }
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            _movementDirection = context.ReadValue<Vector2>();
            _moving = true;
            StartCoroutine(MovementCoroutine());
        }

        private IEnumerator MovementCoroutine()
        {
            while (_moving)
            {
                OnPlayerMoved?.Invoke(_movementDirection);
                yield return null;
            }
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            _moving = false;
        }

        private void OnBeginInteraction(InputAction.CallbackContext context)
        {
            OnPlayerInteract?.Invoke();
        }
    }
}