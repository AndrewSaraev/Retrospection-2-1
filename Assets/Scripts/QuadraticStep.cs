using UnityEngine;
using System.Collections;

public class QuadraticStep : MonoBehaviour {

	static public float Calc(float phase) {
		if (phase <= 0.5f) {
			return phase * phase * 2f;
		}
		else {
			float output = 1f - phase;
			return 1f - output * output * 2f;
		}
	}
}
