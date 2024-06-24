using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	public int health;
	public Player player;
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
		for (int i = 1; i < 10; i++) {
			Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.Find("lum" + i).GetComponent<Collider2D>());
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (health == 0) {
			Destroy(gameObject);
		}

		Vector2 direction = (player != null) ? player.transform.position - transform.position : Vector2.zero;
		anim.SetBool("walking", (Mathf.Abs(direction.x) > 1 && rig.velocity.x != 0));
		if (player.health > 0) {
			rig.velocity = new Vector2(direction.normalized.x * 2, rig.velocity.y);
			transform.eulerAngles = (direction.x > 0) ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
		}

		/*
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1, -1);
		if (hit.collider != null) {
			if (Mathf.Abs(rig.velocity.y) < 0.01f) {
				rig.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
			}
		}
		*/
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
