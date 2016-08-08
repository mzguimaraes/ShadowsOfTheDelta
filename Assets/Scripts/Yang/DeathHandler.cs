// script by Yang Liu
//edited by Marcus Guimaraes @mzguimaraes

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;		// for SceneManager

// this script handles the behaviour when player dies
// all the players are dead, if so, the script will run AfterDeath()

public class DeathHandler : MonoBehaviour {

	int player_count;
	private int dead_player_count;
	public int Dead_player_count {
		get {
			return dead_player_count;
		}
	}

	void Start () {

		// find the two "root" players and store them into the array
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		player_count = players.Length;
		// if the object has a parent, then it is not the "root" player,
		// so we minus the player_count by one
		foreach (GameObject player in players) {
			if (player.transform.parent != null) {
				player_count--;
			}
		}
		dead_player_count = 0;
	}

	public void PlayerDied () {
		// if there is no players in this scene, this script should do nothing
		if (player_count == 0){
			return;
		}

		// one player is killed if this function is called
		dead_player_count += 1;
		if(dead_player_count == player_count){
			// if all players are killer, call AfterDeath()
			AfterDeath ();
		}

	}

	void AfterDeath(){

		// reload the current active scene
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

	}
}
