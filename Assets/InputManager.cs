using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Core.Player.CharacterController;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    public bool PauseMenuOpenClose { get; private set; }
    public bool QuitWaitingScreen { get; private set; }

    //Player input
    private PlayerInput _playerInput;

    //Input Actions 
    private InputAction _pauseMenuOpenCloseAction;
    private InputAction _quitWaitingScreenAction;

    //Player
    public GameObject player;
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenuOpenClose = _pauseMenuOpenCloseAction.WasPressedThisFrame();

        QuitWaitingScreen = _quitWaitingScreenAction.WasPressedThisFrame();


        //Check if quit waiting screen 
        if(QuitWaitingScreen)
        {
            characterController.DeactivatePanel();
            characterController.EnableInput();
        }

    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        _pauseMenuOpenCloseAction = _playerInput.actions["PauseMenuOpenClose"];
        _quitWaitingScreenAction = _playerInput.actions["End Interaction"];

        characterController = player.GetComponent<CharacterController>();

    }


}
