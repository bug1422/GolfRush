using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float shootLimit = 0.05f;
    [SerializeField]
    private GameObject glow;
    private Rigidbody2D rb;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GlowUp();
        if(rb.velocity.y == 0 && Mathf.Abs(rb.velocity.x) <= shootLimit)
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            isMoving = false;
        }
        else isMoving = true;
    }

    private void GlowUp()
    {
        glow.SetActive(!isMoving);
    }

    public bool IsShootable()
    {
        return rb.velocity.x <= shootLimit && rb.velocity.y <= shootLimit;
    }
}
