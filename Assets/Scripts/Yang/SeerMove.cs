// Script by Yang Liu

using UnityEngine;
using System.Collections;

public class SeerMove: MonoBehaviour {

    public float moveSpeed = 5.0f;
	public float jumpForce = 5.0f;

	public float raycastLength = 0.1f;

	//public Transform playerFeet;

    Vector2 moveVector;
    Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		SeerJump ();


		float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        // float vertical = Input.GetAxis("Vertical");
		float vertical = myRigidbody.velocity.y;
        moveVector = new Vector2(horizontal, vertical);

		myRigidbody.velocity = moveVector;


	}

    void FixedUpdate() {
		



    }

	void SeerJump(){
		// shoot a short raycast to the ground
		RaycastHit2D raycast = Physics2D.Raycast (transform.position, -Vector3.up,
			                       raycastLength);
		if(raycast.collider != null){
			if (Input.GetKeyDown (KeyCode.UpArrow)){
				myRigidbody.AddForce(new Vector2(0f, jumpForce));
			}
		}
	}
}
