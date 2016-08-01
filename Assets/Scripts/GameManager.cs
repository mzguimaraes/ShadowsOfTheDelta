using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform lWall;
	public Transform rWall;
	public Transform topWall;
	public Transform botWall;

	public GameObject goal;

	// Use this for initialization
	void Start () {
		Vector3 goalPos = Vector3.zero;
		goalPos.x = Random.Range(lWall.transform.position.x, rWall.transform.position.x);
		goalPos.y = Random.Range(botWall.transform.position.y, topWall.transform.position.y);

		Instantiate(goal, goalPos, Quaternion.identity);
	}

}
