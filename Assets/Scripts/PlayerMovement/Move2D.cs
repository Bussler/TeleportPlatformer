using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    public float moveSpeed=6f;
    public float inAirMovement = 6f;
    public float jumpVelocity=6;
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;

    public bool isGrounded = false;

    public static bool isAllowedToMove = true; // change this in other scripts if in statis etc

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate() // movement in fixed update for smooth
    {
        if (!isAllowedToMove)
            return;

        //movement on x axis
        float amtToMove = Input.GetAxis("Horizontal");

        if (isGrounded)
            amtToMove = amtToMove * moveSpeed * Time.deltaTime;
        else
            amtToMove = amtToMove * inAirMovement * Time.deltaTime;

        transform.Translate(Vector2.right * amtToMove, Space.World);
    }

    void Update()
    {
        if (!TeleportRay.hasTarget) // only allow for jumping when not teleporting
            Jump();
    }

    void Jump()//function that performs jump-motion and moves player on y-axis
    {

        if (Input.GetButtonDown("Jump") && isGrounded) //the moment we press jump, we apply
        {
            rb.velocity = Vector2.up * jumpVelocity;
            isGrounded = false;
        }

        if (rb.velocity.y<0) // we are falling
        {
            rb.velocity += Vector2.up*Physics2D.gravity.y*(fallMultiplier-1)*Time.deltaTime; //create faster falling
        }
        else if (rb.velocity.y>0 && !Input.GetButton("Jump")) //jumping up
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; //jump higher when pressing jump button
        }
    }


}
