// Script by Yang Liu

using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {
    public float maxSpeed = 5f;
    public Vector3 originalSpeed = new Vector3(0f,0f,0f);

	
    void Start() {
    }
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity + originalSpeed * Time.deltaTime;

        transform.position = pos;

	}
}
