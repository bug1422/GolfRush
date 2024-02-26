using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
	[SerializeField]
	private GameObject Pickup;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			Destroy(collision.gameObject);
			Pickup.SetActive(true);
		}
	}
}
