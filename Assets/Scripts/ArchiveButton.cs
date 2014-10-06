using UnityEngine;
using System.Collections;

public class ArchiveButton : MonoBehaviour {

	static public int blockSources = 0;

	enum State { Idle, Pushing, Pushed, Returning };
	State state;
	bool pressed = false;
	float phase = 0;
	public float speed = 4f;
	public string tooltip;

	public delegate void Message();
	public Message onPush;
	public Message onRelease;

	public AudioClip pushSound;
	public AudioClip releaseSound;

/*	void ListenCamera(CameraDirector.Spot newState) {
		switch(newState) {
		case CameraDirector.Spot.Terminal:
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
		pressed = true;
	}

	void OnMouseEnter() {
		Tooltips.leftButton = tooltip;
		Mouse.Hovered = Mouse.Mode.Hand;
	}

	void OnMouseExit() {
		Tooltips.leftButton = null;
		Mouse.Hovered = Mouse.Mode.Arrow;
	}
	
	void OnMouseUp() {
		pressed = false;
	}

	void Update () {
		switch (state) {
		case State.Idle:
			if (blockSources == 0 && pressed) {
				if (onPush != null) onPush();
				blockSources++;
				state = State.Pushing;
				phase = 0f;
				audio.PlayOneShot(pushSound);
			}
			break;
		case State.Pushing:
			phase += Time.deltaTime * speed;
			if (phase >= 1f) {
				phase = 1f;
				state = State.Pushed;
			}
			transform.localEulerAngles = new Vector3(phase * phase * 10f, 0f, 0f);
			break;
		case State.Pushed:
			if (!pressed) {
				if (onRelease != null) onRelease();
				state = State.Returning;
				phase = 0f;
				audio.PlayOneShot(releaseSound);
			}
			break;
		case State.Returning:
			phase += Time.deltaTime * speed;
			if (phase >= 1f) {
				phase = 1f;
				blockSources--;
				state = State.Idle;
			}
			transform.localEulerAngles = new Vector3(10f - phase * phase * 10f, 0f, 0f);
			break;
		}
	}
}
