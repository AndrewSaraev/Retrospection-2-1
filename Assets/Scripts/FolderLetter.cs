using UnityEngine;
using System.Collections;

public class FolderLetter : MonoBehaviour {

	public PaperOld letter;
	public string tooltip;

	void OnMouseDown() {
		letter.enabled = true;
		collider.enabled = false;
		CameraDirector.State = CameraDirector.Spot.Reading;
	}

	void OnMouseEnter() {
		Tooltips.leftButton = tooltip;
	}

	void OnMouseExit() {
		Tooltips.leftButton = null;
	}
}
