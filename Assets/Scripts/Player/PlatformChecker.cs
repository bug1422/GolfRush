using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChecker : Player
{
    private bool onPlatform = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void setPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            onPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        setPlayerOnPlatform(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        setPlayerOnPlatform(collision, false);
    }
}
