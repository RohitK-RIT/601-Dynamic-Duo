using System.Collections;
using System.Collections.Generic;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
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

    private WirePuzzleInputListener _p1InputListener, _p2InputListener;
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
        _p1InputListener ??= new WirePuzzleInputListener(PlayerID.Player1, actionMap, this);
        _p2InputListener ??= new WirePuzzleInputListener(PlayerID.Player2, actionMap, this);

        _p1InputListener.TryEnable();
        _p2InputListener.TryEnable();
    }

    private void DisableInput()
    {
        _p1InputListener.Disable();
        _p2InputListener.Disable();
    }

    void Start()
    {
        p1k1.text = _p1InputListener.UpKey;
        p1k2.text = _p1InputListener.LeftKey;
        p1k3.text = _p1InputListener.DownKey;
        p1k4.text = _p1InputListener.RightKey;

        p2k1.text = _p2InputListener.UpKey;
        p2k2.text = _p2InputListener.DownKey;
        p2k3.text = _p2InputListener.LeftKey;
        p2k4.text = _p2InputListener.RightKey;

        var successIntro = successImage.transform.Find("Intro");
        successIntro.gameObject.GetComponent<TextMeshProUGUI>().text = "Press " + _p1InputListener.InteractKey + "/" + _p2InputListener.InteractKey + " to close";
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

            if (!_waitingForPlayerToInteract)
            {
                _waitingForPlayerToInteract = true;
                _p1InputListener.OnInteractPressed += OnInteractPressed;
                _p2InputListener.OnInteractPressed += OnInteractPressed;
            }

            if (_playerInteracted)
            {
                _p1InputListener.OnInteractPressed -= OnInteractPressed;
                _p2InputListener.OnInteractPressed -= OnInteractPressed;

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
        if (_p1InputListener.UpPressed && _p2InputListener.RightPressed)
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
        if (_p1InputListener.LeftPressed && _p2InputListener.LeftPressed)
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
        if (_p1InputListener.DownPressed && _p2InputListener.UpPressed)
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
        if (_p1InputListener.RightPressed && _p2InputListener.DownPressed)
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
        if (_p1InputListener.UpPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (_p1InputListener.LeftPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (_p1InputListener.DownPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (_p1InputListener.RightPressed)
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[3]);
        }
        else
        {
            p1Icon.enabled = false;
        }

        //P2 Icon
        if (_p2InputListener.UpPressed)
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (_p2InputListener.DownPressed)
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (_p2InputListener.LeftPressed)
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (_p2InputListener.RightPressed)
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