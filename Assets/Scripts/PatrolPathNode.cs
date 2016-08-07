using UnityEngine;
using System.Collections;

public class PatrolPathNode : MonoBehaviour {
	//will be populated later if necessary
	//right now we're just using this for its transform and to clarify code in PatrolPath.cs

	public bool Equals(PatrolPathNode other) {
		return this.GetInstanceID() == other.GetInstanceID();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(transform.position, (Vector3.one * 0.2f));
	}
}
