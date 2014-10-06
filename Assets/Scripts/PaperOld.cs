using UnityEngine;
using System.Collections;

public class PaperOld : MonoBehaviour {

	public string zoomTooltip;
	public string hoverTooltip;
	bool mouseReleased = false;

	public enum StateType { OnTable, NearCamera };
	public StateType state = StateType.OnTable;

	Transform selectedTransform {
		get {
			if (state == StateType.NearCamera) {
				return nearTransform;
			}
			else {
				return farTransform;
			}
		}
	}
	
	public Transform farTransform;
	public Transform nearTransform;
	public float smoothness = 0.25f;

	Vector3 velocity;
	Vector2 rotationVelocity;
	
/*	void ListenCamera(CameraDirector.Spot newState) {
		switch(newState) {
		case CameraDirector.Spot.Table:
			if (!collider.enabled) {
				collider.enabled = true;
			}
			break;
		default:
			if (collider.enabled) {
				collider.enabled = false;
			}
			break;
		}
	}
	
	void OnDisable() {
		CameraDirector.reportListeners -= ListenCamera;
	}
	
	void OnEnable() {
		CameraDirector.reportListeners += ListenCamera;
	}
*/	
	void OnMouseDown() {
		state = StateType.NearCamera;
		CameraDirector.State = CameraDirector.Spot.Reading;
		mouseReleased = false;
//		Tooltips.rightButton = zoomTooltip;
	}
	
	void OnMouseEnter() {
		Tooltips.leftButton = hoverTooltip;
	}
	
	void OnMouseExit() {
		Tooltips.leftButton = null;
	}
	
	void Update() {
		transform.position = Vector3.SmoothDamp(transform.position, selectedTransform.position, ref velocity, smoothness);
		transform.eulerAngles = new Vector3(Mathf.SmoothDampAngle(transform.eulerAngles.x, selectedTransform.eulerAngles.x, ref rotationVelocity.x, smoothness),
		                                    Mathf.SmoothDampAngle(transform.eulerAngles.y, selectedTransform.eulerAngles.y, ref rotationVelocity.y, smoothness),
		                                    0f);


		if (state == StateType.NearCamera) {
			if (!mouseReleased && Input.GetMouseButtonUp(0)) {
				mouseReleased = true;
			}
			if (Input.GetMouseButtonDown(1) || (mouseReleased && Input.GetMouseButtonDown(0))) {
				state = StateType.OnTable;
	//			Tooltips.rightButton = null;
				CameraDirector.State = CameraDirector.Spot.Table;
			}
		}
	}
}
