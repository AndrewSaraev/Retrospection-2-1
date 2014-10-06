using UnityEngine;
using System.Collections;

public class ZoomInArea : MonoBehaviour {
	
	public string zoomTooltip;
	public string hoverTooltip;
	public Control table;

	void OnEnable() {
		table.onClick = Out;
	}

	void OnMouseDown() {
		CameraDirector.State = CameraDirector.Spot.Terminal;
	}

	void OnMouseEnter() {
		Tooltips.leftButton = hoverTooltip;
		Mouse.Hovered = Mouse.Mode.ZoomIn;
	}

	void OnMouseExit() {
		Tooltips.leftButton = null;
		Mouse.Hovered = Mouse.Mode.Arrow;
	}

	void Out() {
		CameraDirector.State = CameraDirector.Spot.Table;
	}

	void Update() {
		if (CameraDirector.State == CameraDirector.Spot.Terminal && Input.GetMouseButtonDown(1)) {
			Out();
		}
	}
}
