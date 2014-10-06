using UnityEngine;
using System.Collections;

public class Framerate : MonoBehaviour {

	public Color color;
	public Color shadowColor;
	public GUIStyle style;

	float fps = 0f;
	float velocity = 0f;
	float smoothness = 1f;

	bool visible = false;

/*	void Awake() {
		Application.targetFrameRate = 120;
	} */

	void OnGUI() {
		if (visible) {
			fps = Mathf.SmoothDamp(fps, 1 / Time.deltaTime, ref velocity, smoothness);
			string text = Mathf.RoundToInt(fps).ToString() + " к/с";
			GUI.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, shadowColor.a);
			GUI.Label(new Rect(Screen.width*0.97f-200f, Screen.height*0.03f, 200f, 24f), text, style);
			GUI.Label(new Rect(Screen.width*0.97f-199f, Screen.height*0.03f, 200f, 24f), text, style);
			GUI.Label(new Rect(Screen.width*0.97f-200f, Screen.height*0.03f+1, 200f, 24f), text, style);
			GUI.Label(new Rect(Screen.width*0.97f-199f, Screen.height*0.03f+1, 200f, 24f), text, style);
			GUI.color = new Color(color.r, color.g, color.b, color.a);
			GUI.Label(new Rect(Screen.width*0.97f-200f, Screen.height*0.03f, 200f, 24f), text, style);
		}
	}

	void Update() {
		if (Input.GetButtonDown("Framerate")) {
			visible = !visible;
			if (visible) {
				fps = 1 / Time.deltaTime;
				velocity = 0f;
			}
		}
	}
}
