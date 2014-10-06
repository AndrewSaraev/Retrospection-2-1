using UnityEngine;
using System.Collections;

public delegate void SetFloat(float value);

public class Volume : MonoBehaviour {

//	static Volume main;
	static public SetFloat onVolumeChange;

	static public float Level {
		get {
			return level;
		}
		set {
			level = Mathf.Clamp01(value);
			if (level != AudioListener.volume) {
				AudioListener.volume = level;
				Notification.Text = "Громкость: " + Mathf.RoundToInt(level * 100) + "%";
				if (onVolumeChange != null) {
					onVolumeChange(level);
				}
			}
		}
	}

	static float level = 1f;

/*
	void OnEnable() {
		main = this;
	}

	void OnDisable() {
		if (main == this) {
			main = null;
		}
	}
*/
	void Update() {
		float delta = Input.GetAxis("Volume") * Time.deltaTime;
		if (delta != 0f) {
			Level = Level + delta;
		}
	}
}