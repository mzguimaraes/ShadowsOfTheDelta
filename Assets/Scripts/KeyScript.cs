using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {

	// Creating public transform variables for both players. These will be used for the key pickup and door unlock codes.
	public Transform Player1Pos;
	public Transform Player2Pos;


	// Creating public instance of one key. Left code for a second and third keys.  Uncomment those lines if they're needed.
	public KeyScript Key1;
	// public KeyScript Key2;
	// public KeyScript Key3;


	// Setting up bools for first player having a key. Left code for a second and third keys.  Uncomment those lines if they're needed.
	public bool Player1HasKey1;
	// bool Player1HasKey2;
	// bool Player1HasKey3;


	// Setting up bools for second player having a key. Left code for a second and third keys.  Uncomment those lines if they're needed.
	public bool Player2HasKey1;
	// bool Player2HasKey2;
	// bool Player2HasKey3;



	// Setting up AudioSources to be triggered when players perform certain actions
	//public AudioSource KeyPickUp;
	//public AudioSource DoorStillLocked;
	//public AudioSource DoorOpening;


	void Start () {

		// Players don't have any keys at the beginning of the game.

		Player1HasKey1 = false;
		// Player1HasKey2 = false;
		// Player1HasKey3 = false;

		Player2HasKey1 = false;
		// Player2HasKey2 = false;
		// Player2HasKey3 = false;

	}
	
	// Update is called once per frame
	void Update () {

		// Code that allows Player 1 or Player 2 to pick up Key1.  The input buttons and distance to key are very subject to change.
		if (((Key1.transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift)) {
			//KeyPickUp.Play ();
			Player1HasKey1 = true;
			Debug.Log ("Player1HasKey1");

		} else if (((Key1.transform.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space)) {
			//KeyPickUp.Play ();
			Player2HasKey1 = true;
			Debug.Log ("Player2HasKey1");
//		} else {
//			Debug.Log ((Key1.transform.position - Player1Pos.position).magnitude);
		}


		// Code that allows Player 1 or Player 2 to pick up Key2.  The input buttons and distance to key are very subject to change. Uncomment if neeeded.
//		if (((Key2.transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift)) {
//			KeyPickUp.Play ();
//			Player1HasKey2 = true;

//		} else if (((Key2.transform.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space)) {
//			KeyPickUp.Play ();
//			Player2HasKey2 = true;
//		}


		// Code that allows Player 1 or Player 2 to pick up Key3.  The input buttons and distance to key are very subject to change. Uncomment if neeeded.
//		if (((Key3.transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift)) {
//			KeyPickUp.Play ();
//			Player1HasKey3 = true;

//		} else if (((Key3.transform.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space)) {
//			KeyPickUp.Play ();
//			Player2HasKey3 = true;
//		}



	}
}
