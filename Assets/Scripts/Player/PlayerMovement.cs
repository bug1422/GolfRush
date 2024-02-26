using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : SlopeInfo
{
    [SerializeField]
    protected float speed = 10f;
    [SerializeField]
    protected float jumpStrength = 1000f;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private Transform groundCheck;
    private bool canJump;
    private bool isFallingThroughPlatform;
    private Vector2 velocity;
    private Vector2 jumpForce;
    private LayerMask groundLayer;
    protected override void Initializtion()
    {
        base.Initializtion();
        groundLayer = GetComponent<Layers>().GetGround();
    }
    protected void Update()
    {
        if (isAlive && !isGrabbingLedge && !(isHolding || isPlaying))
        {
            xInput = Input.GetAxisRaw("Horizontal");
            if (xInput == 1 && !isFacingRight) Flip();
            else if (xInput == -1 && isFacingRight) Flip();
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    protected void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();
    }

    private void Flip()
    {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isFallingThroughPlatform && rb.velocity.y != 0.0f)
        {
            isGrounded = false;
        }
        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }
    }
    private void ApplyMovement()
    {
        if (isAlive)
        {
            if ((isHolding || isPlaying) && isGrounded)
            {
                velocity = new Vector2(0.0f, 0.0f);
                rb.velocity = velocity;
            }
            else if (!isGrabbingLedge)
            {
                if (isGrounded && !isOnSlope && !isJumping) //if not on slope
                {
                    velocity = new Vector2(speed * xInput, 0.0f);
                    rb.velocity = velocity;
                }
                else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping && !isFallingThroughPlatform) //If on slope
                {
                    velocity = new Vector2(speed * slopeNormalPerp.x * -xInput, speed * slopeNormalPerp.y * -xInput);
                    rb.velocity = velocity;
                }
                else if (!isGrounded) //If in air
                {
                    velocity = new Vector2(speed * xInput, rb.velocity.y);
                    rb.velocity = velocity;
                }
            }
        }
    }


    protected void Jump()
    {
        if (canJump)
        {
            canJump = false;
            isJumping = true;
            velocity.Set(0.0f, 0.0f);
            rb.velocity = velocity;
            jumpForce.Set(0.0f, jumpStrength);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    public void setFalling(bool value)
    {
        isFallingThroughPlatform = value;
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
