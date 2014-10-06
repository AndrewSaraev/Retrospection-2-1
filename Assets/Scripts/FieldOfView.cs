using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FieldOfView : MonoBehaviour {

	public Vector2 frame;

	void LateUpdate () {
		if (camera.aspect >= frame.x / frame.y) {
			camera.fieldOfView = Mathf.Atan(frame.y) * 2f * Mathf.Rad2Deg;
		}
		else {
			camera.fieldOfView = Mathf.Atan(frame.x / camera.aspect) * 2f * Mathf.Rad2Deg;
		}
	}
}
