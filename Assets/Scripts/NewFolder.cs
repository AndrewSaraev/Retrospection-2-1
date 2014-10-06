using UnityEngine;
using System.Collections;

public class NewFolder : MonoBehaviour {

	public Control openControl;
	public Control closeControl;
	public Control doc1;

	int takenDoc = 0;



	void Doc1Take() {
		CameraDirector.State = CameraDirector.Spot.Reading;
		takenDoc = 1;
		animation.PlayQueued("1 Take");
	}

	void Close() {
		animation.PlayQueued("Folder Close");
	}

	void Open() {
		animation.PlayQueued("Folder Open");
	}

	void Awake() {
		openControl.onClick += Open;
		closeControl.onClick += Close;
		doc1.onClick += Doc1Take;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			enabled = false;
			CameraDirector.State = CameraDirector.Spot.Table;
			switch (takenDoc) {
			case 1:
				animation.PlayQueued("1 Put");
				break;
			}
		}
	}
}
