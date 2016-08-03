// Script by Yang Liu

using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour {

    public float moveSpeed = 50f;

	public string horizontalAxisName = "Horizontal";
	public string verticalAxisName = "Vertical";

	//public Transform playerFeet;

    Vector2 moveVector;
    Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis (horizontalAxisName) * moveSpeed;
		float vertical = Input.GetAxis(verticalAxisName) * moveSpeed;
        moveVector = new Vector2(horizontal, vertical);
		if(moveVector.magnitude > 1f){
			moveVector.Normalize ();
		}


	}

    void FixedUpdate() {
		myRigidbody.velocity = moveVector * moveSpeed * Time.deltaTime * 10f;
    }

}
