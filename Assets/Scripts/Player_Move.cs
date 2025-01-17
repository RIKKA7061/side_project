using Photon.Pun;
using TMPro;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
	public float speed;
	private TMP_InputField inputField;  // public에서 private로 변경
	SpriteRenderer sp;
	Rigidbody2D rb;
	float L_R; // Left or Right
	float U_D; // Up or Down
	PhotonView view;

	void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		view = GetComponent<PhotonView>();

		// PhotonView가 소유하는 객체만 inputField를 설정하도록 처리
		if (view.IsMine)  // 로컬 플레이어만 inputField 설정
		{
			inputField = FindObjectOfType<TMP_InputField>(); // 씬에서 첫 번째 TMP_InputField를 찾음
		}
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
		rb.velocity = new Vector2(L_R, U_D) * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Circle"))
		{
			Destroy(collision.gameObject);
		}
	}

	public void Send()
	{
		if (inputField != null)
		{
			string send = inputField.text;
			ScoreManager.score = send;
			Debug.Log(send);
		}
	}
}
