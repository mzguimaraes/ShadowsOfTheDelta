using UnityEngine;
using System.Collections;

public class PatrolPathNode : MonoBehaviour {
	//will be populated later if necessary
	//right now we're just using this for its transform and to clarify code in PatrolPath.cs

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(transform.position, (Vector3.one * 0.2f));
	}
}
