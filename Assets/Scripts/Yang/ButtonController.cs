using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;		// for SceneManager

public class ButtonController : MonoBehaviour {


	IEnumerator AudioWait() {

		yield return new WaitForSecondsRealtime(2);

	}

	// Load the specific scene using scene index number
	// -1 for loading the current scene
	public void LoadScene(int sceneNumber = -1){
		if(sceneNumber == -1){
			return;

		}

		SceneManager.LoadScene (sceneNumber);
	}



}
