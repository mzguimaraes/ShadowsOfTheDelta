using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	// Creating public transform variables for both players. These will be used for the key pickup and door unlock codes.
	public Transform Player1Pos;
	public Transform Player2Pos;

	// Creating public instance of one key. Left code for a second and third keys.  Uncomment those lines if they're needed.
	public DoorScript Door1;
	//public DoorScript Door2;
	//public DoorScript Door3;


	// Setting up bools for doors here. Left code for a second and third doors.  Uncomment those lines if they're needed.
	bool Door1IsUnlocked;
	// bool Door2IsUnlocked;
	// bool Door3IsUnlocked;

	// Creating public instance of key so it can be accessed
	public KeyScript Key1;
	// public KeyScript Key2;
	// public KeyScript Key3;


	void Start () {
	
		// Players haven't unlocked any doors at the beginning of the game.
		Door1IsUnlocked = false;
		// Door2IsUnlocked = false;
		// Door3IsUnlocked = false;

	}

	void Update () {

		// Code that allows Player 1 or Player 2 to Unlock Door 1 after picking up Key 1
		if (((Door1.transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Key1.Player1HasKey1 == true) {
			//DoorOpening.Play ();
			Door1IsUnlocked = true;
			Debug.Log ("Player 1 Unlocked Door 1");

		} else if (((Door1.transform.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space) && Key1.Player2HasKey1 == true) {

			//DoorOpening.Play ();
			Door1IsUnlocked = true;
			Debug.Log ("Player 2 Unlocked Door 2");

		}

		if (((Door1.transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Key1.Player1HasKey1 == false){

			//DoorStillLocked.Play ();
			Debug.Log ("Door is still locked");
		}


		// Code that allows Player 1 or Player 2 to unlock Door 2 after picking up Key 2
		//		if (((Door2.transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Player1HasKey2 == true) {
		//			DoorOpening.Play ();
		//			Door2IsUnlocked = true;
		//
		//		} else if (((Door2.transform.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space) && Player2HasKey2 == true) {
		//
		//			DoorOpening.Play ();
		//			Door2IsUnlocked = true;
		//		} else {
		//
		//			DoorStillLocked.Play ();
		//		}


		// Code that allows Player 1 or Player 2 to unlock Door 3 after picking up Key 3
		//		if (((Door3.transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && Player1HasKey3 == true) {
		//			DoorOpening.Play ();
		//			Door3IsUnlocked = true;
		//
		//		} else if (((Door3.transform.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space) && Player2HasKey3 == true) {
		//
		//			DoorOpening.Play ();
		//			Door3IsUnlocked = true;
		//		} else {
		//
		//			DoorStillLocked.Play ();
		//		}


	}
}
