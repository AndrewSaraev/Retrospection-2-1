using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {

	public Speaker speaker;
	public float defaultSmoothness = 0.25f;
	public float dragSmoothness = 0.1f;
	public float smoothness = 0.25f;

	bool wasPlaying;
	float grabPosition;
	float grabMousePosition;

	float position;

	public float targetPosition;
	float velocity;

	public float Position {
		get {
			return position;
		}
		set {
			position = value;
			transform.localPosition = new Vector3(8f - position * 16f, -6f, 0f);
			transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan(transform.localPosition.x / 20f) * Mathf.Rad2Deg);
		}
	}

	public float pitch;
	public float volume;

	int colliderBlocking = 0;
	public int ColliderBlocking {
		get {
			return colliderBlocking;
		}
		set {
			if (colliderBlocking == 0) {
				colliderBlocking = value;
				if (value != 0) {
					collider.enabled = false;
				}
			}
			else {
				colliderBlocking = value;
				if (value == 0) {
					collider.enabled = true;
				}
			}
		}
	}

	void LateUpdate() {
//		audio.pitch = Mathf.Clamp(Mathf.Abs(velocity) * pitch, 1f, 1.5f);
		audio.volume = Mathf.Clamp01(Mathf.Abs(velocity) * volume);
		Position = Mathf.SmoothDamp(Position, targetPosition, ref velocity, smoothness);
	}

	public string tooltip;
	
	void OnMouseEnter() {
		Tooltips.leftButton = tooltip;
		Mouse.Hovered = Mouse.Mode.Hand;
	}
	
	void OnMouseExit() {
		Tooltips.leftButton = null;
		Mouse.Hovered = Mouse.Mode.Arrow;
	}

	void OnMouseDown() {
		ColliderBlocking++;
		wasPlaying = speaker.audio.isPlaying;
		speaker.Pause();
		grabPosition = targetPosition;
		grabMousePosition = Input.mousePosition.x;
		Mouse.Override(Mouse.Mode.Grab);
		smoothness = dragSmoothness;
	}

	void OnMouseDrag() {
		float unitsPerPixel = Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad * 0.5f) * Camera.main.aspect * 2.8f / Camera.main.pixelWidth;
		targetPosition = Mathf.Clamp01(grabPosition + (Input.mousePosition.x - grabMousePosition) * unitsPerPixel);
	}

	void OnMouseUp() {
		ColliderBlocking--;
		speaker.AudioTime = targetPosition;
		if (wasPlaying && targetPosition != 1f) {
			speaker.audio.Play();
		}
		Mouse.ClearOverride();
		smoothness = defaultSmoothness;
	}
}
