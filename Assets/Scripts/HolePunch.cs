using UnityEngine;
using System.Collections;

public class HolePunch : MonoBehaviour {

	public Tape tape;

	public ArchiveButton singleButton;
	public ArchiveButton dualButton;

	public Transform upperArm;
	public Transform bottomArm;
	public Transform bar;

	enum State { Idle, Punching, Down, Returning };
	State state;

	bool pressed;
	public float speed;
	float phase;

	float position;
	float Position {
		get {
			return position;
		}
		set {
			position = value;
			transform.localPosition = new Vector3(highHead.x, highHead.y, highHead.z - 0.4f * position);
			bottomArm.localPosition = new Vector3(highBottonArm.x, highBottonArm.y, highBottonArm.z - 0.2f * position);
			bottomArm.localEulerAngles = new Vector3(27f * sign * position, 0f, 0f);
			upperArm.localPosition = new Vector3(highUpperArm.x, highUpperArm.y, highUpperArm.z - 0.2f * position);
			upperArm.localEulerAngles = new Vector3(27f * sign * position, 0f, 0f);
			bar.localPosition = new Vector3(highBar.x, highBar.y, highBar.z + 0.15f * position);
		}
	}

	Vector3 highHead;
	Vector3 highUpperArm;
	Vector3 highBottonArm;
	Vector3 highBar;

	public int sign;
	public int textureIndex;

	public AudioClip sound;
	


	void Awake() {
		highHead = transform.localPosition;
		highBottonArm = bottomArm.localPosition;
		highUpperArm = upperArm.localPosition;
		highBar = bar.localPosition;

		singleButton.onPush += Punch;
		dualButton.onPush += Punch;
		singleButton.onRelease += Release;
		dualButton.onRelease += Release;
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
				audio.PlayOneShot(sound);
				ArchiveButton.blockSources++;
			}
			break;
		case State.Punching:
			phase += Time.deltaTime * speed;
			if (phase >= 1f) {
				phase = 1f;
				state = State.Down;
				tape.Print(textureIndex, Color.red);
			}
			Position = phase * phase;
			break;
		case State.Down:
			if (!pressed) {
				phase = 0f;
				state = State.Returning;
				tape.SingleMove();
			}
			break;
		case State.Returning:
			phase += Time.deltaTime * speed;
			if (phase >= 1f) {
				phase = 1f;
				ArchiveButton.blockSources--;
				state = State.Idle;
			}
			Position = 1f - phase * phase;
			break;
		}
	}
}
