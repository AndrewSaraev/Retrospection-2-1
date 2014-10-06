using UnityEngine;
using System.Collections;

public class VolumeSlider : MonoBehaviour {

	float PositionToLevel(float position) {
		return position / 180f;
	}

	float LevelToPosition(float level) {
		return level * 180f;
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

	void OnLevelChange(float level) {
		Position = LevelToPosition(level);
	}

	void OnEnable() {
		Position = LevelToPosition(AudioListener.volume);
		Volume.onVolumeChange += OnLevelChange;
	}

	void OnDisable() {
		Volume.onVolumeChange -= OnLevelChange;
	}

	void OnMouseDown() {
		grabDelta = Position - Input.mousePosition.x;
	}

	void OnMouseDrag() {
		Volume.Level = PositionToLevel(Input.mousePosition.x + grabDelta);
	}
}
