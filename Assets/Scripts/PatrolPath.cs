/*
 * Author: Marcus Guimaraes @mzguimaraes
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolPath : MonoBehaviour {

	public List<PatrolPathNode> path;

	void OnDrawGizmos() {
		for (int i = 0; i < path.Count - 1; i++) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(path[i].transform.position, path[i+1].transform.position);

		}
	}
}
