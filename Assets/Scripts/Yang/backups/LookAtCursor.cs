using UnityEngine;
using System.Collections;

public class LookAtCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cursorPosInWord = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 lookDirection = cursorPosInWord - transform.position;
		// set the z to 0f so that the camera can see it
		lookDirection.z = 0f;
		transform.up = lookDirection;
	}
}
