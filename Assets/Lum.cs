using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lum : MonoBehaviour
{
	public TMPro.TextMeshProUGUI textTMP;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player")) {
			textTMP.text = (int.Parse(textTMP.text) + 1).ToString();
			Destroy(gameObject);
		}
	}
}
