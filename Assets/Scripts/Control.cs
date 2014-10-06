using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public string tooltip;
	public Mouse.Mode cursor;

	public delegate void Message();
	public Message onClick;
	
	void OnDisable() {
		collider.enabled = false;
	}
	
	void OnEnable() {
		collider.enabled = true;
	}

	void OnMouseEnter() {
		Tooltips.leftButton = tooltip;
		Mouse.Hovered = cursor;
	}
	
	void OnMouseExit() {
		Tooltips.leftButton = null;
		Mouse.Hovered = Mouse.Mode.Arrow;
	}

	void OnMouseDown() {
		if (onClick != null) {
			onClick();
		}
	}
}