using System;
using System.Collections;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public class CharacterInputListener : PlayerInputListener
    {
        public event Action<Vector2> OnPlayerMoved;
        public event Action OnPlayerInteract;

        private bool _moving;
        private Vector2 _movementDirection;

        public CharacterInputListener(PlayerID playerID, ActionMap actionMap, MonoBehaviour owner) : base(playerID,actionMap, owner)
        {
        }

        protected override void RegisterEvents()
        {
            Map["Movement"].performed += OnMovement;
            Map["Movement"].canceled += OnMovementCanceled;
            Map["Begin Interaction"].performed += OnBeginInteraction;
        }

        protected override void UnregisterEvents()
        {
            Map["Movement"].performed -= OnMovement;
            Map["Movement"].canceled -= OnMovementCanceled;
            Map["Begin Interaction"].performed -= OnBeginInteraction;
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