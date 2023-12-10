using System;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public class CharacterHUDInputListener : PlayerInputListener
    {
        public event Action OnBackPressed;
        public CharacterHUDInputListener(PlayerID playerID, ActionMap actionMap, MonoBehaviour owner) : base(playerID, actionMap, owner)
        {
        }

        protected override void RegisterEvents()
        {
            Map["End Interaction"].performed += BackPressed;
        }

        protected override void UnregisterEvents()
        {
            Map["End Interaction"].performed -= BackPressed;
        }

        private void BackPressed(InputAction.CallbackContext obj)
        {
            OnBackPressed?.Invoke();
        }
    }
}