using System.Collections;
using System.Collections.Generic;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using UnityEngine.UI;
using TMPro;
using CharacterController = Core.Player.CharacterController;
using UnityEngine;

public class IconCheck : MonoBehaviour
{


    public GameObject player;
    CharacterController characterController;
    TextMeshProUGUI TM;




    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        TM = GetComponent<TextMeshProUGUI>();
        TM.text = characterController.GetCancelKey();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
