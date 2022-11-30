﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D mainCollider;
    public Camera cam;
    public LayerMask terrainMask;
    float originalCameraSize;
    public float movementSpeed;

    public float dashDistance;
    public float dashTime;
    public float postDashVelocityMultiplier;

    public int mana;

    bool allowInput = true;
    bool isDashing;
    bool canJump;

    IEnumerator dashRoutine;

    // Start is called before the first frame update
    void Start()
    {
        originalCameraSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (!allowInput) return; //if not allowInput, end the function here

        //get input
        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);

        //do the thing
        Vector2 newVelocity = rb.velocity;

        newVelocity.x = horizontalInput * movementSpeed; //set horizontal velocity to input
        if (jumpInput) //if jump button pressed
        {
            if (canJump) //if allowed to jump
            {
                newVelocity.y = movementSpeed * 5; //set y vel to movementSpeed (i.e. jump)
                canJump = false; //disable jump
            }
        }

        rb.velocity = newVelocity; //set velocity

        if (!isDashing && dashRoutine == null && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashRoutine = Dash(); //create a new dash routine
            StartCoroutine(dashRoutine); //start routine
        };
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Floor"))
        {
            canJump = true;
            if(dashRoutine != null) //if in the middle of a dash
            {
                StopCoroutine(dashRoutine); //stop dash (or will clip through the floor (could be mechanic maybe?))
                dashRoutine = null; //unassign
                isDashing = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing == true)
        {
            Dodge();
        }
    }

    void Dodge()
    {
        Debug.Log("Fortnite");
        ++mana; //add one to mana count
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float dashTimer = 0;
        float modifiedDashDistance = dashDistance;
 
        Vector3 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //get horizontal and vertical inputs, then map to a vector
        Vector3 startPos = transform.position; //get starting position 
        //float originalCameraSize = cam.orthographicSize; //save original size

        //prevent clipping
        RaycastHit2D hit = Physics2D.Raycast(transform.position, inputVector.normalized, modifiedDashDistance, terrainMask);
        if(hit)
        {
            Vector2 stopPos = hit.point;
            modifiedDashDistance = ((Vector2)startPos - stopPos).magnitude;
            modifiedDashDistance -= mainCollider.bounds.extents.magnitude;
        }
        
        //do the thing
        while (dashTimer < dashTime)
        {
            transform.position = modifiedDashDistance / dashTime * dashTimer * inputVector.normalized + startPos; //set position to direction of dash * distance * time passed as percentage of timer + starting position
            cam.orthographicSize = originalCameraSize * dashTimer / dashTime * 0.1f + originalCameraSize * 0.9f; //camera size shenenigans
            dashTimer += Time.deltaTime; //update timer 
            yield return null; //wait a frame
           
        }

        rb.velocity = inputVector.normalized * modifiedDashDistance / dashTime * postDashVelocityMultiplier; //allow some of the velocity to carry through after
        cam.orthographicSize = originalCameraSize; //reset camera size
        isDashing = false;
        dashRoutine = null;
    }
    
}
