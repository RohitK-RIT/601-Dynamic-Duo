using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

public class CanvasWireSwitch : Mini_Games.MiniGame
{
    //A list to store different y vale
    List<float> yValueList = new List<float> { 218f, 50f, -161f, -322f };


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

    void Start()
    {
        p1k1.text = Player1Input.up.ToString();
        p1k2.text = Player1Input.left.ToString();
        p1k3.text = Player1Input.down.ToString();
        p1k4.text = Player1Input.right.ToString();

        p2k1.text = Player2Input.up.ToString();
        p2k2.text = Player2Input.down.ToString();
        p2k3.text = Player2Input.left.ToString();
        p2k4.text = Player2Input.right.ToString();

        Transform successIntro = successImage.transform.Find("Intro");
        successIntro.gameObject.GetComponent<TextMeshProUGUI>().text = "Press " + Player1Input.interaction.ToString() + "/" + Player2Input.interaction.ToString() + " to close";

    }

    // Update is called once per frame
    void Update()
    {

        //if succeed, destroy interaction object and puzzle canvas
        if(wireRed.enabled && wireBlue.enabled && wireGreen.enabled && wirePurple.enabled)
        {
            //successImage.enabled = true;
            successImage.SetActive(true);
            progressBar.gameObject.SetActive(false);


            if (Input.GetKey(Player1Input.interaction) || Input.GetKey(Player2Input.interaction))
            {

                //retrieve player control
                characterController1.enabled = true;
                characterController2.enabled = true;
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


    public void CheckKeys()
    {
        //Check purple wire
        if (Input.GetKey(Player1Input.up) && Input.GetKey(Player2Input.right))
        {

            if(audioSource.isPlaying == false)
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
        if (Input.GetKey(Player1Input.left) && Input.GetKey(Player1Input.left))
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
        if (Input.GetKey(Player1Input.down) && Input.GetKey(Player2Input.up))
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
        if (Input.GetKey(Player1Input.right) && Input.GetKey(Player2Input.down))
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
        if (Input.GetKey(Player1Input.up))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (Input.GetKey(Player1Input.left))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (Input.GetKey(Player1Input.down))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (Input.GetKey(Player1Input.right))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[3]);
        }
        else
        {
            p1Icon.enabled = false;
        }

        //P2 Icon
        if (Input.GetKey(Player2Input.up))
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (Input.GetKey(Player2Input.down))
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (Input.GetKey(Player2Input.left))
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (Input.GetKey(Player2Input.right))
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
