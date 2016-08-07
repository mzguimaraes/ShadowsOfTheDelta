﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitDoorBrett : MonoBehaviour {

	public Transform exitDoor; // Tracks the position of the exit door.
	public GameObject playerOne; // Tracks the position of player one's avatar.
	public GameObject playerTwo; // Tracks the position of player two's avatar.

	//public GameObject closedExitDoorSprite; // This is the sprite that represents the closed door before it has been opened.
	//public GameObject openedExitDoorSprite; // This is the sprite that represents the open door after it has been opened.

	public GameObject alarm; // This is the GameObject which contains the alarm timer.

	public Text playerOneInstruction; // This is the text that tells player one which button to press to escape the level.
	public Text playerTwoInstruction; // This is the text that tells player two which button to press to escape the level.
	public GameObject playerOnePanel; // This is the panel behind the player one help box.
	public GameObject playerTwoPanel; // This is the panel behind the player two help box.
	public GameObject breakoutPanel; // This is the panel behind the breakout text box!



	public float newCounterTime = 30; // This is the counter time that is assigned when one player escapes the level.

	// public Text playerEscapedText; // This is the text that shows when either player escapes the level.  It says, "Player [#] escaped!"

	public bool hasPlayerEscaped = false; // This boolean tracks whether a player has escaped the level yet.

	// Setting up AudioSources to be triggered when players perform certain actions
	public AudioSource ExitDoorSource;
	public AudioClip exitDoorOpenSFX;

	float textTimeEnd = 1000;  // This variable tracks how long it takes for the "Completed Level Screen" to load.





	// Use this for initialization
	void Start () {
		breakoutPanel.SetActive (false);

	}

	void EndGame () {
		ExitDoorSource.PlayOneShot (exitDoorOpenSFX); // Play the SFX of the door opening.
		if (hasPlayerEscaped == false) { // If no one else has escaped...
//			closedExitDoorSprite.SetActive (false); // Turn off Closed door sprite
//			openedExitDoorSprite.SetActive (true); // Turn on Open door sprite
			// alarm.GetComponent<AlarmTimerBrett>();
			AlarmTimerBrett.timeLeft = newCounterTime; // Set the time left variable to the value of newCounterTime, which is thirty seconds.
			hasPlayerEscaped = true;
		} else {
			//playerEscapedText.text = "Breakout!";  // Display "Breakout" in the center of the screen.
			textTimeEnd = 5;
			breakoutPanel.SetActive(true);

		}

	}



	// Update is called once per frame
	void Update () {
		playerOneInstruction.text = ""; // If the player is not near any door, this text will not come up at all.
		playerTwoInstruction.text = ""; // If the player is not near any door, this text will not come up at all.
		playerOnePanel.SetActive(false);
		playerTwoPanel.SetActive(false);
		textTimeEnd -= Time.deltaTime; // Countdown from the timer.


		if ((exitDoor.position - playerOne.transform.position).magnitude < 2.5f && playerOne.activeInHierarchy)  { // If player one is within a few feet of the exit door, show him the following text.
			playerOneInstruction.text = "Press [RIGHT SHIFT] to escape!";
			playerOnePanel.SetActive (true);
			if (Input.GetKeyDown(KeyCode.RightShift)) { // If he presses right shift,
				playerOne.SetActive(false); // Disable his character from the scene.
				EndGame();
			}
				
		}

		if ((exitDoor.position - playerTwo.transform.position).magnitude < 2.5f && playerTwo.activeInHierarchy)  { // If player two is within a few feet of the exit door, show him the following text.
			playerTwoInstruction.text = "Press [SPACE] to escape!";
			playerTwoPanel.SetActive (true);
			if (Input.GetKeyDown(KeyCode.Space)) { // If he presses space,
				playerTwo.SetActive(false); // Disable his character from the scene.
				EndGame();

			}
		}

		if (textTimeEnd <= 0) { // When their is no time left in the timer,
			SceneManager.LoadScene ("CompletedLevelScreenBrett"); // After the "Breakout!" text has shown for five seconds, go to the completed level screen.
		}

	}
}

