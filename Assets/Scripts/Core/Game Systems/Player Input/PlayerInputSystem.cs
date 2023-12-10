using System;
using System.Collections.Generic;
using System.Linq;
using Core.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Core.Game_Systems.Player_Input
{
    public class PlayerInputSystem : MonoBehaviour
    {
        [SerializeField] private InputActionAsset p1ActionAsset, p2ActionAsset;

        private PlayerInput _p1Input, _p2Input;
        private List<PlayerInputListener> _p1InputListeners, _p2InputListeners;
        private PlayerInputListener _activeP1Listener, _activeP2Listener;

        private PlayerInputListener ActiveP1Listener
        {
            get => _activeP1Listener;
            set
            {
                if (_activeP1Listener == value) return;

                _activeP1Listener?.Disable();
                _activeP1Listener = value;
            }
        }

        private PlayerInputListener ActiveP2Listener
        {
            get => _activeP2Listener;
            set
            {
                if (_activeP2Listener == value) return;

                _activeP2Listener?.Disable();
                _activeP2Listener = value;
            }
        }

        public static PlayerInputSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            Init();
        }

        private void Init()
        {
            _p1Input = CreatePlayerInput(PlayerID.Player1);
            _p2Input = CreatePlayerInput(PlayerID.Player2);

            _p1Input.onDeviceLost += input =>
            {
                // input.user.UnpairDevices();
                input.SwitchCurrentControlScheme(nameof(Keyboard));
                var keyboard = InputSystem.devices.FirstOrDefault(device => device is Keyboard);
                InputUser.PerformPairingWithDevice(keyboard, input.user);
            };
            _p2Input.onDeviceLost += input =>
            {
                input.user.UnpairDevices();
                input.SwitchCurrentControlScheme(nameof(Keyboard));
                var keyboards = InputSystem.devices.Where(device => device is Keyboard).ToArray();
                InputUser.PerformPairingWithDevice(keyboards[keyboards.Length > 1 ? 1 : 0], input.user);
            };

            var gamepads = InputSystem.devices.Where(device => device is Gamepad).ToArray();
            var keyboards = InputSystem.devices.Where(device => device is Keyboard).ToArray();

            if (gamepads.Any())
            {
                _p1Input.SwitchCurrentControlScheme(nameof(Gamepad));
                InputUser.PerformPairingWithDevice(gamepads[0], _p1Input.user);

                if (gamepads.Length > 1)
                {
                    _p2Input.SwitchCurrentControlScheme(nameof(Gamepad));
                    InputUser.PerformPairingWithDevice(gamepads[1], _p2Input.user);
                }
                else
                {
                    _p2Input.SwitchCurrentControlScheme(nameof(Keyboard));
                    InputUser.PerformPairingWithDevice(keyboards[0], _p2Input.user);
                }
            }
            else
            {
                _p1Input.SwitchCurrentControlScheme(nameof(Keyboard));
                InputUser.PerformPairingWithDevice(keyboards[0], _p1Input.user);

                _p2Input.SwitchCurrentControlScheme(nameof(Keyboard));
                InputUser.PerformPairingWithDevice(keyboards[keyboards.Length > 1 ? 1 : 0], _p2Input.user);
            }

            _p1InputListeners = new List<PlayerInputListener>();
            _p2InputListeners = new List<PlayerInputListener>();
        }

        private PlayerInput CreatePlayerInput(PlayerID playerID)
        {
            var go = new GameObject($"{playerID.ToString()} Input");
            go.transform.SetParent(transform);

            var input = go.AddComponent<PlayerInput>();
            input.actions = playerID switch
            {
                PlayerID.Player1 => p1ActionAsset,
                PlayerID.Player2 => p2ActionAsset,
                _ => null
            };

            input.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;

            return input;
        }

        [ContextMenu("Dummy")]
        public void Dummy()
        {
            Debug.Log($"{nameof(Gamepad)}");
        }

        private void OnEnable()
        {
            EventManager.AddListener<PlayerInputHandlerRegistrationEvent>(RegisterPlayerInputHandler);
            EventManager.AddListener<PlayerInputHandlerUnregistrationEvent>(UnregisterPlayerInputHandler);
            EventManager.AddListener<PlayerInputAccessRequest>(RequestInputAccess);

            InputSystem.onDeviceChange += OnDeviceChanged;
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<PlayerInputHandlerRegistrationEvent>(RegisterPlayerInputHandler);
            EventManager.RemoveListener<PlayerInputHandlerUnregistrationEvent>(UnregisterPlayerInputHandler);
            EventManager.RemoveListener<PlayerInputAccessRequest>(RequestInputAccess);
        }

        #region Device Handling

        private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
        {
            if (change != InputDeviceChange.Added) return;

            PlayerInput input = null;
            string controlScheme = null;

            switch (device)
            {
                case Gamepad:
                    controlScheme = nameof(Gamepad);
                    if (!_p1Input.devices.Any(inputDevice => inputDevice is Gamepad) || _p1Input.devices.Contains(device))
                        input = _p1Input;
                    else if (!_p2Input.devices.Any(inputDevice => inputDevice is Gamepad) || _p1Input.devices.Contains(device))
                        input = _p2Input;

                    break;
                case Keyboard:
                    controlScheme = nameof(Keyboard);
                    if (!_p1Input.devices.Any(inputDevice => inputDevice is Gamepad) && InputSystem.devices.Count(inputDevice => inputDevice is Keyboard) > 1)
                        input = _p2Input;

                    break;
            }

            if (!input) return;

            input.SwitchCurrentControlScheme(controlScheme);
            InputUser.PerformPairingWithDevice(device, input.user);
        }

        #endregion

        #region Listener Handling

        private void RegisterPlayerInputHandler(PlayerInputHandlerRegistrationEvent @event)
        {
            @event.ActionMapCallback?.Invoke(GetActionMap(@event.PlayerID, @event.ActionMap));
            if (!@event.Listener.IsValid) return;

            switch (@event.PlayerID)
            {
                case PlayerID.Player1:
                    _p1InputListeners.Add(@event.Listener);
                    break;
                case PlayerID.Player2:
                    _p2InputListeners.Add(@event.Listener);
                    break;
            }
        }

        private InputActionMap GetActionMap(PlayerID playerID, ActionMap type)
        {
            var actionAsset = playerID switch
            {
                PlayerID.Player1 => p1ActionAsset,
                PlayerID.Player2 => p2ActionAsset,
                _ => null
            };

            return !actionAsset ? null : actionAsset.actionMaps.FirstOrDefault(map => map.name == type.ToString());
        }

        private void UnregisterPlayerInputHandler(PlayerInputHandlerUnregistrationEvent @event)
        {
            if (_p1InputListeners.Contains(@event.Listener))
                _p1InputListeners.Remove(@event.Listener);
            else if (_p2InputListeners.Contains(@event.Listener))
                _p2InputListeners.Remove(@event.Listener);
        }

        private void RequestInputAccess(PlayerInputAccessRequest request)
        {
            var listener = request.Listener;
            if (!listener.IsValid) return;

            var isActive = false;
            var mapName = listener.ActionMap.ToString();
            switch (listener.PlayerID)
            {
                case PlayerID.Player1:
                    _p1Input.SwitchCurrentActionMap(mapName);
                    if (_p1Input.currentActionMap.name == mapName)
                    {
                        ActiveP1Listener = listener;
                        isActive = true;
                    }

                    break;
                case PlayerID.Player2:
                    _p2Input.SwitchCurrentActionMap(mapName);
                    if (_p2Input.currentActionMap.name == mapName)
                    {
                        ActiveP2Listener = listener;
                        isActive = true;
                    }

                    break;
                default:
                    Debug.LogError("Player input access request failed (click to find which object)", listener.GameObject);
                    break;
            }

            request.OnCompleted?.Invoke(isActive);
        }

        #endregion

        public void EnableAllInput()
        {
            ActiveP1Listener?.TryEnable();
            ActiveP2Listener?.TryEnable();
        }

        public void DisableAllInput()
        {
            ActiveP1Listener?.Disable();
            ActiveP2Listener?.Disable();
        }
    }

    public class PlayerInputHandlerRegistrationEvent : GameEvent
    {
        public PlayerInputListener Listener { get; }
        public PlayerID PlayerID { get; }
        public ActionMap ActionMap { get; }
        public Action<InputActionMap> ActionMapCallback { get; }

        public PlayerInputHandlerRegistrationEvent(PlayerInputListener listener, PlayerID playerID, ActionMap actionMap, Action<InputActionMap> actionMapCallback)
        {
            Listener = listener;
            PlayerID = playerID;
            ActionMap = actionMap;
            ActionMapCallback = actionMapCallback;
        }
    }

    public class PlayerInputHandlerUnregistrationEvent : GameEvent
    {
        public PlayerInputListener Listener { get; }

        public PlayerInputHandlerUnregistrationEvent(PlayerInputListener listener)
        {
            Listener = listener;
        }
    }

    public class PlayerInputAccessRequest : GameEvent
    {
        public PlayerInputListener Listener { get; }
        internal readonly Action<bool> OnCompleted;

        public PlayerInputAccessRequest(PlayerInputListener listener, Action<bool> callback)
        {
            Listener = listener;
            OnCompleted = callback;
        }
    }
}