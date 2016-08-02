using UnityEngine;
using System.Collections;

public class KeysAndDoors : MonoBehaviour {

	// Creating public transform variables for both players, keys, and doors. These will be used for the key pickup and door unlock codes.
	public Transform Player1Pos;
	public Transform Player2Pos;

	public Transform Key1Pos;
	// public Transform Key2Pos;
	// public Transform Key3Pos;

	public Transform Door1Pos;
	// public Transform Door2Pos;
	// public Transform Door3Pos;


	// Setting up bools for first player having a key. Left code for a second and third keys.  Uncomment those lines if they're needed.

	bool Player1HasKey1;
	// bool Player1HasKey2;
	// bool Player1HasKey3;


	// Setting up bools for second player having a key. Left code for a second and third keys.  Uncomment those lines if they're needed.

	bool Player2HasKey1;
	// bool Player2HasKey2;
	// bool Player2HasKey3;


	// Setting up bools for doors here. Left code for a second and third doors.  Uncomment those lines if they're needed.

	bool Door1IsUnlocked;
	// bool Door2IsUnlocked;
	// bool Door3IsUnlocked;


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



		// Players haven't unlocked any doors at the beginning of the game.

		Door1IsUnlocked = false;
		// Door2IsUnlocked = false;
		// Door3IsUnlocked = false;
	}
	
	// Update is called once per frame
	void Update () {

		// Code that allows Player 1 or Player 2 to pick up Key1.  The input buttons and distance to key are very subject to change.
		if (((Key1Pos.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift)) {
			//KeyPickUp.Play ();
			Player1HasKey1 = true;
			Debug.Log ("Player1HasKey1");

		} else if (((Key1Pos.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space)) {
			//KeyPickUp.Play ();
			Player2HasKey1 = true;
			Debug.Log ("Player2HasKey1");
		}


		// Code that allows Player 1 or Player 2 to pick up Key2.  The input buttons and distance to key are very subject to change. Uncomment if neeeded.
//		if (((Key2Pos.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift)) {
//			KeyPickUp.PlayOneShot ();
//			Player1HasKey2 = true;

//		} else if (((Key2Pos.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space)) {
//			KeyPickUp.PlayOneShot ();
//			Player2HasKey2 = true;
//		}


		// Code that allows Player 1 or Player 2 to pick up Key3.  The input buttons and distance to key are very subject to change. Uncomment if neeeded.
//		if (((Key3Pos.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift)) {
//			KeyPickUp.PlayOneShot ();
//			Player1HasKey3 = true;

//		} else if (((Key3Pos.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space)) {
//			KeyPickUp.PlayOneShot ();
//			Player2HasKey3 = true;
//		}


		// Code that allows Player 1 or Player 2 to Unlock Door 1 after picking up Key 1
		if (((Door1Pos.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Player1HasKey1 == true) {
			//DoorOpening.Play ();
			Door1IsUnlocked = true;
			Debug.Log ("Player 1 Unlocked Door 1");

		} else if (((Door1Pos.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space) && Player2HasKey1 == true) {

			//DoorOpening.Play ();
			Door1IsUnlocked = true;
			Debug.Log ("Player 2 Unlocked Door 2");

		}

		if (((Door1Pos.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Player1HasKey1 == false){
			
			//DoorStillLocked.Play ();
			Debug.Log ("Door is still locked");
		}


		// Code that allows Player 1 or Player 2 to unlock Door 2 after picking up Key 2
//		if (((Door2Pos.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Player1HasKey2 == true) {
//			DoorOpening.PlayOneShot ();
//			Door2IsUnlocked = true;
//
//		} else if (((Door2Pos.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space) && Player2HasKey2 == true) {
//
//			DoorOpening.PlayOneShot ();
//			Door2IsUnlocked = true;
//		} else {
//
//			DoorStillLocked.PlayOneShot ();
//		}


		// Code that allows Player 1 or Player 2 to unlock Door 3 after picking up Key 3
//		if (((Door3Pos.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Player1HasKey3 == true) {
//			DoorOpening.PlayOneShot ();
//			Door3IsUnlocked = true;
//
//		} else if (((Door3Pos.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space) && Player2HasKey3 == true) {
//
//			DoorOpening.PlayOneShot ();
//			Door3IsUnlocked = true;
//		} else {
//
//			DoorStillLocked.PlayOneShot ();
//		}

	}
}
