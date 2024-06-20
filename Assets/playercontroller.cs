using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	private float x, y, z;
	private bool doubleJump;
	private int jumps;
	private Rigidbody2D rig;
	private bool hand;
	Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		rig = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		/*
		if (rig.velocity.y < 0f) {
            anim.SetBool("falling", true);
        } else if (rig.velocity.y == 0f)
		{
			anim.SetBool("falling", false);
		} */
		x = transform.position.x;
		y = rig.velocity.y;
		movement();
		punch();
	}

	void movement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
		transform.position += movement * Time.deltaTime * speed;

		float axis = Input.GetAxis("Horizontal");
		bool isRunning = axis != 0f;
		anim.SetBool("running", isRunning);
		if (isRunning) {
			transform.eulerAngles = axis > 0f ? Vector3.zero : new Vector3(0f, 180f, 0f);
		}

		if (Input.GetButtonDown("Jump") && !doubleJump) {
			rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			anim.SetBool("jumping", true);
			jumps++;
			if (jumps == 2) {
				doubleJump = true;
			}
		}
	}

	void punch()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			if (!hand) {
				anim.Play("leftpunch");
			} else {
				anim.Play("rightpunch");
			}
			hand = !hand;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground")) {
			Debug.Log("enter collision");
			doubleJump = false;
			jumps = 0;
			anim.SetBool("jumping", false);
			anim.SetBool("falling", false);
			rig.isKinematic = false;
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			Debug.Log("stay collision");
			rig.isKinematic = false;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			Debug.Log("exit collision");
			rig.isKinematic = false;
		}
	}
}
