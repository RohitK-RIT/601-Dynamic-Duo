using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    public bool PauseMenuOpenClose { get; private set; }


    private PlayerInput _playerInput;

    private InputAction _pauseMenuOpenCloseAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenuOpenClose = _pauseMenuOpenCloseAction.WasPressedThisFrame();


    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        _pauseMenuOpenCloseAction = _playerInput.actions["PauseMenuOpenClose"];


    }

    public void ChangeDefaultMap(string s)
    {
        _playerInput.SwitchCurrentActionMap(s);
    }
}
