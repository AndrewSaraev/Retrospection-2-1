using UnityEngine;
using System.Collections;

public class Instruction : MonoBehaviour {

	public Transform getPoint;
	public Transform readingPoint;
	public Control cancel;
	public float speed;

	public AudioClip getSound;
	public AudioClip putSound;

	Vector3 startPosition;
	Quaternion startRotation;



	void ToReadingPoint() {
		GetComponent<SlerpTransform>().AddTarget(readingPoint.position, readingPoint.rotation, speed);
	}

	void ToStartPoint() {
		GetComponent<SlerpTransform>().AddTarget(startPosition, startRotation, speed);
	}

	void Read() {
		audio.PlayOneShot(getSound);
		CancelInvoke();
		CameraDirector.State = CameraDirector.Spot.Instruction;
		GetComponent<SlerpTransform>().AddTarget(getPoint.position, getPoint.rotation, speed);
		Invoke("ToReadingPoint", 0.5f / speed);
		collider.enabled = false;
		cancel.enabled = true;
	}

	void Return() {
		audio.PlayOneShot(putSound);
		CancelInvoke();
		GetComponent<SlerpTransform>().AddTarget(getPoint.position, getPoint.rotation, speed);
		Invoke("ToStartPoint", 0.5f / speed);
		CameraDirector.State = CameraDirector.Spot.Terminal;
		collider.enabled = true;
		cancel.enabled = false;
	}

	void Start() {
		startPosition = transform.position;
		startRotation = transform.rotation;
		GetComponent<Control>().onClick += Read;
		cancel.onClick += Return;
	}

	void Update() {
		if (CameraDirector.State == CameraDirector.Spot.Instruction && Input.GetMouseButtonDown(1)) {
			Return();
		}
	}
}
