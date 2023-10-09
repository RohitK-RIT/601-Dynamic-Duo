using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : MonoBehaviour
{

    //Check if the player is in range
    private bool isInRange = false;

    //The interaction key
    [Tooltip("Use which key to interact")]
    public KeyCode interactKey = KeyCode.E;

    void Update()
    {

        if (isInRange == true && Input.GetKeyDown(interactKey))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        Debug.Log("Interact");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }


}
