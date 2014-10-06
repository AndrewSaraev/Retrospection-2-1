using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {

	public Material flare;
	public Material gas;
	public Light lightSource;
	public AudioClip sound;

	public float minTrainPeriod;
	public float maxTrainPeriod;

	float lightIntensity;
	
	public float brightness;

	void Start () {
		lightIntensity = lightSource.intensity;
	}

	public float timeToNext = 30f;
	
	void Update () {
		if (true) {
			timeToNext -= Time.deltaTime;
			if (timeToNext <= 0f) {
				timeToNext = Random.Range(minTrainPeriod, maxTrainPeriod);
				audio.PlayOneShot(sound, 2.5f);
				GetComponent<Animator>().Play("Train");
			}
		}

		Color color = flare.color;
		color.a = brightness;
		flare.color = color;
		color = gas.color;
		color.a = brightness;
		gas.color = color;
		lightSource.intensity = lightIntensity * brightness;
	}
}
