using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {
	public float moveSpeed = 50.0f;
	public string targetTag = "Player";

	Rigidbody2D followerRigidbody;
	// Use this for initialization
	void Start () {
		followerRigidbody = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		// try to find the target object using the tag
		// if is null, abort the script
		GameObject targetGO = GameObject.FindWithTag(targetTag);
		if(targetGO == null){
			return;
		}

		Vector3 followDirection = targetGO.transform.position - this.transform.position;
		followDirection.Normalize ();
		followerRigidbody.velocity = followDirection * Time.deltaTime * moveSpeed;

	}
}
