using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

	public Tape tape;
		
	public ArchiveButton button;
		
	enum State { Idle, Down, Punching, Returning };
	State state;
		
	public float speed;
	float phase;
	bool pressed;

	float position;
	float Position {
		get {
			return position;
		}
		set {
			position = value;
			transform.localPosition = new Vector3(-7.5f, 1.85f, 6.75f + 0.25f * position);
		}
	}

	public Color color;		
		
		
	void Awake() {
		button.onPush += Punch;
		button.onRelease += Release;
	}
		
	void Punch() {
		pressed = true;
	}
		
	void Release() {
		pressed = false;
	}
		
	void Update () {
		switch (state) {
		case State.Idle:
			if (pressed) {
				phase = 0f;
				state = State.Punching;
				ArchiveButton.blockSources++;
			}
			break;
		case State.Punching:
			phase += Time.deltaTime * speed;
			if (phase >= 1f) {
				phase = 1f;
				state = State.Down;
				tape.Print(0, color);
				tape.Print(1, color);
			}
			Position = phase * phase;
			break;
		case State.Down:
			if (!pressed) {
				phase = 0f;
				state = State.Returning;
			}
			break;
		case State.Returning:
			phase += Time.deltaTime * speed;
			if (phase >= 1f) {
				phase = 1f;
				tape.SingleMove();
				ArchiveButton.blockSources--;
				state = State.Idle;
			}
			Position = 1f - phase * phase;
			break;
		}
	}
}