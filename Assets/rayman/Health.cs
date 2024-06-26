using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public Player player;
	private SpriteRenderer spriteRenderer;
	public Sprite[] hearts;

	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		int health = player.health;
		spriteRenderer.sprite = hearts[health];
	}
}
