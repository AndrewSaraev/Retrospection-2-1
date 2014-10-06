using UnityEngine;
using System.Collections;

public class Paper : MonoBehaviour {

	public Transform tablePoint;
	public Transform getPoint;
	public Transform readingPoint;
	
	public AudioClip getSound;
	public AudioClip putSound;
	public float speed;

	int reading = 0;

	void ToReadingPoint() {
		GetComponent<SlerpTransform>().AddTarget(readingPoint.position, readingPoint.rotation, speed);
	}
	
	void ToTablePoint() {
		GetComponent<SlerpTransform>().AddTarget(tablePoint.position, tablePoint.rotation, speed);
	}

	void Read() {
		audio.PlayOneShot(getSound);
		CancelInvoke();
		CameraDirector.State = CameraDirector.Spot.Reading;
		GetComponent<SlerpTransform>().AddTarget(getPoint.position, getPoint.rotation, speed);
		Invoke("ToReadingPoint", 0.5f / speed);
		reading = 1;
	}
	
	void Return() {
		audio.PlayOneShot(putSound);
		CancelInvoke();
		CameraDirector.State = CameraDirector.Spot.Table;
		GetComponent<SlerpTransform>().AddTarget(getPoint.position, getPoint.rotation, speed);
		Invoke("ToTablePoint", 0.5f / speed);
		reading = 0;
	}

	void OnDisable() {
		GetComponent<Control>().onClick -= Read;
	}
	
	void OnEnable() {
		GetComponent<Control>().onClick += Read;
	}
	
	void Update() {
		switch (reading) {
		case 1:
			reading = 2;
			break;
		case 2:
			if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) {
				Return();
			}
			break;
		}
	}
}
