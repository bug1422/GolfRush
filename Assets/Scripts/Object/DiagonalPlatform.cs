using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DiagonalPlatform : MonoBehaviour
{
    [SerializeField]
    private EdgeCollider2D col;
    [SerializeField]
    private float startTime;
    [SerializeField]
    private float endTime;
    [SerializeField]
    private float offset = 2.5f;
    private bool onPlatform = false;
    private Vector2 contact;
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && onPlatform)
        {
            col.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(CaculateSeconds());
        col.enabled = true;
    }

    private float CaculateSeconds()
    {
        var startDistance =  Vector2.Distance(col.points[0],contact);
        var endDistance = Vector2.Distance(col.points[1], contact);
        print(startDistance + " " + endDistance);
        if (startDistance < offset)
        {
            print('y');
            col.isTrigger = true;
            return 0;
        }
        else return Mathf.Lerp(startTime, endTime, endDistance / (startDistance + endDistance));
    }

    private void setPlayerOnPlatform(Collision2D other, bool value)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlatform = value;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        collision.gameObject.GetComponent<PlayerMovement>().setFalling(true);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerMovement>().setFalling(false);
        col.isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        setPlayerOnPlatform(collision, true);
        contact = collision.contacts.FirstOrDefault().point;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        setPlayerOnPlatform(collision, false);
    }
}
