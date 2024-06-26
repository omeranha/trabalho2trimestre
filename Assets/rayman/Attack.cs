using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("zombie")) {
			Zombie zombie = collision.gameObject.GetComponent<Zombie>();
			if (zombie != null) {
				zombie.Hit();
			}
		}
	}
}
