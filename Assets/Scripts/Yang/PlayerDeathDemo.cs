using UnityEngine;
using System.Collections;

public class PlayerDeathDemo : MonoBehaviour {

	public KeyCode keycode = KeyCode.Delete;
	// Update is called once per frame
	void Update () {
		// kill the player is the keycode is pressed
		if(Input.GetKeyDown(keycode)){
			if(GetComponent<PlayerStatus>() != null){
				GetComponent<PlayerStatus> ().Kill ();
			}
		}
	}
}
