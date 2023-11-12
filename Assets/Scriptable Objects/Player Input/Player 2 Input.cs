//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scriptable Objects/Player Input/Player 2 Input.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Player2Input: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player2Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player 2 Input"",
    ""maps"": [
        {
            ""name"": ""Level"",
            ""id"": ""5b39d756-fa55-4734-a446-d82606aaa8b1"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""e4492304-fa52-4ba2-ae1c-30505855176f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Begin Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""73b7cd8f-8f4a-4126-a563-0cbbdb945dd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""End Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""9a6b038e-fa9f-4f0d-ac4c-cb0700667606"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""cff23ed7-4b86-4400-a35a-83e1e0d9975b"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bb00bfaf-14c5-4595-911d-a77faa3e9ff8"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d557f151-9d96-401b-8476-c4f1e1dac7c0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""98839818-9faa-4963-8ae7-3221dd4e4086"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9a8d14a6-5d4f-4f50-a20b-83b6bff58c9a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6c6afb37-66ec-4e04-8926-ec6121a36474"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dfa7ce4-e882-4c5f-aab7-c2e52dac8c57"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Begin Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6546529e-f96e-44e4-a004-8199a5ed8ee8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Begin Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""473e5c8b-889f-410d-8f53-003d4ac54c95"",
                    ""path"": ""<Keyboard>/period"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""End Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5d13874-ed46-4d12-8c18-c52d1b9cbe33"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""End Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Boxer"",
            ""id"": ""f6206674-8613-433c-954b-6b1d10334b86"",
            ""actions"": [
                {
                    ""name"": ""Navigation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a0cf655e-3c2e-40c2-a3c4-23ab229159b9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""37cee60c-c3b5-492c-91cf-684683866bc0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""a1d1012e-08b4-4ae7-b427-0267703034f8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7c40d51d-04f1-4e3c-a889-e205ad07cfed"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e78b0c7a-57b2-40d0-a00f-7659d17d4d09"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e41c9a26-c8c7-4089-8d9d-51fd6ac07fe9"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""29e6756d-6b23-4264-98b4-621a2c026211"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Navigation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""02a9a21a-fdd1-4246-9934-30b53f54fa50"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Wire Switch"",
            ""id"": ""caf06895-633a-40ea-95fe-20ed208b46c9"",
            ""actions"": [
                {
                    ""name"": ""Up Wire Button"",
                    ""type"": ""Button"",
                    ""id"": ""0b3594f0-0a46-430c-aaa9-ce37c33abf13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down Wire Button"",
                    ""type"": ""Button"",
                    ""id"": ""4d09a695-ac49-4f3f-a7c3-4e1df96640e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left Wire Button"",
                    ""type"": ""Button"",
                    ""id"": ""bfa8be7c-a788-4854-b5a2-b5b215664c3c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right Wire Button"",
                    ""type"": ""Button"",
                    ""id"": ""8fcac925-44b8-4de9-b557-4727c981cb0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""75fdbdc2-ac16-464c-953f-933ffe04bfb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3a650125-f9f8-4f41-8a61-eadd5f4d25c7"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Up Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4f4cbbe-dd6c-4400-b9e7-956b7f2d439f"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Up Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b24759a-95d5-4e5f-8597-7c486763c824"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2d710a3-005b-42d7-b2a4-6a38bda32b5b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Left Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50ea8c15-0148-4eff-b2a5-c6c4feebb67b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Right Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b4c84d0-c0d2-4e48-ae8c-bbf4ca3cab94"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Down Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f103623-9144-4308-b9a1-1f5d13ced1a5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Left Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ceafb2c7-e06e-49c2-9eb5-a9f8d2eb9341"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Right Wire Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d5d47b1-0067-44e3-8cdc-013c637277ca"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad85eb69-9f3e-4f40-8d8f-3d018439ab10"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Level
        m_Level = asset.FindActionMap("Level", throwIfNotFound: true);
        m_Level_Movement = m_Level.FindAction("Movement", throwIfNotFound: true);
        m_Level_BeginInteraction = m_Level.FindAction("Begin Interaction", throwIfNotFound: true);
        m_Level_EndInteraction = m_Level.FindAction("End Interaction", throwIfNotFound: true);
        // Boxer
        m_Boxer = asset.FindActionMap("Boxer", throwIfNotFound: true);
        m_Boxer_Navigation = m_Boxer.FindAction("Navigation", throwIfNotFound: true);
        m_Boxer_Interact = m_Boxer.FindAction("Interact", throwIfNotFound: true);
        // Wire Switch
        m_WireSwitch = asset.FindActionMap("Wire Switch", throwIfNotFound: true);
        m_WireSwitch_UpWireButton = m_WireSwitch.FindAction("Up Wire Button", throwIfNotFound: true);
        m_WireSwitch_DownWireButton = m_WireSwitch.FindAction("Down Wire Button", throwIfNotFound: true);
        m_WireSwitch_LeftWireButton = m_WireSwitch.FindAction("Left Wire Button", throwIfNotFound: true);
        m_WireSwitch_RightWireButton = m_WireSwitch.FindAction("Right Wire Button", throwIfNotFound: true);
        m_WireSwitch_Interact = m_WireSwitch.FindAction("Interact", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Level
    private readonly InputActionMap m_Level;
    private List<ILevelActions> m_LevelActionsCallbackInterfaces = new List<ILevelActions>();
    private readonly InputAction m_Level_Movement;
    private readonly InputAction m_Level_BeginInteraction;
    private readonly InputAction m_Level_EndInteraction;
    public struct LevelActions
    {
        private @Player2Input m_Wrapper;
        public LevelActions(@Player2Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Level_Movement;
        public InputAction @BeginInteraction => m_Wrapper.m_Level_BeginInteraction;
        public InputAction @EndInteraction => m_Wrapper.m_Level_EndInteraction;
        public InputActionMap Get() { return m_Wrapper.m_Level; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelActions set) { return set.Get(); }
        public void AddCallbacks(ILevelActions instance)
        {
            if (instance == null || m_Wrapper.m_LevelActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LevelActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @BeginInteraction.started += instance.OnBeginInteraction;
            @BeginInteraction.performed += instance.OnBeginInteraction;
            @BeginInteraction.canceled += instance.OnBeginInteraction;
            @EndInteraction.started += instance.OnEndInteraction;
            @EndInteraction.performed += instance.OnEndInteraction;
            @EndInteraction.canceled += instance.OnEndInteraction;
        }

        private void UnregisterCallbacks(ILevelActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @BeginInteraction.started -= instance.OnBeginInteraction;
            @BeginInteraction.performed -= instance.OnBeginInteraction;
            @BeginInteraction.canceled -= instance.OnBeginInteraction;
            @EndInteraction.started -= instance.OnEndInteraction;
            @EndInteraction.performed -= instance.OnEndInteraction;
            @EndInteraction.canceled -= instance.OnEndInteraction;
        }

        public void RemoveCallbacks(ILevelActions instance)
        {
            if (m_Wrapper.m_LevelActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILevelActions instance)
        {
            foreach (var item in m_Wrapper.m_LevelActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LevelActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LevelActions @Level => new LevelActions(this);

    // Boxer
    private readonly InputActionMap m_Boxer;
    private List<IBoxerActions> m_BoxerActionsCallbackInterfaces = new List<IBoxerActions>();
    private readonly InputAction m_Boxer_Navigation;
    private readonly InputAction m_Boxer_Interact;
    public struct BoxerActions
    {
        private @Player2Input m_Wrapper;
        public BoxerActions(@Player2Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigation => m_Wrapper.m_Boxer_Navigation;
        public InputAction @Interact => m_Wrapper.m_Boxer_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Boxer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BoxerActions set) { return set.Get(); }
        public void AddCallbacks(IBoxerActions instance)
        {
            if (instance == null || m_Wrapper.m_BoxerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BoxerActionsCallbackInterfaces.Add(instance);
            @Navigation.started += instance.OnNavigation;
            @Navigation.performed += instance.OnNavigation;
            @Navigation.canceled += instance.OnNavigation;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(IBoxerActions instance)
        {
            @Navigation.started -= instance.OnNavigation;
            @Navigation.performed -= instance.OnNavigation;
            @Navigation.canceled -= instance.OnNavigation;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(IBoxerActions instance)
        {
            if (m_Wrapper.m_BoxerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBoxerActions instance)
        {
            foreach (var item in m_Wrapper.m_BoxerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BoxerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BoxerActions @Boxer => new BoxerActions(this);

    // Wire Switch
    private readonly InputActionMap m_WireSwitch;
    private List<IWireSwitchActions> m_WireSwitchActionsCallbackInterfaces = new List<IWireSwitchActions>();
    private readonly InputAction m_WireSwitch_UpWireButton;
    private readonly InputAction m_WireSwitch_DownWireButton;
    private readonly InputAction m_WireSwitch_LeftWireButton;
    private readonly InputAction m_WireSwitch_RightWireButton;
    private readonly InputAction m_WireSwitch_Interact;
    public struct WireSwitchActions
    {
        private @Player2Input m_Wrapper;
        public WireSwitchActions(@Player2Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @UpWireButton => m_Wrapper.m_WireSwitch_UpWireButton;
        public InputAction @DownWireButton => m_Wrapper.m_WireSwitch_DownWireButton;
        public InputAction @LeftWireButton => m_Wrapper.m_WireSwitch_LeftWireButton;
        public InputAction @RightWireButton => m_Wrapper.m_WireSwitch_RightWireButton;
        public InputAction @Interact => m_Wrapper.m_WireSwitch_Interact;
        public InputActionMap Get() { return m_Wrapper.m_WireSwitch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WireSwitchActions set) { return set.Get(); }
        public void AddCallbacks(IWireSwitchActions instance)
        {
            if (instance == null || m_Wrapper.m_WireSwitchActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_WireSwitchActionsCallbackInterfaces.Add(instance);
            @UpWireButton.started += instance.OnUpWireButton;
            @UpWireButton.performed += instance.OnUpWireButton;
            @UpWireButton.canceled += instance.OnUpWireButton;
            @DownWireButton.started += instance.OnDownWireButton;
            @DownWireButton.performed += instance.OnDownWireButton;
            @DownWireButton.canceled += instance.OnDownWireButton;
            @LeftWireButton.started += instance.OnLeftWireButton;
            @LeftWireButton.performed += instance.OnLeftWireButton;
            @LeftWireButton.canceled += instance.OnLeftWireButton;
            @RightWireButton.started += instance.OnRightWireButton;
            @RightWireButton.performed += instance.OnRightWireButton;
            @RightWireButton.canceled += instance.OnRightWireButton;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(IWireSwitchActions instance)
        {
            @UpWireButton.started -= instance.OnUpWireButton;
            @UpWireButton.performed -= instance.OnUpWireButton;
            @UpWireButton.canceled -= instance.OnUpWireButton;
            @DownWireButton.started -= instance.OnDownWireButton;
            @DownWireButton.performed -= instance.OnDownWireButton;
            @DownWireButton.canceled -= instance.OnDownWireButton;
            @LeftWireButton.started -= instance.OnLeftWireButton;
            @LeftWireButton.performed -= instance.OnLeftWireButton;
            @LeftWireButton.canceled -= instance.OnLeftWireButton;
            @RightWireButton.started -= instance.OnRightWireButton;
            @RightWireButton.performed -= instance.OnRightWireButton;
            @RightWireButton.canceled -= instance.OnRightWireButton;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(IWireSwitchActions instance)
        {
            if (m_Wrapper.m_WireSwitchActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IWireSwitchActions instance)
        {
            foreach (var item in m_Wrapper.m_WireSwitchActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_WireSwitchActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public WireSwitchActions @WireSwitch => new WireSwitchActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ILevelActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnBeginInteraction(InputAction.CallbackContext context);
        void OnEndInteraction(InputAction.CallbackContext context);
    }
    public interface IBoxerActions
    {
        void OnNavigation(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IWireSwitchActions
    {
        void OnUpWireButton(InputAction.CallbackContext context);
        void OnDownWireButton(InputAction.CallbackContext context);
        void OnLeftWireButton(InputAction.CallbackContext context);
        void OnRightWireButton(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}