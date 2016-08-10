using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlScreenStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.J)) {
			SceneManager.LoadScene (2);
			// Debug.Log (SwitchIsOn);
		}

	}
}
