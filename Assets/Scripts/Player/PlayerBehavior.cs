using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    //Player's panel------by Rin
    private GameObject characterPanal;





    // Start is called before the first frame update
    void Start()
    {
        //Set player panel and deactivate-----by Rin
        characterPanal = GameObject.Find("Player1Panel");
        DeactivatePanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey)
            return;

        if (Input.GetKey(KeyCode.E))
        {
            Interact();
        }

        var movementDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            movementDirection += Vector2.up;

        if (Input.GetKey(KeyCode.S))
            movementDirection += Vector2.down;

        if (Input.GetKey(KeyCode.A))
            movementDirection += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            movementDirection += Vector2.right;

        Move(movementDirection);
    }

    private void Interact()
    {
        //Debug.Log("Kar interact");
    }

    private void Move(Vector2 direction)
    {
        var delta = direction * (speed * Time.deltaTime);

        transform.position += new Vector3(delta.x, 0, delta.y);
    }





    //Player Panel-------by Rin
    public void ActivatePanel()
    {
        characterPanal.SetActive(true);
    }

    public void DeactivatePanel()
    {
        characterPanal.SetActive(false);
    }
}