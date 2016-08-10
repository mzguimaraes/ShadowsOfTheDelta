/*
 * Author: Brett Moody @bam4 and Mike Thal @mikeathal
 */



using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchFunctions : MonoBehaviour {

	// Creating public transform variables for both players. 
	public Transform Player1Pos;
	public Transform Player2Pos;

	SpriteRenderer DoorSprite; // This makes the sprite renderer for this lever available.
	public Sprite blueLever; // This is the sprite that represents the ON position.
	public Sprite redLever; // This is the sprite that represents the OFF position.

	public bool SwitchIsOn = false; // Setting the switch to false intially. Doors are THERE when switches are OFF.
	public SwitchFunctions otherSwitch;  // Getting access to the switch's twin.

	public Text playerOneInstruction; // These text boxes tell the player which buttons to press.
	public Text playerTwoInstruction;
	public GameObject playerOnePanel; // These panels contain the players' intstructions.
	public GameObject playerTwoPanel;

	// Setting up AudioSources to be triggered when players perform certain actions
	public AudioSource SwitchSFX;
	public AudioClip TurnSwitchOn;
	public AudioClip TurnSwitchOff;

	public bool switchColor;


	void Start () {
		SwitchIsOn = false; //I believe this is redundant.
		DoorSprite = GetComponent<SpriteRenderer> ();
	}


	void FlipSwitch () {
		SwitchIsOn = !SwitchIsOn;

		if (SwitchIsOn) {
			SwitchSFX.PlayOneShot (TurnSwitchOn);
		} else {
			SwitchSFX.PlayOneShot (TurnSwitchOff);
		}


		otherSwitch.SwitchIsOn = SwitchIsOn;
		switchColor = true;
		otherSwitch.switchColor = true;
//		
	}



	// Update is called once per frame
	void Update () {
			

		if (switchColor) {
			if (SwitchIsOn == true) {
				DoorSprite.sprite = blueLever;
			} else if (SwitchIsOn == false) {
				DoorSprite.sprite = redLever;
			}
			switchColor = false;
		}
		
		

		// Code that allows Player 1 to flip the switch.
		if ((transform.position - Player1Pos.position).magnitude < 2.5f) {
			playerOnePanel.SetActive (true);
			playerOneInstruction.text = "Press RIGHT SHIFT to flip the switch!";
			if (Input.GetKeyDown (KeyCode.RightShift)) {
				FlipSwitch();
			// Debug.Log (SwitchIsOn);
			}

		} 
//			else if (((transform.position - Player1Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.RightShift) && (SwitchIsOn == true)) {
////
////			SwitchSFX.PlayOneShot (KeyPickup);
////			SwitchIsOn = false;
////			Debug.Log (SwitchIsOn);
////		}


		if (((transform.position - Player2Pos.position).magnitude < 2.5f)) {

			playerTwoPanel.SetActive (true);
			playerTwoInstruction.text = "Press SPACE to flip the switch!";
			if (Input.GetKeyDown (KeyCode.Space)) {
				FlipSwitch ();
			}

		}
//			else if (((transform.position - Player2Pos.position).magnitude < 2.5f) && Input.GetKeyDown (KeyCode.Space) && (SwitchIsOn = true)) {
//
//			SwitchSFX.PlayOneShot (KeyPickup);
//			SwitchIsOn = false;
//			Debug.Log ("Switch is Off");
//		}
	}
}
