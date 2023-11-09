using System;
using System.Linq;
using Core.Game_Systems;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Mini_Games
{
    public abstract class MiniGame : MonoBehaviour
    {
        public event Action<bool> OnClosed;
        protected bool IsCompleted;

        protected PlayerInput Player1Input, Player2Input;
        private CharacterController _characterController1, _characterController2;

        public static bool IsOpen { get; private set; }

        protected virtual void OnEnable()
        {
            var controllers = FindObjectsOfType<CharacterController>();
            _characterController1 = controllers.FirstOrDefault(controller => controller.name == "Player1");
            _characterController2 = controllers.FirstOrDefault(controller => controller.name == "Player2");

            if(!_characterController1 || !_characterController2)
            {
                gameObject.SetActive(false);
                return;
            }
            
            Player1Input = _characterController1.GetPlayerInput();
            Player2Input = _characterController2.GetPlayerInput();
            
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
            if(!Player1Input || !Player2Input)
                return;
            
            _characterController1.SetPlayerInput(Player1Input);
            _characterController2.SetPlayerInput(Player2Input);

            Player1Input = null;
            Player2Input = null;
            
            OnClosed?.Invoke(IsCompleted);
            IsOpen = false;
        }
    }
}