using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	public int health;
	public Collider2D attackCollider;
	public TMPro.TextMeshProUGUI textTMP;
	public Sprite winGame;
	public Sprite loseGame;
	private int jumps;
	private bool hand;
	private Rigidbody2D rig;
	private SpriteRenderer spriteRenderer;
	Animator anim;
	private float lastHit;
	private bool gameFinished;
	
	// Start is called before the first frame update
	void Start()
	{
		rig = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		attackCollider.enabled = false;
		health = 3;
	}

	// Update is called once per frame
	void Update()
	{
		if (!gameFinished) {
			Movement();
			Punch();
			CheckEndGame();
		}
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
			anim.SetBool("jumping", true);
			anim.SetBool("falling", true);
			jumps++;
		}
	}

	void Punch()
	{
		if (Input.GetKeyDown(KeyCode.B)) {
			anim.Play(hand ? "rightpunch" : "leftpunch");
			hand = !hand;
			StartCoroutine(ActivateAttack());
		}
	}

	void CheckEndGame()
	{
		GameObject[] lums = GameObject.FindGameObjectsWithTag("lum");
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombie");

		if (lums.Length == 0 && zombies.Length == 0) {
			StopPlayer(winGame);
		}

		if (health == 0) {
			StopPlayer(loseGame);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("obstacle")) {
			jumps = 0;
			anim.SetBool("jumping", false);
			anim.SetBool("falling", false);
		}

		if (collision.gameObject.CompareTag("lum")) {
			textTMP.text = (int.Parse(textTMP.text) + 1).ToString();
			Destroy(collision.gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("zombie") && Time.time - lastHit >= 2f) {
			if (health > 0) {
				health--;
				StartCoroutine(GotHit());
				lastHit = Time.time;
			}
		}
	}

	void StopPlayer(Sprite sprite)
	{
		anim.enabled = false;
		spriteRenderer.sprite = sprite;
		gameFinished = true;
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
