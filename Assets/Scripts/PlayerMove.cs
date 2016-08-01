/*
 * Author: Marcus Guimaraes
 */


using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float speed = 50f;

	private Rigidbody2D rb2d;
	private Vector2 moveVector;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		moveVector = Vector2.zero;

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		moveVector = new Vector2(horizontal, vertical);

		if (moveVector.magnitude > 1f) {
			moveVector.Normalize();
		}

		rb2d.velocity = moveVector * speed * Time.deltaTime;

	}
		
}
