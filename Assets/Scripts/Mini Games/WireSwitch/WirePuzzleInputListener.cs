using System;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mini_Games.WireSwitch
{
    public class WirePuzzleInputListener : MiniGameInputListener
    {
        public bool UpPressed { get; private set; }

        public string UpKey => Map["Up"].GetBindingDisplayString();

        public bool DownPressed { get; private set; }

        public string DownKey => Map["Down"].GetBindingDisplayString();

        public bool LeftPressed { get; private set; }

        public string LeftKey => Map["Left"].GetBindingDisplayString();

        public bool RightPressed { get; private set; }

        public string RightKey => Map["Right"].GetBindingDisplayString();

        public event Action OnInteractPressed;

        public string InteractKey => Map["Interact"].GetBindingDisplayString();

        public WirePuzzleInputListener(PlayerID playerID, ActionMap actionMap, MonoBehaviour owner) : base(playerID, actionMap, owner)
        {
        }

        protected override void RegisterEvents()
        {
            base.RegisterEvents();
            Map["Up"].performed += UpButtonPressed;
            Map["Up"].canceled += UpButtonReleased;
            Map["Down"].performed += DownButtonPressed;
            Map["Down"].canceled += DownButtonReleased;
            Map["Left"].performed += LeftButtonPressed;
            Map["Left"].canceled += LeftButtonReleased;
            Map["Right"].performed += RightButtonPressed;
            Map["Right"].canceled += RightButtonReleased;
            Map["Interact"].performed += InteractButtonPressed;
        }

        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Map["Up"].performed -= UpButtonPressed;
            Map["Up"].canceled -= UpButtonReleased;
            Map["Down"].performed -= DownButtonPressed;
            Map["Down"].canceled -= DownButtonReleased;
            Map["Left"].performed -= LeftButtonPressed;
            Map["Left"].canceled -= LeftButtonReleased;
            Map["Right"].performed -= RightButtonPressed;
            Map["Right"].canceled -= RightButtonReleased;
            Map["Interact"].performed -= InteractButtonPressed;
        }

        private void UpButtonPressed(InputAction.CallbackContext callbackContext)
        {
            UpPressed = true;
        }

        private void UpButtonReleased(InputAction.CallbackContext callbackContext)
        {
            UpPressed = false;
        }

        private void DownButtonPressed(InputAction.CallbackContext callbackContext)
        {
            DownPressed = true;
        }

        private void DownButtonReleased(InputAction.CallbackContext callbackContext)
        {
            DownPressed = false;
        }

        private void LeftButtonPressed(InputAction.CallbackContext callbackContext)
        {
            LeftPressed = true;
        }

        private void LeftButtonReleased(InputAction.CallbackContext callbackContext)
        {
            LeftPressed = false;
        }

        private void RightButtonPressed(InputAction.CallbackContext callbackContext)
        {
            RightPressed = true;
        }

        private void RightButtonReleased(InputAction.CallbackContext callbackContext)
        {
            RightPressed = false;
        }

        private void InteractButtonPressed(InputAction.CallbackContext callbackContext)
        {
            OnInteractPressed?.Invoke();
        }
    }
}