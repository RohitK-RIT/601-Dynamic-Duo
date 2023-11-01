using CharacterController = Core.Player.CharacterController;

using UnityEngine;

public class Test_TriggerWire : MonoBehaviour
{

    public GameObject prefabToInstantiate;

    private GameObject player1;
    private GameObject player2;

    private CharacterController characterController1;
    private CharacterController characterController2;

    bool isP1InRange;
    bool isP2InRange;

    bool isTrigger;


    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        characterController1 = player1.GetComponent<CharacterController>();
        characterController2 = player2.GetComponent<CharacterController>();
    }

    void Update()
    {

        if (!isTrigger) 
        { 
            if ((isP1InRange == true && Input.GetKey(KeyCode.E)) || (Input.GetKey(KeyCode.Slash) && isP2InRange == true))
            {
                Debug.Log("start mini game wire switch");
                GameObject newPrefabInstance = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
                newPrefabInstance.GetComponent<CanvasWireSwitch>().parent = this;

                //Stop player movement
                characterController1.enabled = false;
                characterController2.enabled = false;

                isTrigger = true;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isP1InRange = true;
        }
        else if(other.gameObject.tag == "Player2")
        {
            isP2InRange = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            isP1InRange = false;
        }
        else if (other.gameObject.tag == "Player2")
        {
            isP2InRange = false;
        }

    }


}
