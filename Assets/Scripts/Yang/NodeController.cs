using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodeController : MonoBehaviour {

	public GameObject player;
	public Button buttonPrefab;
	public Transform panel;

	void OnTriggerEnter2D(Collider2D activator) {
		// if the activator is the player
		if(activator.gameObject == player){
			if(Input.GetKeyDown(KeyCode.Space)){
				// if the player presses space, instantiate a node prefab
				// and set the parent to the panel
				Button buttonObj = (Button)
					Instantiate (buttonPrefab, buttonPrefab.transform.position, 
						buttonPrefab.transform.rotation);
				buttonObj.transform.SetParent (panel);

				// set this game object to inactive
				gameObject.SetActive(false);
			}
		}
    }
}
