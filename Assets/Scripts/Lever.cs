using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

	public AudioClip onSound;
	public AudioClip offSound;

	public Speaker speaker;
	enum State { Down, GoingUp, Up, GoingDown };
	State state = State.Down;

	float phase;
	const float speed = 5f;

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

	void Update() {
		switch (state) {
		case State.Down:
			if (speaker.audio.isPlaying) {
				phase = 0f;
				state = State.GoingUp;
				ColliderBlocking++;
				audio.PlayOneShot(onSound);
			}
			break;
		case State.GoingUp:
			phase += Time.deltaTime * speed;
			if (phase < 1f) {
				transform.localEulerAngles = new Vector3(phase * phase * -90f, 0f, 0f);
			}
			else {
				transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
				state = State.Up;
				ColliderBlocking--;
			}
			break;
		case State.Up:
			if (!speaker.audio.isPlaying) {
				phase = 0f;
				state = State.GoingDown;
				ColliderBlocking++;
				audio.PlayOneShot(offSound);
			}
			break;
		case State.GoingDown:
			phase += Time.deltaTime * speed;
			if (phase < 1f) {
				transform.localEulerAngles = new Vector3(-90f + phase * phase * 90f, 0f, 0f);
			}
			else {
				transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				state = State.Down;
				ColliderBlocking--;
			}
			break;
		}
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

	void OnMouseUpAsButton() {
		if (speaker.audio.isPlaying) {
			if (state == State.Up) {
				phase = 0f;
				state = State.GoingDown;
				ColliderBlocking++;
				audio.PlayOneShot(offSound);
			}
			speaker.Pause();
		}
		else {
			if (state == State.Down) {
				phase = 0f;
				state = State.GoingUp;
				ColliderBlocking++;
				audio.PlayOneShot(onSound);
			}
			if (speaker.AudioTime != 1f) {
				speaker.audio.Play();
			}
		}
	}
}
