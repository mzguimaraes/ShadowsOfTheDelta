// Script by Yang Liu

using UnityEngine;
using System.Collections;

public class SeerShoot : MonoBehaviour {
    public GameObject bulletPrefab;
    public float fireDelay = 0.25f;
    public Transform bulletStartTransform;
    public AudioSource shootSound;

    float cooldownTimer = 0;
  //  Vector3 lastFramePos = new Vector3(0f, 0f, 0f);
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownTimer <= 0) {
            //Debug.Log("Pew!");
            cooldownTimer = fireDelay;


			// instantiate the bullet, set its rotation to the player's up rotation
			// set the bullet to be within the same layer with the player
			GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, bulletStartTransform.position, 
				transform.rotation);


            //bulletGO.layer = gameObject.layer;

            // Play the sound for the shot

            //shootSound.Play();
            // possible problem with Play(): it restarts the sound if already playing
			if (shootSound != null) {
				shootSound.PlayOneShot (shootSound.clip);
			}

        }
	}
}
