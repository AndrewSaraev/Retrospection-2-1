using UnityEngine;
using System.Collections;

public class Letter : MonoBehaviour {

	public Transform far;
	public Transform near;
	public SlerpTransform fold;
		
	public AudioClip getSound;
	public AudioClip putSound;

	public Train train;

	int reading = 2;
	bool fog = true;

	void Take() {
		audio.PlayOneShot(getSound);
		reading = 1;
		CameraDirector.State = CameraDirector.Spot.Reading;
		GetComponent<SlerpTransform>().AddTarget(near.position, near.rotation, 2.5f);
		fold.AddTarget(Vector3.zero, Quaternion.identity, 1.5f);
	}

	void Start() {
		GetComponent<SlerpTransform>().AddTarget(near.position, near.rotation, 0.35f);
		fold.AddTarget(Vector3.zero, Quaternion.identity, 0.35f);
	}

	void OnDisable() {
		GetComponent<Control>().onClick -= Take;
	}

	void OnEnable() {
		GetComponent<Control>().onClick += Take;
	}

	void Update() {
		switch (reading) {
		case 1:
			reading = 2;
			break;
		case 2:
			if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) {
				fold.AddTarget(Vector3.zero, new Quaternion(0f, -0.9961947f, 0f, 0.0871558f), 2.5f);
				GetComponent<SlerpTransform>().AddTarget(far.position, far.rotation, 1.5f);
				audio.PlayOneShot(putSound);
				reading = 0;
				CameraDirector.State = CameraDirector.Spot.Table;
				if (fog) {
					GetComponent<FogController>().enabled = true;
					fog = false;
					train.enabled = true;
				}
			}
			break;
		}
	}
}
