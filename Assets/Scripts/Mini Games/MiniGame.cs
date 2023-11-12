using System;
using System.Collections.Generic;
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

        public void Init(List<CharacterController> controllers)
        {
            for (var i = 0; i < 2; i++)
            {
                switch (controllers[i].PlayerID)
                {
                    case 1:
                        _characterController1 = controllers[i];
                        break;
                    case 2:
                        _characterController2 = controllers[i];
                        break;
                }
            }

            Player1Input = _characterController1.GetPlayerInput();
            Player2Input = _characterController2.GetPlayerInput();
        }

        protected virtual void OnEnable()
        {
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
            if (!Player1Input || !Player2Input)
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