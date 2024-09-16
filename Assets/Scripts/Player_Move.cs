using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
	public float speed;
	SpriteRenderer sp;
	Rigidbody2D rb;
	float L_R;//Left or Right
	float U_D;//Up or Down
	PhotonView view;
	

	void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		view = GetComponent<PhotonView>();
	}

	void Update()
	{
		if (view.IsMine)
		{
			L_R = Input.GetAxisRaw("Horizontal");
			U_D = Input.GetAxisRaw("Vertical");
		}
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2 (L_R, U_D) * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Circle"))
		{
			Destroy(collision.gameObject);
			ScoreManager.score++;
		}
	}
}
