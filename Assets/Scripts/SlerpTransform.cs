using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlerpTransform : MonoBehaviour {

	public bool localSpace;

	List<Target> targets = new List<Target>();



	public void AddTarget(Vector3 position, Quaternion rotation, float speed) {
		targets.Add(new Target(position, rotation, speed));
	}

	void Awake() {
		if (localSpace) {
			targets.Add(new Target(transform.localPosition, transform.localRotation, 0f, 1f));
		}
		else {
			targets.Add(new Target(transform.position, transform.rotation, 0f, 1f));
		}
	}

	void Update() {
		if (targets.Count > 0) {
			int completeTarget = -1;
			Vector3 newPosition = targets[0].position;
			Quaternion newRotation = targets[0].rotation;
			for (int i = 1; i < targets.Count; i++) {
				targets[i].weight += Time.deltaTime * targets[i].speed;
				if (targets[i].weight >= 1f) {
					targets[i].weight = 1f;
					completeTarget = i;
				}
				newPosition = Vector3.Lerp(newPosition, targets[i].position, QuadraticStep.Calc(targets[i].weight));
				newRotation = Quaternion.Lerp(newRotation, targets[i].rotation, QuadraticStep.Calc(targets[i].weight));
			}
			if (localSpace) {
				transform.localPosition = newPosition;
				transform.localRotation = newRotation;
			}
			else {
				transform.position = newPosition;
				transform.rotation = newRotation;
			}
			if (completeTarget > 0) {
				targets.RemoveRange(0, completeTarget);
			}
		}
	}

	class Target {
		public Vector3 position;
		public Quaternion rotation;
		public float speed;
		public float weight;

		public Target(Vector3 position, Quaternion rotation, float speed, float weight) {
			this.position = position;
			this.rotation = rotation;
			this.weight = weight;
			this.speed = speed;
		}

		public Target(Vector3 position, Quaternion rotation, float speed) {
			this.position = position;
			this.rotation = rotation;
			this.speed = speed;
			this.weight = 0f;
		}

		public override string ToString() {
			return "Position: " + position + "; Rotation: " + rotation + "; Speed: " + speed + "; Weight: " + weight;
		}
	}
}
