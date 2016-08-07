﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// Creating public transform variables for both players. These will be used for the key pickup and door unlock codes.
	public Transform Player1Pos;  
	public Transform Player2Pos;

	// Creating public instance of one key. Left code for a second and third keys.  Uncomment those lines if they're needed.

	SpriteRenderer DoorSprite;
	Collider2D DoorCollider;

	// Setting up bools for doors here. Left code for a second and third doors.  Uncomment those lines if they're needed.
	bool DoorIsUnlocked = false;

	// Creating public instance of key so it can be accessed
	public SwitchFunctions Switch;

	public AudioSource DoorSFX;
	public AudioClip DoorOpening;
	public AudioClip DoorStillLocked;

	void Start () {
		
		Switch.SwitchIsOn = false;
		DoorSprite = GetComponent<SpriteRenderer> ();
		DoorCollider = GetComponent<Collider2D> ();

	}

	void Update () {
		//Note from AGK: Rather than checking every frame is Switch.SwitchIsOn is true, you might consider a
		//public function that the Switch itself calls? Depends on the setup but that might be easier.
		

		// Code that allows Player 1 or Player 2 to Unlock Door 1 after picking up Key 1
		if (Switch.SwitchIsOn == true) {

			DoorSprite.enabled = false;
			DoorCollider.enabled = false;

		} else if (Switch.SwitchIsOn == false) {

			DoorSprite.enabled = true;
			DoorCollider.enabled = true;

		}

	}
}
