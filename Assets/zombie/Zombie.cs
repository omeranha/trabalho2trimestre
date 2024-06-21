using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	public int health;
	public Transform player;
	private Rigidbody2D rig;
	private SpriteRenderer spriteRenderer;
	Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		health = 3;
	}

	// Update is called once per frame
	void Update()
	{
		if (health == 0) {
			Destroy(gameObject);
		}

		if (!player) {
			return;
		}

		Vector2 direction = player.position - transform.position;
		rig.velocity = new Vector2(direction.normalized.x * 2, rig.velocity.y);
		transform.eulerAngles = (direction.x > 0) ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
		anim.SetBool("walking", (Mathf.Abs(direction.x) > 1 && rig.velocity.x != 0));
	}

	public void Hit() {
		StartCoroutine(GotHit());
		health--;
	}

	IEnumerator GotHit()
	{
		Color originalColor = spriteRenderer.color;
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.3f);
		spriteRenderer.color = originalColor;
	}
}
