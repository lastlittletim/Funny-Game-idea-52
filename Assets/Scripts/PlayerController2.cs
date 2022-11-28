using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float movementSpeed;

    public bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float horizontalInput = Input.GetAxis("Horizontal");

        //move


        rb.velocity = new Vector2(horizontalInput * movementSpeed  ,  rb.velocity.y);  

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (canJump)
            rb.velocity = Vector2.up * movementSpeed; 
            canJump = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Floor"))
        {
            canJump = true;
        }
    }
}
