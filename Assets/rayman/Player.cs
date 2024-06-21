using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	public int health;
	private int jumps;
	private bool hand;
	private Rigidbody2D rig;
	private SpriteRenderer spriteRenderer;
	Animator anim;
	public Collider2D attackCollider;
	
	// Start is called before the first frame update
	void Start()
	{
		rig = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		health = 3;
		attackCollider.enabled = false;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (health == 0) {
			//Destroy(gameObject);
		}

		/*
		float velocity = rig.velocity.y;
		if (velocity < 0f && !anim.GetBool("jumping")) {
			anim.SetBool("falling", true);
		} else if (velocity >= 0f) {
			anim.SetBool("falling", false);
		}
		*/
		Movement();
		Punch();
	}

	void Movement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
		transform.position += movement * Time.deltaTime * speed;

		float axis = Input.GetAxis("Horizontal");
		bool isRunning = axis != 0f;
		anim.SetBool("running", isRunning);
		if (isRunning) {
			transform.eulerAngles = (axis > 0f) ? Vector3.zero : new Vector3(0f, 180f, 0f);
		}

		if (Input.GetButtonDown("Jump") && jumps < 2) {
			rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			anim.SetBool("jumping", true); // todo: use anim.Play to avoid working on animator and cleaner code
			jumps++;
		}
	}

	void Punch()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			anim.Play(hand ? "rightpunch" : "leftpunch");
			hand = !hand;
			StartCoroutine(ActivateAttack());
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("ground")) {
			jumps = 0;
			anim.SetBool("jumping", false);
			anim.SetBool("falling", false);
			//rig.isKinematic = false; todo: tests
		}

		if (collision.gameObject.CompareTag("zombie")) {
			health--;
			StartCoroutine(GotHit());
		}
	}

	IEnumerator ActivateAttack()
	{
		attackCollider.enabled = true;
		yield return new WaitForSeconds(0.3f);
		attackCollider.enabled = false;
	}

	IEnumerator GotHit()
	{
		Color originalColor = spriteRenderer.color;
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.3f);
		spriteRenderer.color = originalColor;
	}
}
