/*
 * Author: Marcus Guimaraes @mzguimaraes
 */

using UnityEngine;
using System.Collections;

public class PatrolPathNode : MonoBehaviour {

	public bool Equals(PatrolPathNode other) {
		return this.GetInstanceID() == other.GetInstanceID();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(transform.position, (Vector3.one * 0.2f));
	}
}
