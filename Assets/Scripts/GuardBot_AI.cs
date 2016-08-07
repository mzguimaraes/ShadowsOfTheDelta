/*
 * Author: Marcus Guimaraes @mzguimaraes
 */


using UnityEngine;
using System.Collections;

public class GuardBot_AI : MonoBehaviour {
	//TODO: change movement system to use physics
	//TODO: field of view 180 degrees ahead
	//TODO: raycast for vision (break on walls)

	public PatrolPath patrol;
	public float speed = 2f;
	public float playerFollowTime = 5f;
	public GameObject bullet;

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


	private Collider2D spotlight;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player") {
			other.gameObject.GetComponent<PlayerStatus>().Kill();
		}
	}

	// Use this for initialization
	void Start () {
		transform.position = patrol.path[0].transform.position;
		destination = patrol.path[1];
		spotlight = GetComponent<Collider2D>();
		playerFollowCountDown = playerFollowTime;
		lastKnownPosition = Vector3.zero;
	}

	//patrol
	void patrolMove(Vector3 target) {
		//call in Update()
		//moves the bot incrementally towards the next node in patrol
		//moves through walls so don't cross any with patrol paths

		Vector3 moveVector = target - transform.position; //vector between
		moveVector.Normalize();
		moveVector = moveVector * speed * Time.deltaTime; //scale

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
					}
				}
			}
		}
	}

	//check for player
	void OnTriggerExit2D(Collider2D other) {
		//TODO: make this work off the childed light beam's collider
		//TODO: fix movement direction
		if (other.tag == "Player") {
			playerFollowCountDown = playerFollowTime;
			lastKnownPosition = other.transform.position;
			directionLastKnown = (lastKnownPosition - transform.position).normalized;
		}
	}

	//shoot
//	void OnTriggerStay2D(Collider2D other) {
//		//TODO: make bullet rotate toward player (and change bullet sprite to be oblong)
//		if (other.tag == "Player") {
//			Instantiate(bullet, transform.position, Quaternion.identity);
//		}
//	}

	//follow/search for player
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			followingPath = false;
		}
		else if (other.tag == "Map") {
			//destination *= -1*/;
		}
	}




	
	// Update is called once per frame
	void Update () {
		if (followingPath){ //move along patrol
			patrolMove(destination.transform.position);
			checkDestinationAgainstPatrol();
		}
		else { //move to player
			playerFollowCountDown -= Time.deltaTime;


			patrolMove(transform.position + directionLastKnown);

			if (playerFollowCountDown <= 0f) {
				followingPath = true;
			}
		}
	}
}
