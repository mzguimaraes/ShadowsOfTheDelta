using UnityEngine;
using System.Collections;

public class PlayerGoal : MonoBehaviour {

	[HideInInspector] public bool hasFoundGoal = false;

	private bool hasWon = false;

	void Update () {
		if (hasFoundGoal && !hasWon) {
			transform.localScale *= 10000;
			gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			hasWon = true;
		}
	}
}
