using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(0);
		}
	}
}
