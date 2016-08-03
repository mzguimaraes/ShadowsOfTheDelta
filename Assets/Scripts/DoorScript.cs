using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	// Creating public transform variables for both players. These will be used for the key pickup and door unlock codes.
	public Transform Player1Pos;
	public Transform Player2Pos;

	// Creating public instance of one key. Left code for a second and third keys.  Uncomment those lines if they're needed.
	public DoorScript Door;



	// Setting up bools for doors here. Left code for a second and third doors.  Uncomment those lines if they're needed.
	bool DoorIsUnlocked = false;

	// Creating public instance of key so it can be accessed
	public KeyScript Switch;

	public AudioSource DoorSFX;
	public AudioClip DoorOpening;
	public AudioClip DoorStillLocked;

	void Start () {
		
		Switch.SwitchIsOn = false;

	}

	void Update () {

		// Code that allows Player 1 or Player 2 to Unlock Door 1 after picking up Key 1
		if (Switch.SwitchIsOn == true) {
			Debug.Log ("Door is Open");

		}

	}
}