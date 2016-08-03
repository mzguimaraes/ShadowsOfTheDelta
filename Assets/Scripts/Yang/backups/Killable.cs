using UnityEngine;
using System.Collections;


public class Killable : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth = 0;


    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	//void Update () {
 //       // destroy myself if my health is less than or equal to 0
	//    if(currentHealth <= 0) {
 //           Destroy(gameObject);
 //       }
	//}

    void Update() {
        //Debug.Log(currentHealth);
    }

    public void Hurt(int damage) {
        currentHealth -= damage;
    
        // clamping health number
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    	
		//Debug.Log (currentHealth.ToString());
        // destroy myself if my health is less than or equal to 0
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}

