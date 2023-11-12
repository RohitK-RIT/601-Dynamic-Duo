using System.Collections;
using System.Collections.Generic;
using Core.Game_Systems;
using Mini_Games.Boxer;
using Mini_Games.WireSwitch;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

public class CanvasWireSwitch : Mini_Games.MiniGame
{
    //A list to store different y vale
    List<float> yValueList = new() { 218f, 50f, -161f, -322f };


    //Set time for different wires
    private float startTimeRed = 0f;
    private float startTimeBlue = 0f;
    private float startTimeGreen = 0f;
    private float startTimePurple = 0f;


    //Hold time for 2 players
    public float requiredHoldTime = 3f;

    //Wire Image
    public Image wireRed;
    public Image wireBlue;
    public Image wireGreen;
    public Image wirePurple;

    //Player Icon Image
    public Image p1Icon;
    public Image p2Icon;

    //Players
    private GameObject player1;
    private GameObject player2;


    //Success Image
    public GameObject successImage;


    //Player Controllers
    private CharacterController characterController1;
    private CharacterController characterController2;


    //Audio
    AudioSource audioSource;


    //Progress Bar
    public Slider progressBar;


    //Onboarding
    public TextMeshProUGUI p1k1;
    public TextMeshProUGUI p1k2;
    public TextMeshProUGUI p1k3;
    public TextMeshProUGUI p1k4;

    public TextMeshProUGUI p2k1;
    public TextMeshProUGUI p2k2;
    public TextMeshProUGUI p2k3;
    public TextMeshProUGUI p2k4;

    private WirePuzzleInputHandler _p1InputHandler, _p2InputHandler;
    private bool _playerInteracted, _waitingForPlayerToInteract;
    
    void Awake()
    {
        wireRed.enabled = false;
        wireBlue.enabled = false;
        wireGreen.enabled = false;
        wirePurple.enabled = false;

        successImage.SetActive(false);


        //Player icons
        p1Icon.enabled = false;
        p2Icon.enabled = false;

        this.enabled = true;

        //Get player control
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        characterController1 = player1.GetComponent<CharacterController>();
        characterController2 = player2.GetComponent<CharacterController>();


        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        EnableInput();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        DisableInput();
        base.OnDisable();
    }

    protected override void OnDestroy()
    {
        DisableInput();
        base.OnDestroy();
    }

    private void EnableInput()
    {
        _p1InputHandler ??= new WirePuzzleInputHandler(PlayerID.Player1, this);
        _p2InputHandler ??= new WirePuzzleInputHandler(PlayerID.Player2, this);

        _p1InputHandler.Enable();
        _p2InputHandler.Enable();
    }

    private void DisableInput()
    {
        _p1InputHandler.Disable();
        _p2InputHandler.Disable();
    }

    void Start()
    {
        p1k1.text = _p1InputHandler.UpKey;
        p1k2.text = _p1InputHandler.LeftKey;
        p1k3.text = _p1InputHandler.DownKey;
        p1k4.text = _p1InputHandler.RightKey;

        p2k1.text = _p2InputHandler.UpKey;
        p2k2.text = _p2InputHandler.DownKey;
        p2k3.text = _p2InputHandler.LeftKey;
        p2k4.text = _p2InputHandler.RightKey;

        var successIntro = successImage.transform.Find("Intro");
        successIntro.gameObject.GetComponent<TextMeshProUGUI>().text = "Press " + _p1InputHandler.InteractKey + "/" + _p2InputHandler.InteractKey + " to close";
    }

    // Update is called once per frame
    void Update()
    {
        //if succeed, destroy interaction object and puzzle canvas
        if (wireRed.enabled && wireBlue.enabled && wireGreen.enabled && wirePurple.enabled)
        {
            //successImage.enabled = true;
            successImage.SetActive(true);
            progressBar.gameObject.SetActive(false);
            
            if(!_waitingForPlayerToInteract)
            {
                _waitingForPlayerToInteract = true;
                _p1InputHandler.OnInteractPressed += OnInteractPressed;
                _p2InputHandler.OnInteractPressed += OnInteractPressed;
            } 

            if (_playerInteracted)
            {
                _p1InputHandler.OnInteractPressed -= OnInteractPressed;
                _p2InputHandler.OnInteractPressed -= OnInteractPressed;
                
                //retrieve player control
                IsCompleted = true;

                Destroy(gameObject);
                return;
            }

        }

        //Check if wire should be connected
        CheckKeys();

        //Move player Icon when player press keys
        MovePlayerIcon();
    }

    private void OnInteractPressed()
    {
        _playerInteracted = true;
    }


    public void CheckKeys()
    {
        //Check purple wire
        if (_p1InputHandler.UpPressed && _p2InputHandler.RightPressed)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }


            if (startTimePurple == 0f)
            {
                startTimePurple = Time.time;
            }

            progressBar.value = (Time.time - startTimePurple) / requiredHoldTime;

            if (Time.time - startTimePurple >= requiredHoldTime)
            {
                audioSource.Stop();
                wirePurple.enabled = true;
            }

            return;
        }
        else
        {
            //audioSource.Stop();
            startTimePurple = 0f;
        }

        //Check Green
        if (_p1InputHandler.LeftPressed && _p2InputHandler.LeftPressed)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            if (startTimeGreen == 0f)
            {
                startTimeGreen = Time.time;
            }

            progressBar.value = (Time.time - startTimeGreen) / requiredHoldTime;

            if (Time.time - startTimeGreen >= requiredHoldTime)
            {
                audioSource.Stop();
                wireGreen.enabled = true;
            }

            return;
        }
        else
        {
            //audioSource.Stop();
            startTimeGreen = 0f;
        }


        //Check Red
        if (_p1InputHandler.DownPressed && _p2InputHandler.UpPressed)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            if (startTimeRed == 0f)
            {
                startTimeRed = Time.time;
            }

            progressBar.value = (Time.time - startTimeRed) / requiredHoldTime;

            if (Time.time - startTimeRed >= requiredHoldTime)
            {
                audioSource.Stop();
                wireRed.enabled = true;
            }

            return;
        }
        else
        {
            //audioSource.Stop();
            startTimeRed = 0f;
        }


        //Check Blue
        if (_p1InputHandler.RightPressed && _p2InputHandler.DownPressed)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            if (startTimeBlue == 0f)
            {
                startTimeBlue = Time.time;
            }

            progressBar.value = (Time.time - startTimeBlue) / requiredHoldTime;

            if (Time.time - startTimeBlue >= requiredHoldTime)
            {
                audioSource.Stop();
                wireBlue.enabled = true;
            }

            return;
        }
        else
        {
            //audioSource.Stop();
            startTimeBlue = 0f;
        }


        progressBar.value = 0f;
        audioSource.Stop();
    }


    public void MovePlayerIcon()
    {
        //Pl Icon
        if (_p1InputHandler.UpPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (_p1InputHandler.LeftPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (_p1InputHandler.DownPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (_p1InputHandler.RightPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[3]);
        }
        else
        {
            p1Icon.enabled = false;
        }

        //P2 Icon
        if (_p2InputHandler.UpPressed)
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (_p2InputHandler.DownPressed)
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (_p2InputHandler.LeftPressed)
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (_p2InputHandler.RightPressed)
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[3]);
        }
        else
        {
            p2Icon.enabled = false;
        }
    }
}