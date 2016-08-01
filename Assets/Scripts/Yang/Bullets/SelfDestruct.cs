// Script by Yang Liu

using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    GameObject playerObject;

	public float destructDistance = 30f;


    void Start() {
        playerObject = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D activator) {
    }

    void Update() {

		// if we cannot find the player, abort
		playerObject = GameObject.FindWithTag("Player");
		if(playerObject == null){
			return;
		}

		// if the bullet is far away from the player,
		// destroy it.
        float distanceFromPlayer = (playerObject.transform.position - transform.position).magnitude;
		if (distanceFromPlayer > destructDistance) {
            Destroy(this.gameObject);
        }
    }
}
