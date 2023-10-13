using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorSwitch : BasicInteraction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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


    protected override void OnInteract()
    {
        
    }
}
