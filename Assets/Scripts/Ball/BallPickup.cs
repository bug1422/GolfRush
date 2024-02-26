using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    private Vector3 t;
    // Start is called before the first frame update
    void Start()
    {
        t = transform.position;
		StartCoroutine(PickUpFloating());
	}


	private IEnumerator PickUpFloating()
    {
        while (true)
        {
            transform.position = t +  new Vector3(0, Mathf.Sin(Time.time) * 0.5f, 0);
            yield return null;
        }
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        Instantiate(ball, transform.TransformPoint(Vector2.zero), Quaternion.identity);
        transform.parent.gameObject.SetActive(false);
	}
}
