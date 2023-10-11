using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_character2 : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey)
            return;

        var movementDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            movementDirection += Vector2.up;

        if (Input.GetKey(KeyCode.DownArrow))
            movementDirection += Vector2.down;

        if (Input.GetKey(KeyCode.RightArrow))
            movementDirection += Vector2.left;

        if (Input.GetKey(KeyCode.LeftArrow))
            movementDirection += Vector2.right;

        Move(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        var delta = direction * (speed * Time.deltaTime);

        transform.position += new Vector3(delta.x, 0, delta.y);
    }
}
