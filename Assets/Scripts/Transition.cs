using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Transition : MonoBehaviour {

	public Transform origin;
	public Transform destination;
	public float phase;



	void LateUpdate () {
		if (origin != null) {
			if (destination != null) {
				transform.position = Vector3.Lerp(origin.position, destination.position, phase);
				transform.rotation = Quaternion.Lerp(origin.rotation, destination.rotation, phase);
			}
			else {
				transform.position = origin.position;
				transform.rotation = origin.rotation;
			}
		}
		else {
			if (destination != null) {
				transform.position = destination.position;
				transform.rotation = destination.rotation;
			}
		}
	}
}
