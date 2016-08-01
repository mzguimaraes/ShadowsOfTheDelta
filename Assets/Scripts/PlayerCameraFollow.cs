using UnityEngine;
using System.Collections;

public class PlayerCameraFollow : MonoBehaviour {

	public Vector3 offset = Vector3.zero;

	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = transform.position + offset;
	}
}
