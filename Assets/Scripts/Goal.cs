using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerGoal>().hasFoundGoal = true;
		}
	}
}
