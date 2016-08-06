/*
 * Author: Marcus Guimaraes @mzguimaraes
 */


using UnityEngine;
using System.Collections;

public class GuardBot_AI : MonoBehaviour {
	//TODO: change movement system to use physics
	//TODO: field of view 180 degrees ahead

	public PatrolPath patrol;
	public float patrolSpeed = 2f;
	public float chaseSpeed = 5f;
	public float playerFollowTime = 5f;

	//for finding player(s)
	private bool followingPath = true;
	private float playerFollowCountDown;
	private Vector3 lastKnownPosition;
	private Vector3 directionLastKnown;

	//patrolMove()
	private PatrolPathNode destination;

	//checkDestinationAgainstPatrol()
	private int destinationIndex = 1; //index of destination in patrol.path
	private bool isPatrollingBackwards = false; //flips when bot reaches end of patrol path and heads back

	// Use this for initialization
	void Start () {
		transform.position = patrol.path[0].transform.position;
		destination = patrol.path[1];
		playerFollowCountDown = playerFollowTime;
		lastKnownPosition = Vector3.zero;
	}

	//patrol
	void patrolMove(Vector3 target, float speed) {
		//call in Update()
		//moves the bot incrementally towards the next node in patrol

		Vector3 moveVector = target - transform.position; //vector between
		moveVector.Normalize();
		moveVector = moveVector * speed * Time.deltaTime; //scale

		//check for wall in way
		RaycastHit2D rch2d = Physics2D.Raycast(transform.position, moveVector, moveVector.magnitude);
		if (rch2d.collider != null) {
			if (rch2d.collider.tag == "Map") {
				//move around wall TODO
			}

		}
		//change to velocity TODO
		transform.position += moveVector;
	}

	//check destination, swap if necessary
	void checkDestinationAgainstPatrol() {
		//TODO: make this clean 
		if ( (transform.position - destination.transform.position).magnitude <= 0.1f ) {
			//reached destination node, set next one
			if (!isPatrollingBackwards) { //moving forwards through path
				if (destinationIndex + 1 < patrol.path.Count) { //there are more nodes ahead
					PatrolPathNode nextNode = patrol.path[destinationIndex + 1];
					destination = nextNode;
					destinationIndex++;
				}
				else { //no more nodes in this direction, start moving the other way
					isPatrollingBackwards = true;
					if (destinationIndex - 1 >= 0) { //there are nodes behind this one
						PatrolPathNode nextNode = patrol.path[destinationIndex - 1];
						destination = nextNode;
						destinationIndex--;
						gameObject.GetComponent<SpriteRenderer>().flipX ^= true; //bitwise OR--flips value

					}
//					else { //only one node in this patrol
//						//stay where we are (this isn't desired and will probably break,
//						//but i'm not testing this because deadlines)
//					}
				}
			}
			else { //moving backwards through path -- same logic as above but reversed
				if (destinationIndex - 1 >= 0) {
					PatrolPathNode nextNode = patrol.path[destinationIndex - 1];
					destination = nextNode;
					destinationIndex--;
				}
				else {
					isPatrollingBackwards = false;
					if (destinationIndex + 1 < patrol.path.Count) {
						PatrolPathNode nextNode = patrol.path[destinationIndex + 1];
						destination = nextNode;
						destinationIndex++;
						gameObject.GetComponent<SpriteRenderer>().flipX ^= true; //bitwise OR--flips value
					}
				}
			}
		}
	}

	//check for player
//	void OnTriggerExit2D(Collider2D other) {
//		//TODO: make this work off the childed light beam's collider
//		if (other.tag == "Player") {
//			playerFollowCountDown = playerFollowTime;
//			updateLastKnown(other.transform.position);
//		}
//	}

	//replacement for OnTriggerExit2D() using raycasts
	bool checkVision() {
		RaycastHit2D rch2d = Physics2D.Raycast(transform.position, transform.up); //guards are designed w/ down as up
		if (rch2d.collider != null) {
			if (rch2d.collider.tag == "Player") {
				playerFollowCountDown = playerFollowTime;
				updateLastKnown(rch2d.collider.transform.position);
				followingPath = false;
				return true;
			}
			else {
				Collider2D[] objectsInVision = Physics2D.OverlapCircleAll(transform.position, rch2d.distance);
				foreach (Collider2D obj in objectsInVision) {
					if (obj.tag == "Player") {
						playerFollowCountDown = playerFollowTime;
						updateLastKnown(rch2d.collider.transform.position);
						followingPath = false;
						return true;
					}
				}
			}
		}
		return false;
	}

//	//if see player change movement direction
//	void OnTriggerStay2D(Collider2D other) {
//		//TODO: make bullet rotate toward player (and change bullet sprite to be oblong)
//		if (other.tag == "Player") {
//			updateLastKnown(other.transform.position);
//
////			//raycast to see if near player
////			RaycastHit2D rch2d = Physics2D.Raycast(transform.position,
////				(other.transform.position - transform.position), 
////				0.1f);
////			if (rch2d != null && rch2d.collider.tag == "Player") {
////				
////				rch2d.collider.gameObject.SetActive(false);
////			}
//
//
//			//Instantiate(bullet, transform.position, Quaternion.identity);
//		}
//	}

//	//see player
//	void OnTriggerEnter2D(Collider2D other) {
//		if (other.tag == "Player") {
//			followingPath = false;
//		}
//		else if (other.tag == "Map") {
//			//pathfind
//			//destination *= -1;
//		}
//	}



	//caught player
	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player") {
			other.gameObject.GetComponent<PlayerStatus>().Kill();
		}
	}

	void updateLastKnown(Vector3 position) {
		lastKnownPosition = position;
		directionLastKnown = (lastKnownPosition - transform.position).normalized;
	}

	void rotateForMovement(Vector3 target) {
		//rotates object in direction of movement

		//get unit vector of direction
		Vector3 vectorToTarget = target - transform.position;
		vectorToTarget.Normalize();

		//rotate in that direction
		transform.up = vectorToTarget;
	}
		
	IEnumerator lookAround() {
		//looks around in a circle
		for (float f = 0f; f <= 360f; f += 1) {
			if (followingPath) break;
			transform.Rotate(new Vector3(0f, 0f, 1f) * Time.deltaTime );
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
		bool playerInVision = checkVision();

		if (followingPath){ //move along patrol
			rotateForMovement(destination.transform.position);
			patrolMove(destination.transform.position, patrolSpeed);
			checkDestinationAgainstPatrol();
		}
		else if (playerInVision){ //move to player
			rotateForMovement(transform.position + directionLastKnown);
			patrolMove(transform.position + directionLastKnown, chaseSpeed);

			//if reached lastKnownPos, stop and look around
			if (( lastKnownPosition - transform.position ).magnitude <= 0.25f ) {
				//TODO: make this work
				StartCoroutine("lookAround");
			}

			playerFollowCountDown -= Time.deltaTime;
		}
		else {
			patrolMove(transform.position + directionLastKnown, chaseSpeed);
			playerFollowCountDown -= Time.deltaTime;
			if (playerFollowCountDown <= 0f) {
				followingPath = true;
			}
		}
	}

	void OnDrawGizmos() {
		RaycastHit2D rch2d = Physics2D.Raycast(transform.position, transform.up);
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position, rch2d.point);
	}
}
