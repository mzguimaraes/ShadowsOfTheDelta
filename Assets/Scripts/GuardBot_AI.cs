/*
 * Author: Marcus Guimaraes @mzguimaraes
 */


using UnityEngine;
using System.Collections;

public class GuardBot_AI : MonoBehaviour {

	public PatrolPath patrol;
	public float speed = 2f;

	private PatrolPathNode destination;
	private int destinationIndex = 1; //index of destination in patrol.path
	private bool isPatrollingBackwards = false; //flips when bot reaches end of patrol path and heads back

	//patrol
	void patrolMove() {
		//call in Update()
		//moves the bot incrementally towards the next node in patrol

		Vector3 moveVector = destination.transform.position - transform.position; //vector between
		moveVector.Normalize();
		moveVector = moveVector * speed * Time.deltaTime; //scale

		transform.position += moveVector;
	}

	//check destination, swap if necessary
	void updateDestination() {
		//TODO: make this clean 
		//TODO: will probably break on patrolPath of size 1 or less
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
					else { //only one node in this patrol
						//stay where we are (this isn't desired and will probably break,
						//but i'm not testing this because deadlines)
					}
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
	//shoot
	//follow/search for player



	// Use this for initialization
	void Start () {
		transform.position = patrol.path[0].transform.position;
		destination = patrol.path[1];
	}
	
	// Update is called once per frame
	void Update () {
		patrolMove();
		updateDestination();
	
	}
}
