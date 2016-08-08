using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	//TODO: make this do damage
	//TODO: make this work

	public float speed = 5f;
	public float timeToLive = 3f;

	private Rigidbody2D rb2d;

	void Start() {
		Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
		Vector2 toPlayer = playerPos.position - transform.position;
		toPlayer.Normalize();
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity += toPlayer * speed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if(GetComponent<PlayerStatus>() != null){
				GetComponent<PlayerStatus> ().Kill ();
			}
			//do damage (once implemented TODO)
			// Destroy(this.gameObject);
		}
		else if (other.tag != "Enemy") {
			Destroy(this.gameObject);
		}
	}

	void Update() {
		if (timeToLive > 0f) {
			timeToLive -= Time.deltaTime;
		}
		else {
			Destroy(this.gameObject);
		}
	}
}
