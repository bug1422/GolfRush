using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Player player;
    protected CapsuleCollider2D col;

    protected static bool isAlive;
    protected static bool isFacingRight;
    protected static bool isJumping;
    protected static bool isGrounded;
    protected static bool isGrabbingLedge;
    protected static bool isClimbing;
    protected static bool isOnSlope;
    protected static bool isHolding;
    protected static bool isPlaying;
    protected static int charge = 0;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isFacingRight = true;
        EventHandler.gameEnding += PlayerEnding;
        Initializtion();
    }
    protected virtual void Initializtion()
    {
        if(GetComponent<Rigidbody2D>()) 
        { 
            rb = GetComponent<Rigidbody2D>();
        }
        if(GetComponent<Player>())
        {
            player = GetComponent<Player>();
        }
        if (GetComponent<CapsuleCollider2D>())
        { 
            col = GetComponent<CapsuleCollider2D>();
        }
    }
    
    private void Update()
    {
        Globals.playerTransform = transform;
    }
    private void PlayerEnding()
    {
        isAlive = false;
    }
}
