using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {
    public int damage = 1;
	public double hitPerSecond = 1;

	double intrigger_timer;

    // a function that is automatically called when
    // something with a Rigidbody2D enters this trigger
	void OnTriggerEnter2D(Collider2D activator) {
		intrigger_timer = Time.time;

		// if the collider is in the same layer with the parent object,
		// do not hurt anybody
		if(activator.gameObject.layer == gameObject.layer){
			return;
		}

        // does the activating thing have a Killable script on it?
        if (activator.GetComponent<Killable>() != null) {
            // TODO: subtract health from the Killable script


			activator.GetComponent<Killable>().Hurt(damage);
            

			// destroy this object
            //Destroy(activator.gameObject);
        }
    }

    // a function that is automatically called when
    // AS LONG AS a thing stays in the trigger, each frame
    void OnTriggerStay2D(Collider2D activator) {
		// if the collider is in the same layer with the parent object,
		// do not hurt anybody
		if(activator.gameObject.layer == gameObject.layer){
			return;
		}


        if (activator.GetComponent<Killable>() != null) {
			double elasped_time = Time.time - intrigger_timer;

			if(elasped_time > 1f / hitPerSecond){
	           

				activator.GetComponent<Killable>().Hurt(damage);


				intrigger_timer = Time.time;
			}
        }
    }
}
