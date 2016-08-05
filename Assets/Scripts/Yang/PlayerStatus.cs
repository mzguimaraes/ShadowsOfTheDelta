using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	// the deadSprite which will replace the current sprite if the player is killed
	public Sprite deadSprite;
	// set this to the object you assign the DeathHandler script on 
	public GameObject deathHandler;
	// the behaviour of the player if she dies,
	// GOTHOUGH: the player who is still alive will not collide with the dead player
	// IMMOVABLE: the player will collide with the dead player, which is immovable
	public enum DeadBehaviour {GOTHROUGH, IMMOVABLE};
	public DeadBehaviour deadBehaviour = DeadBehaviour.GOTHROUGH;

	bool isDead = false;
	Rigidbody2D playerRigidbody;

	void Start(){
		playerRigidbody = GetComponent<Rigidbody2D> ();
	}

	// call this function if you want to know whether this player is dead
	public bool IsDead(){
		return isDead;

	}

	public void Kill(){
		// we don't need to kill the player if the player is already dead
		if(isDead){
			return;
		}

		// we will abort this script if no sceneController is assigned
		if(deathHandler == null){
			Debug.Log ("KillPlayer.Kill(): no sceneController is assigned.");
			return;
		}


		// now the player is literally dead
		isDead = true;
		// change the Sprite to deadSprite
		if (deadSprite != null) {
			GetComponent<SpriteRenderer> ().sprite = deadSprite;
			// set the sortingOrder to negative 1 to let it under the player
			GetComponent<SpriteRenderer> ().sortingOrder = -1;
		}
		// disable child objects
		foreach(Transform child in transform){
			child.gameObject.SetActive (false);
		}
		// disable all the other components except SpriteRenderer and CircleCollider2D
		MonoBehaviour[] components = GetComponents<MonoBehaviour> ();
		foreach(MonoBehaviour comp in components){
			comp.enabled = false;
		}
		// add other components below if you want them to be enabled after the player is dead
		GetComponent<SpriteRenderer>().enabled = true;


		// set the Rigidbody2D 
		playerRigidbody.isKinematic = true;


		if (deadBehaviour == DeadBehaviour.IMMOVABLE) {
			// if the behaviour is IMMOVABLE, enable the circle collider
			GetComponent<CircleCollider2D> ().enabled = true;
		} else if (deadBehaviour == DeadBehaviour.GOTHROUGH){
			// if the behaviour is GOTHROUGH, disable the circle collider
			GetComponent<CircleCollider2D> ().enabled = false;
		}


		// tell PlayerDeathHandler that a player has died
		deathHandler.GetComponent<DeathHandler> ().PlayerDied ();
	}
}
