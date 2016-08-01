using UnityEngine;
using System.Collections;

public class EnemyDrone : MonoBehaviour {

//	public CircleCollider2D hearingRadius;
	public float speed = 210f;

	private Transform player;
	public Transform PlayerPointer {
		set {
			player = value;
		}
	}


	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb2d.velocity = Vector2.zero;
		if (player != null) {
			Vector3 moveVector = player.position - transform.position;
			if (moveVector.magnitude > 1f) {
				moveVector.Normalize();
			}
			rb2d.velocity = moveVector * speed * Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy(other.gameObject);
		}
	}
}
