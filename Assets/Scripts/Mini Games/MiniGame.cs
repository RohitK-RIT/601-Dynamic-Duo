using System;
using Core.Game_Systems;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Mini_Games
{
    public abstract class MiniGame : MonoBehaviour
    {
        public event Action<bool> OnClosed;
        protected bool isCompleted;

        protected PlayerInput player1Input, player2Input;
        private CharacterController _characterController1, _characterController2;

        public static bool IsOpen { get; private set; }

        protected virtual void OnEnable()
        {
            _characterController1 = GameObject.Find("Player1").GetComponent<CharacterController>();
            _characterController2 = GameObject.Find("Player2").GetComponent<CharacterController>();

            player1Input = _characterController1.GetPlayerInput();
            player2Input = _characterController2.GetPlayerInput();
            
            isCompleted = false;
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
            if(!player1Input || !player2Input)
                return;
            
            _characterController1.SetPlayerInput(player1Input);
            _characterController2.SetPlayerInput(player2Input);

            player1Input = null;
            player2Input = null;
            
            OnClosed?.Invoke(isCompleted);
            IsOpen = false;
        }
    }
}