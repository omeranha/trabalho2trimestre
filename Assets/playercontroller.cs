using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	private Rigidbody2D rig;
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
		movement();
    }

	void movement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
		transform.position += movement * Time.deltaTime * speed;

		if (Input.GetAxis("Horizontal") > 0f)
		{
			anim.SetBool("running", true);
			transform.eulerAngles = new Vector3(0f, 0f, 0f);
		}

		if (Input.GetAxis("Horizontal") < 0f)
		{
			anim.SetBool("running", true);
			transform.eulerAngles = new Vector3(0f, 180f, 0f);
		}

		if (Input.GetAxis("Horizontal") == 0f)
		{
			anim.SetBool("running", false);
		}

		if (Input.GetButtonDown("Jump"))
		{
			rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}
	}
}
