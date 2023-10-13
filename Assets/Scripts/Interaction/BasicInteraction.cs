using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : MonoBehaviour
{

    //Check if the player is in range
    protected bool isPlayer1InRange = false;
    protected bool isPlayer2InRange = false;

    //The interaction key
    [Tooltip("Use which key to interact")]
    public KeyCode p1InteractKey = KeyCode.E;
    public KeyCode p2InteractKey = KeyCode.F;

    void Update()
    {
        //If player1 interact
        if (isPlayer1InRange == true && Input.GetKeyDown(p1InteractKey))
        {
            OnInteract();
            GameObject.Find("Player1").GetComponent<PlayerBehavior>().ActivatePanel();

            //Testing
            print("Player1");
        }

        //if player2 interact
        if (isPlayer2InRange == true && Input.GetKeyDown(p2InteractKey))
        {
            OnInteract();
            GameObject.Find("Player2").GetComponent<Test_character2>().ActivatePanel();

            //Testing
            print("Player2");
        }


    }


    protected virtual void OnInteract()
    {
        Debug.Log("Interact");
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer1InRange = true;
        }
        else if (other.CompareTag("Player2"))
        {
            isPlayer2InRange = true;
            
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer1InRange = false;
            GameObject.Find("Player1").GetComponent<PlayerBehavior>().DeactivatePanel();
        }
        else if(other.CompareTag("Player2"))
        {
            isPlayer2InRange = false;
            GameObject.Find("Player2").GetComponent<Test_character2>().DeactivatePanel();
        }
    }


}
