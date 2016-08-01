using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

	public float rotateSpeed = 30;
	public float maxAngle = 50;

	float lastAngle = 0;
	bool turningLeft = true;

    public GameObject laserBeamObject;
    public float laserLength = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// turn the laser generator
		if (turningLeft) {
			transform.Rotate (0f, 0f, Time.deltaTime * rotateSpeed);
			float currentAngle = transform.rotation.eulerAngles.z;
			if(InRange(currentAngle, 0f, 180f) && currentAngle > maxAngle){
				turningLeft = false;
			}
		}else{
			transform.Rotate (0f, 0f, - Time.deltaTime * rotateSpeed);
			float currentAngle = transform.rotation.eulerAngles.z;
			if(InRange(currentAngle, 180f, 360f) && currentAngle < 360f - maxAngle){
				turningLeft = true;
			}
		}

        // actually shoot raycast now, only detecting things on the player layer
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up,
            laserLength * laserBeamObject.transform.localScale.y,
            LayerMask.GetMask("Player"));

        // let's check the results of the raycast
        if (raycastHit.collider != null) {
            Destroy(raycastHit.collider.gameObject); // destroy the thing it hit
        }


    }

	bool InRange(float candidate_angle, float min_angle, float max_angle){
		return (candidate_angle < max_angle) && (candidate_angle >= min_angle);
	}
}
