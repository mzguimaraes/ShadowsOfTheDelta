using UnityEngine;
using System.Collections;

public class Decoy : MonoBehaviour {
	public float persistTime = 3f;

	private GameObject enemy;
	private Transform player;
	private float timeToLive;

	// Use this for initialization
	void Start () {
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag("Player").transform;

		//make enemy target me
		enemy.GetComponent<EnemyDrone>().PlayerPointer = transform;

		timeToLive = persistTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeToLive -= Time.deltaTime;
		if (timeToLive <= 0) {
			enemy.GetComponent<EnemyDrone>().PlayerPointer = player;
			player.gameObject.GetComponent<PlayerDecoy>().IsOut = false;
			Destroy(gameObject);
		}
	}
}
