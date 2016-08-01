using UnityEngine;
using System.Collections;

public class PlayerDecoy : MonoBehaviour {

	public GameObject decoy;
	public int numAllowed = 3;

	private bool isOut = false;
	public bool IsOut {
		set {
			isOut = value;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F) && !isOut && numAllowed > 0) {
			Instantiate(decoy, transform.position, Quaternion.identity);
			isOut = true;
			numAllowed--;
		}
	}


}
