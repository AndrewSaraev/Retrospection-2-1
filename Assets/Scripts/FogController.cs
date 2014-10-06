using UnityEngine;
using System.Collections;

public class FogController : MonoBehaviour {

	float phase = 0;
	float far;
	float near;

	public Transform flare1;
	public Transform flare2;
	float flare1Scale;
	float flare2Scale;

	void Awake() {
		RenderSettings.fog = true;
		far = RenderSettings.fogEndDistance;
		near = RenderSettings.fogStartDistance;
		flare1Scale = flare1.localScale.x;
		flare2Scale = flare2.localScale.x;
		flare1.localScale = Vector3.zero;
		flare2.localScale = Vector3.zero;
	}

	void Update() {
		phase += Time.deltaTime * 1f;
		if (phase >= 1f) {
			enabled = false;
			RenderSettings.fog = false;
			flare1.localScale = new Vector3(flare1Scale, flare1Scale, flare1Scale);
			flare2.localScale = new Vector3(flare2Scale, flare2Scale, flare2Scale);
		}
		else {
			float quadraticPhase = QuadraticStep.Calc(phase);
			RenderSettings.fogStartDistance = Mathf.Lerp(near, 100, quadraticPhase);
			RenderSettings.fogEndDistance = Mathf.Lerp(far, 200, quadraticPhase);
			float flarePhase = QuadraticStep.Calc(Mathf.Min(phase * 2f, 1f));
			float flare1Lerp = Mathf.Lerp(0f, flare1Scale, flarePhase);
			float flare2Lerp = Mathf.Lerp(0f, flare2Scale, flarePhase);
			flare1.localScale = new Vector3(flare1Lerp, flare1Lerp, flare1Lerp);
			flare2.localScale = new Vector3(flare2Lerp, flare2Lerp, flare2Lerp);
		}
	}

}
