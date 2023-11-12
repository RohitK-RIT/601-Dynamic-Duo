using System;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mini_Games.WireSwitch
{
    public class WirePuzzleInputHandler : PlayerInputHandler
    {
        public bool UpPressed { get; private set; }

        public string UpKey
        {
            get
            {
                switch (PlayerID)
                {
                    case PlayerID.Player1:
                        return Player1Input.WireSwitch.UpWireButton.bindings[0].ToDisplayString();
                    case PlayerID.Player2:
                        return Player2Input.WireSwitch.UpWireButton.bindings[0].ToDisplayString();
                    default:
                        return null;
                }
            }
        }

        public bool DownPressed { get; private set; }

        public string DownKey
        {
            get
            {
                switch (PlayerID)
                {
                    case PlayerID.Player1:
                        return Player1Input.WireSwitch.DownWireButton.bindings[0].ToDisplayString();
                    case PlayerID.Player2:
                        return Player2Input.WireSwitch.DownWireButton.bindings[0].ToDisplayString();
                    default:
                        return null;
                }
            }
        }

        public bool LeftPressed { get; private set; }

        public string LeftKey
        {
            get
            {
                switch (PlayerID)
                {
                    case PlayerID.Player1:
                        return Player1Input.WireSwitch.LeftWireButton.bindings[0].ToDisplayString();
                    case PlayerID.Player2:
                        return Player2Input.WireSwitch.LeftWireButton.bindings[0].ToDisplayString();
                    default:
                        return null;
                }
            }
        }

        public bool RightPressed { get; private set; }

        public string RightKey
        {
            get
            {
                switch (PlayerID)
                {
                    case PlayerID.Player1:
                        return Player1Input.WireSwitch.RightWireButton.bindings[0].ToDisplayString();
                    case PlayerID.Player2:
                        return Player2Input.WireSwitch.RightWireButton.bindings[0].ToDisplayString();
                    default:
                        return null;
                }
            }
        }

        public event Action OnInteractPressed;

        public string InteractKey
        {
            get
            {
                switch (PlayerID)
                {
                    case PlayerID.Player1:
                        return Player1Input.WireSwitch.Interact.bindings[0].ToDisplayString();
                    case PlayerID.Player2:
                        return Player2Input.WireSwitch.Interact.bindings[0].ToDisplayString();
                    default:
                        return null;
                }
            }
        }

        public WirePuzzleInputHandler(PlayerID playerID, MonoBehaviour owner) : base(playerID, owner)
        {
        }

        protected override void RegisterEvents()
        {
            switch (PlayerID)
            {
                case PlayerID.Player1:
                    Player1Input.WireSwitch.UpWireButton.performed += UpButtonPressed;
                    Player1Input.WireSwitch.UpWireButton.canceled += UpButtonReleased;
                    Player1Input.WireSwitch.DownWireButton.performed += DownButtonPressed;
                    Player1Input.WireSwitch.DownWireButton.canceled += DownButtonReleased;
                    Player1Input.WireSwitch.LeftWireButton.performed += LeftButtonPressed;
                    Player1Input.WireSwitch.LeftWireButton.canceled += LeftButtonReleased;
                    Player1Input.WireSwitch.RightWireButton.performed += RightButtonPressed;
                    Player1Input.WireSwitch.RightWireButton.canceled += RightButtonReleased;
                    Player1Input.WireSwitch.Interact.performed += InteractButtonPressed;
                    break;
                case PlayerID.Player2:
                    Player2Input.WireSwitch.UpWireButton.performed += UpButtonPressed;
                    Player2Input.WireSwitch.UpWireButton.canceled += UpButtonReleased;
                    Player2Input.WireSwitch.DownWireButton.performed += DownButtonPressed;
                    Player2Input.WireSwitch.DownWireButton.canceled += DownButtonReleased;
                    Player2Input.WireSwitch.LeftWireButton.performed += LeftButtonPressed;
                    Player2Input.WireSwitch.LeftWireButton.canceled += LeftButtonReleased;
                    Player2Input.WireSwitch.RightWireButton.performed += RightButtonPressed;
                    Player2Input.WireSwitch.RightWireButton.canceled += RightButtonReleased;
                    Player2Input.WireSwitch.Interact.performed += InteractButtonPressed;
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
                    Player1Input.WireSwitch.UpWireButton.performed -= UpButtonPressed;
                    Player1Input.WireSwitch.UpWireButton.canceled -= UpButtonReleased;
                    Player1Input.WireSwitch.DownWireButton.performed -= DownButtonPressed;
                    Player1Input.WireSwitch.DownWireButton.canceled -= DownButtonReleased;
                    Player1Input.WireSwitch.LeftWireButton.performed -= LeftButtonPressed;
                    Player1Input.WireSwitch.LeftWireButton.canceled -= LeftButtonReleased;
                    Player1Input.WireSwitch.RightWireButton.performed -= RightButtonPressed;
                    Player1Input.WireSwitch.RightWireButton.canceled -= RightButtonReleased;
                    Player1Input.WireSwitch.Interact.performed -= InteractButtonPressed;
                    break;
                case PlayerID.Player2:
                    Player2Input.WireSwitch.UpWireButton.performed -= UpButtonPressed;
                    Player2Input.WireSwitch.UpWireButton.canceled -= UpButtonReleased;
                    Player2Input.WireSwitch.DownWireButton.performed -= DownButtonPressed;
                    Player2Input.WireSwitch.DownWireButton.canceled -= DownButtonReleased;
                    Player2Input.WireSwitch.LeftWireButton.performed -= LeftButtonPressed;
                    Player2Input.WireSwitch.LeftWireButton.canceled -= LeftButtonReleased;
                    Player2Input.WireSwitch.RightWireButton.performed -= RightButtonPressed;
                    Player2Input.WireSwitch.RightWireButton.canceled -= RightButtonReleased;
                    Player2Input.WireSwitch.Interact.performed -= InteractButtonPressed;
                    break;
                default:
                    return;
            }
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