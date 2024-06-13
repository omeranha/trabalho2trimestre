using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
	public float speed = 3;
	public float jumpForce = 5;
	private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
		rig = GetComponent<Rigidbody2D>();
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

		if (Input.GetButtonDown("Jump"))
		{
			rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}
	}
}
