using UnityEngine;
using System.Collections;

public class SpawnZombie : MonoBehaviour {
	public GameObject zombieGO;

	public float respawnTime = 5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating("RespawnZombie", respawnTime, respawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	}
		

	void RespawnZombie(){
		Instantiate (zombieGO, transform.position, Quaternion.Euler (0f, 0f, 0f));
	}
}
