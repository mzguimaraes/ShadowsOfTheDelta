// Script by Yang Liu

using UnityEngine;
using System.Collections;

public class SeerMove: MonoBehaviour {

    public float moveSpeed = 5.0f;

    Vector2 moveVector;
    Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveVector = new Vector2(horizontal, vertical);

        // "normalization"
        //moveVector /= moveVector.magnitude;     // manual normalization
        //moveVector = moveVector.normalized;     // better way of normalization ("normalized is a property")
        if (moveVector.magnitude > 1f) {
            moveVector.Normalize();               // alternate better way of normalization
        }
	}

    void FixedUpdate() {
        myRigidbody.velocity = moveVector * moveSpeed;
        // myRigidbody.velocity = new Vector2(0f, 0f);
        // myRigidbody.velocity = Vector2.zero; // shortcut for the line above

    }
}
