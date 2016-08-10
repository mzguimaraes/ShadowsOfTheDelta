/*
 * Author: Marcus Guimaraes @mzguimaraes
 */

using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.Audio;

public class GuardBot_AI_v2 : MonoBehaviour {

	//sound: guardNotice
	private AudioSource audioSource;

	//for movement with physics
	//private Rigidbody2D myRb2d;
	//private Vector2 moveForce;

	
	//for patrolling
	public PatrolPath patrol;
	public float speed = 2f;
	private Transform patrolDestination;
	private int patrolDestinationIndex = 1;
	private bool isCircular = false;
	private bool isTravelingBackwards = false;

	//for player interaction
	private Vector3 lastKnownPosition;
	public float playerChaseTime = 3f;
	private float playerChaseCountDown;
	public float chaseSpeed = 4f;
	private bool isChasing = false;
	private bool isPatrolling = true;
	private Stack<Vector3> visited = new Stack<Vector3>(); //locations visited since patrol last left

	private float arrivalDistance = 0.1f; //radius in which GuardBot is "at" a location
	private bool canMove = true;

	public GameObject flashlight;
	private float canMoveRetryTimerMax = 1f;
	private float canMoveRetryTimer = 1f;

	private void returnToPatrol() {
		//if patrolDestination in sight, move there
		RaycastHit2D lookToPatrol = Physics2D.Raycast(transform.position, 
			patrolDestination.position - transform.position, 
			(patrolDestination.position - transform.position).magnitude);
		//Debug.Log(lookToPatrol.collider != null ? lookToPatrol.collider.ToString() : "null");

		//patrolDestination in sight as long as raycast hit nothing (since length is dynamic)
		if (lookToPatrol.collider == null) {
			//return to patrol
			visited.Clear();
			isPatrolling = true;
			canMove = true; //bit of a hack
		}
		//if patrolDestination not in sight, move to last location visited
		else {
			if (visited.Count > 0){//move to last visited location
				rotateTowards(visited.Peek());
				Vector3 moveVector = visited.Peek() - transform.position;

				if (moveVector.magnitude <= arrivalDistance) { //we've reached the destination
					visited.Pop();
					return; //may hang for a frame but that's ok TODO: fix this if time
				}

				moveVector.Normalize();

				transform.position += moveVector * speed * Time.deltaTime;
//				moveForce = new Vector2(moveVector.x, moveVector.y);
			}
			else {
				isPatrolling = true;
			}
		}
	}
		
	//move along patrol path
	private void moveAlongPatrolPath() {
		Vector3 moveVector = patrolDestination.position - transform.position;
		moveVector.Normalize();

		transform.position += moveVector * speed * Time.deltaTime;
//		moveForce = new Vector2(moveVector.x, moveVector.y);
	}

	//move along circular path too
	//reverse direction at end of path
	private void checkPatrol() {

		RaycastHit2D rch2d = Physics2D.Raycast(transform.position, transform.up, 
			arrivalDistance * 4f);
		if (rch2d.collider != null) { //something in the way
			if (rch2d.collider.tag == "door") {
				//switch directions
				if (!isTravelingBackwards) {
					isTravelingBackwards = true;
					patrolDestination = patrol.path[patrolDestinationIndex --].transform;
				}
				else {
					isTravelingBackwards = false;
					patrolDestination = patrol.path[patrolDestinationIndex ++].transform;
				}
			}
		}

		if ((transform.position - patrolDestination.position).magnitude <= arrivalDistance ) { //reached destination, find new one

			//transform.position = patrolDestination.position; //teleport to exact destination (to keep path predictable)

			if (!isTravelingBackwards) {
				if (patrolDestinationIndex + 1 < patrol.path.Count) { //if there's another node ahead, use it
					patrolDestination = patrol.path[patrolDestinationIndex ++].transform;
				}
				else if (isCircular) { //path is circular, so go back to the first node
					patrolDestination = patrol.path[0].transform;
					patrolDestinationIndex = 0;
				}
				else { //path is non-circular and guard has reached the end--go back
					isTravelingBackwards = true;
					patrolDestination = patrol.path[patrolDestinationIndex --].transform;
				}
			}
			else { //is travelling backwards--same logic as above but reversed
				if (patrolDestinationIndex - 1 >= 0) {
					patrolDestination = patrol.path[patrolDestinationIndex --].transform;
				}
				else if (isCircular) { //this shouldn't ever happen, but just in case
					patrolDestination = patrol.path[0].transform;
					patrolDestinationIndex = 0;
				}
				else {
					isTravelingBackwards = false;
					patrolDestination = patrol.path[patrolDestinationIndex ++].transform;
				}
			}
		}
	}

	//look for the player
	private Transform findPlayer() {
		RaycastHit2D rch2d = Physics2D.Raycast(transform.position, transform.up);
		if (rch2d.collider != null) {
			if (rch2d.collider.tag == "Player") {
				lastKnownPosition = rch2d.collider.gameObject.transform.position;
				playerChaseCountDown = playerChaseTime;
				canMove = true;
				//audioSource.PlayOneShot(audioSource.clip);
				return rch2d.collider.transform;
			}
		}
		return null;
	}

	//when it sees the player, run towards it
	private void moveToPlayer(Vector3 player) {

		RaycastHit2D rch2d = Physics2D.Raycast(transform.position, transform.up, arrivalDistance * 4f);
		if (rch2d.collider != null) {
			if (rch2d.collider.tag == "door") {
				return; //don't move
			}
		}

		Vector3 moveVector = player - transform.position;
		moveVector.Normalize();
		transform.position += moveVector * chaseSpeed * Time.deltaTime;
		//moveForce = new Vector2(moveVector.x, moveVector.y);
	}

	//when line of sight broken with player, move to last seen location and look around
	private void moveToLastKnown(Vector3 target) {
		if ( (target - transform.position).magnitude > arrivalDistance ) { //move closer
			moveToPlayer(target);
		}
		else { //look around
			if (visited.Count > 0) {
				if (!visited.Peek().Equals(target)) { //add position to visited if not already there
				visited.Push(target);
				}
			}
			else { //visited.Count is empty
				visited.Push(target);
			}
			transform.Rotate(0f, 0f, 300f * Time.deltaTime);
		}
	}

	//rotate in direction of movement
	private void rotateTowards(Vector3 target) {
		Vector3 lookVector = target - transform.position;
		transform.up = lookVector;
	}

	//catch the player and kill them
	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player") {
			other.gameObject.GetComponent<PlayerStatus>().Kill();
			isChasing = false;
		}
		else if (other.collider.tag == "door") {
			canMove = false;
		}
		canMoveRetryTimer = canMoveRetryTimerMax;
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.collider.tag == "door" ) {
			if (!other.collider.enabled) {
				canMove = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "door") {
			canMove = true;
		}
	}

	//scales the flashlight image to stop at walls
	private void scaleFlashlight() {
		RaycastHit2D rch2d = Physics2D.Raycast(flashlight.transform.position, transform.up);
		if (rch2d.collider != null) {
			flashlight.transform.localScale = new Vector3(1f, rch2d.distance, 1f);
		}
	}

	// Use this for initialization
	void Start () {
		if( patrol.path[patrol.path.Count - 1].Equals( patrol.path[0] ) ) //first and last nodes are equal--path is circular
			isCircular = true;
		
		if (patrol.path[1] != null) //if there is a second node in the path, set the destination to it
			patrolDestination = patrol.path[1].transform;
		else {
			patrolDestination = patrol.path[0].transform;
		}
		
		if (patrol.path[0] != null)
			transform.position = patrol.path[0].transform.position;

		//myRb2d = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//scaleFlashlight();
		//stop looking after a certain amount of time has elapsed
		if (playerChaseCountDown <= 0f) {
			isChasing = false;
		}

		if (canMove){
			Transform player = findPlayer();
			if (isChasing) {
				moveToLastKnown(lastKnownPosition);
				playerChaseCountDown -= Time.deltaTime;
			}
			else if (!isPatrolling) {
				returnToPatrol();
			}
			else if (player == null) { //no player -- normal patrol
				rotateTowards(patrolDestination.position);
				moveAlongPatrolPath();
				checkPatrol();
			}
			else { //sees player and will chase
				isChasing = true;
				isPatrolling = false;
				canMove = true; //bit of a hack
				audioSource.PlayOneShot(audioSource.clip);
				rotateTowards(player.position);
				moveToPlayer(player.position);

			}
		}
		else { //!canMove
			canMoveRetryTimer -= Time.deltaTime;
			if (canMoveRetryTimer <= 0f) {
				RaycastHit2D rch2d = Physics2D.Raycast(transform.position, transform.up);
				if (rch2d.collider != null) {
					if (rch2d.collider.tag != "door") {
						canMove = true;
					}
					else {
						canMoveRetryTimer = canMoveRetryTimerMax;
					}
				}
			}	
		}
	}

//	void FixedUpdate() {
//		moveForce *= speed * Time.deltaTime;
//		myRb2d.velocity = moveForce;
//	}

	void OnDrawGizmos() {
		RaycastHit2D rch2d = Physics2D.Raycast(transform.position, transform.up);
		Gizmos.color = Color.white;
		if (rch2d.point != Vector2.zero) {
			Gizmos.DrawLine(transform.position, rch2d.point);
		}
		else {
			Gizmos.DrawLine(transform.position, transform.position + transform.up);
		}
	}
}
