using UnityEngine;
using System.Collections;

public class QualitySlider : MonoBehaviour {

	int PositionToLevel(float position) {
		return Mathf.RoundToInt(position / 90f);
	}

	float LevelToPosition(int level) {
		return level * 90f;
	}

	float Position {
		get {
			return guiTexture.pixelInset.x + 8f;
		}
		set {
			Rect rect = guiTexture.pixelInset;
			rect.x = value - 8f;
			guiTexture.pixelInset = rect;
		}
	}
	


	float grabDelta;

	void OnLevelChange(int level) {
		Position = LevelToPosition(level);
	}

	void OnEnable () {
		Position = LevelToPosition(QualityManager.Level);
		QualityManager.onQualityChange += OnLevelChange;
	}

	void OnDisable() {
		QualityManager.onQualityChange -= OnLevelChange;
	}

	void OnMouseDown() {
		grabDelta = Position - Input.mousePosition.x;
	}

	void OnMouseDrag() {
		QualityManager.Level = PositionToLevel(Input.mousePosition.x + grabDelta);
	}
}
