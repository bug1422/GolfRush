using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : Player
{
    [SerializeField]
    private AnimationClip swing;
    [SerializeField]
    private int chargeLevel = 5;
    [SerializeField]
    private float chargeTime = 5f;
    [SerializeField]
    private float chargeForce = 5f;
    [SerializeField]
    private float curveXY = 1f;
    private float chargePower;
    private float percentage;
    private bool shootable = false;
    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        percentage = 100f / chargeLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && isGrounded && !isHolding && !isPlaying && Input.GetMouseButtonDown(0))
        {
            isHolding = true;
            StartCoroutine(IsHolding());
            StartCoroutine(Charging());
        }
        if (isHolding)
        {
            var value = Mathf.FloorToInt(chargePower / percentage);
            charge = value;
        }
    }

    private IEnumerator IsHolding()
    {
        yield return new WaitUntil(() => { return Input.GetMouseButtonUp(0); });
        Shoot();
        isHolding = false;
        isPlaying = true;
        charge = 0;
        chargePower = 0;
        StartCoroutine(WaitForClip());
    }
    private void Shoot()
    {
        if(ball != null)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            int facing = isFacingRight ? 1 : -1;
            Vector2 force = new Vector2(facing * charge * percentage * chargeForce * curveXY, charge * percentage * chargeForce * (1/curveXY));
            rb.AddForce(force,ForceMode2D.Impulse);
        }
    }
    private IEnumerator WaitForClip()
    {
        yield return new WaitForSeconds(swing.length);
        isPlaying = false;
    }

    private IEnumerator Charging()
    {
        float time = 0f;
        Vector2 startValue = transform.position;
        while (time <= chargeTime)
        {
            chargePower = Mathf.Lerp(0f, 100f, time / chargeTime);
            time += Time.deltaTime;
            if (!isHolding) break;
            yield return null;
        }
        if (isHolding) chargePower = 100f;
        else chargePower = 0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.gameObject);
        if (collision.CompareTag("Ball"))
        {
            ball = collision.gameObject;
            shootable = ball.GetComponent<Ball>().IsShootable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            shootable = false;
            ball = null;
        }
    }
}
