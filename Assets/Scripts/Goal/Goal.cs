using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Animator anim;
    private Collider2D col;
    [SerializeField]
    private AnimationClip create;
    [SerializeField]
    private AnimationClip complete;
    [SerializeField]
    private float speedLimit = 0.01f;
    private bool isContinue;
    public static event Action spawnBall;
    private void Start()
    {
        isContinue = true;
        spawnBall?.Invoke();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(Creating());
        EventHandler.gameEnding += GoalEnding;
    }
    private void GoalEnding()
    {
        isContinue = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isContinue && collision.gameObject.CompareTag("Ball"))
        {
            var speed = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            if ( speed < speedLimit)
            {
                Destroy(collision.gameObject);
                EventHandler.UpdateScore();
                anim.SetTrigger("Win");
                StartCoroutine(Winning());
            }
        }
    }
    private IEnumerator Creating()
    {
        yield return new WaitForSeconds(create.length);
        anim.SetTrigger("ready");
        col.enabled = true;
    }
    private IEnumerator Winning()
    {
        yield return new WaitForSeconds(complete.length);
        Destroy(gameObject);
    }
    
}
