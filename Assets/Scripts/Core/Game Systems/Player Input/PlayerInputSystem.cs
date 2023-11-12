using System;
using System.Collections.Generic;
using Core.Managers;
using UnityEngine;

namespace Core.Game_Systems.Player_Input
{
    public class PlayerInputSystem : MonoBehaviour
    {
        private List<PlayerInputHandler> _p1InputHandlers, _p2InputHandlers;

        private PlayerInputHandler _activeP1Handler, _activeP2Handler;

        private PlayerInputHandler ActiveP1Handler
        {
            get => _activeP1Handler;
            set
            {
                if (_activeP1Handler == value) return;

                _activeP1Handler?.Disable();
                _activeP1Handler = value;
                _activeP1Handler.Enable();
            }
        }

        private PlayerInputHandler ActiveP2Handler
        {
            get => _activeP2Handler;
            set
            {
                if (_activeP2Handler == value) return;

                _activeP2Handler?.Disable();
                _activeP2Handler = value;
                _activeP2Handler?.Enable();
            }
        }

        public static PlayerInputSystem Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _p1InputHandlers = new List<PlayerInputHandler>();
            _p2InputHandlers = new List<PlayerInputHandler>();
        }

        private void OnEnable()
        {
            EventManager.AddListener<PlayerInputHandlerRegistrationEvent>(RegisterPlayerInputHandler);
            EventManager.AddListener<PlayerInputHandlerUnregistrationEvent>(UnregisterPlayerInputHandler);
            EventManager.AddListener<PlayerInputAccessRequest>(RequestInputAccess);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<PlayerInputHandlerRegistrationEvent>(RegisterPlayerInputHandler);
            EventManager.RemoveListener<PlayerInputHandlerUnregistrationEvent>(UnregisterPlayerInputHandler);
            EventManager.RemoveListener<PlayerInputAccessRequest>(RequestInputAccess);
        }

        private void RequestInputAccess(PlayerInputAccessRequest request)
        {
            switch (request.PlayerInputHandler.PlayerID)
            {
                case PlayerID.Player1:
                    if (_p1InputHandlers.Contains(request.PlayerInputHandler))
                    {
                        ActiveP1Handler = request.PlayerInputHandler;
                        request.OnCompleted?.Invoke(ActiveP1Handler == request.PlayerInputHandler);
                    }

                    break;
                case PlayerID.Player2:
                    if (_p2InputHandlers.Contains(request.PlayerInputHandler))
                    {
                        ActiveP2Handler = request.PlayerInputHandler;
                        request.OnCompleted?.Invoke(ActiveP2Handler == request.PlayerInputHandler);
                    }

                    break;
                default: return;
            }
        }

        private void RegisterPlayerInputHandler(PlayerInputHandlerRegistrationEvent @event)
        {
            switch (@event.PlayerInputHandler.PlayerID)
            {
                case PlayerID.Player1:
                    _p1InputHandlers.Add(@event.PlayerInputHandler);
                    return;
                case PlayerID.Player2:
                    _p2InputHandlers.Add(@event.PlayerInputHandler);
                    return;
                default: return;
            }
        }

        private void UnregisterPlayerInputHandler(PlayerInputHandlerUnregistrationEvent @event)
        {
            switch (@event.PlayerInputHandler.PlayerID)
            {
                case PlayerID.Player1:
                    _p1InputHandlers.Remove(@event.PlayerInputHandler);
                    break;
                case PlayerID.Player2:
                    _p2InputHandlers.Remove(@event.PlayerInputHandler);
                    break;
                default: return;
            }
        }
    }

    public class PlayerInputHandlerRegistrationEvent : GameEvent
    {
        public PlayerInputHandler PlayerInputHandler { get; }

        public PlayerInputHandlerRegistrationEvent(PlayerInputHandler playerInputHandler)
        {
            PlayerInputHandler = playerInputHandler;
        }
    }

    public class PlayerInputHandlerUnregistrationEvent : GameEvent
    {
        public PlayerInputHandler PlayerInputHandler { get; }

        public PlayerInputHandlerUnregistrationEvent(PlayerInputHandler playerInputHandler)
        {
            PlayerInputHandler = playerInputHandler;
        }
    }

    public class PlayerInputAccessRequest : GameEvent
    {
        public PlayerInputHandler PlayerInputHandler { get; }
        internal Action<bool> OnCompleted;

        public PlayerInputAccessRequest(PlayerInputHandler playerInputHandler, Action<bool> callback)
        {
            PlayerInputHandler = playerInputHandler;
            OnCompleted = callback;
        }
    }
}