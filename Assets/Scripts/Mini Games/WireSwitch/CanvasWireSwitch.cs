using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    // Update is called once per frame
    void Update()
    {

        //if succeed, destroy interaction object and puzzle canvas
        if(wireRed.enabled && wireBlue.enabled && wireGreen.enabled && wirePurple.enabled)
        {
            //successImage.enabled = true;
            successImage.SetActive(true);
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Slash))
            {

                //retrieve player control
                characterController1.enabled = true;
                characterController2.enabled = true;
                isCompleted = true;
                Destroy(gameObject);
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
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.RightArrow))
        {

            if(audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
            

            if (startTimePurple == 0f)
            {
                startTimePurple = Time.time;
            }

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
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow))
        {

            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            if (startTimeGreen == 0f)
            {
                startTimeGreen = Time.time;
            }

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
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.UpArrow))
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            if (startTimeRed == 0f)
            {
                startTimeRed = Time.time;
            }

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
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.DownArrow))
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            if (startTimeBlue == 0f)
            {
                startTimeBlue = Time.time;
            }

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

        audioSource.Stop();
    }


    public void MovePlayerIcon()
    {
        //Pl Icon
        if (Input.GetKey(KeyCode.W))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            p1Icon.enabled = true;
            p1Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p1Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[3]);
        }
        else
        {
            p1Icon.enabled = false;
        }

        //P2 Icon
        if (Input.GetKey(KeyCode.UpArrow))
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[0]);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[1]);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            p2Icon.enabled = true;
            p2Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(p2Icon.GetComponent<RectTransform>().anchoredPosition.x, yValueList[2]);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
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
