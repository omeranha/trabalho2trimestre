using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lum : MonoBehaviour
{
	public TMPro.TextMeshProUGUI textTMP;
	public GameObject lumUI;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player")) {
			textTMP.text = (int.Parse(textTMP.text) + 1).ToString();
			gameObject.GetComponent<Collider2D>().enabled = false;
			StartCoroutine(MoveToUI());
		}

		IEnumerator MoveToUI()
		{
			Vector3 target = lumUI.transform.position;
			Vector3 direction = (target - transform.position).normalized;
			while (Vector3.Distance(transform.position, target) > 0.1f) {
				transform.position += direction * 30 * Time.deltaTime;
				yield return null;
			}
			Destroy(gameObject);
		}
	}
}
