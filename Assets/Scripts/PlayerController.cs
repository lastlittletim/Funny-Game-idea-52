using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //declare variables
    public float movementSpeed;

    Rigidbody2D rb;
    public BoxCollider2D floorDetector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get rigidbody script
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        rb.velocity = new Vector2( //set velocity of object
            Input.GetAxis("Horizontal") * movementSpeed, //set horizontal velocity (x) to horizontal input (i.e. move in the direction)
            rb.velocity.y //leave vertical velocity as is, or gravity will not work
            );
    }
}
