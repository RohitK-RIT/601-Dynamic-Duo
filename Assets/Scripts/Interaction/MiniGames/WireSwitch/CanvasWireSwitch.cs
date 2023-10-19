using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

public class CanvasWireSwitch : MonoBehaviour
{
    //A list to store different y vale
    List<float> yValueList = new List<float> { 84f, 15f, -63f, -137f };


    //Set time for different wires
    private float startTimeRed = 0f;
    private float startTimeBlue = 0f;
    private float startTimeGreen = 0f;
    private float startTimePurple = 0f;

    public float requiredHoldTime = 3f;

    public Image wireRed;
    public Image wireBlue;
    public Image wireGreen;
    public Image wirePurple;

    public Image p1Icon;
    public Image p2Icon;


    private GameObject player1;
    private GameObject player2;

    private CharacterController characterController1;
    private CharacterController characterController2;

    //Used to delete the interact object
    public Test_TriggerWire parent;


    void Awake()
    {
        wireRed.enabled = false;
        wireBlue.enabled = false;
        wireGreen.enabled = false;
        wirePurple.enabled = false;

        //Player icons
        p1Icon.enabled = false;
        p2Icon.enabled = false;


        //Get player control
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        characterController1 = player1.GetComponent<CharacterController>();
        characterController2 = player2.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //if succeed, destroy interaction object and puzzle canvas
        if(wireRed.enabled && wireBlue.enabled && wireGreen.enabled && wirePurple.enabled)
        {
            Debug.Log("Destroy");

            //retrieve player control
            characterController1.enabled = true;
            characterController2.enabled = true;

            //Destroy puzzle
            Destroy(parent);
            Destroy(gameObject);
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

            if (startTimePurple == 0f)
            {
                startTimePurple = Time.time;
            }

            if (Time.time - startTimePurple >= requiredHoldTime)
            {
                wirePurple.enabled = true;
            }
        }
        else
        {
            startTimePurple = 0f;
        }

        //Check Green
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow))
        {

            if (startTimeGreen == 0f)
            {
                startTimeGreen = Time.time;
            }

            if (Time.time - startTimeGreen >= requiredHoldTime)
            {
                wireGreen.enabled = true;
            }
        }
        else
        {
            startTimeGreen = 0f;
        }


        //Check Red
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.UpArrow))
        {

            if (startTimeRed == 0f)
            {
                startTimeRed = Time.time;
            }

            if (Time.time - startTimeRed >= requiredHoldTime)
            {
                wireRed.enabled = true;
            }
        }
        else
        {
            startTimeRed = 0f;
        }


        //Check Blue
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.DownArrow))
        {

            if (startTimeBlue == 0f)
            {
                startTimeBlue = Time.time;
            }

            if (Time.time - startTimeBlue >= requiredHoldTime)
            {
                wireBlue.enabled = true;
            }
        }
        else
        {
            startTimeBlue = 0f;
        }
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
