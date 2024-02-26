using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpriteControl : Player
{
    private Animator anim;
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private List<Sprite> swingLevel;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            anim.enabled = false;
            sr.sprite = swingLevel[charge];
            StartCoroutine(Swing());
        }
        else
        {
            if(isAlive) anim.SetFloat("x", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isPlaying", isPlaying);
            anim.SetBool("isJumping", isJumping);
            anim.SetBool("isGrabbingLedge", isGrabbingLedge);
            anim.SetBool("isClimbing", isClimbing);
            anim.SetBool("isOnSlope", isOnSlope);
        }
    }

    private IEnumerator Swing()
    {
        yield return new WaitUntil(() => { return isPlaying; });
        anim.enabled = true;
        anim.Play("Golfing");
        sr.sprite = sprite;
    }
}
