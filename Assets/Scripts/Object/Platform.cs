using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private Collider2D col;
    private bool onPlatform = false;
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
        yield return new WaitForSeconds(1f);
        col.enabled = true;
    }


    private void setPlayerOnPlatform(Collision2D other, bool value)
    {
        if (other.gameObject.CompareTag("Player"))
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
