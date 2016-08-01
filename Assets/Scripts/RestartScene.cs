using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour {
	public KeyCode keycode = KeyCode.R;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(keycode)){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}
}
