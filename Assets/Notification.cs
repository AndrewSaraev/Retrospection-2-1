using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour {

	static Notification main;

	static public string Text {
		get {
			if (main != null) {
				return main.text;
			}
			else {
				return null;
			}
		}
		set {
			if (main != null) {
				main.text = value;
				main.opacity = 2f;
			}
		}
	}



	public Color color;
	public Color shadowColor;
	public GUIStyle style;



	string text;
	float opacity = 0f;
	


	void OnEnable() {
		main = this;
	}

	void OnGUI() {
		if (opacity > 0f) {
			GUI.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, shadowColor.a * Mathf.Clamp01(opacity));
			GUI.Label(new Rect(Screen.width*0.03f, Screen.height*0.03f, 200f, 24f), text, style);
			GUI.Label(new Rect(Screen.width*0.03f+1f, Screen.height*0.03f, 200f, 24f), text, style);
			GUI.Label(new Rect(Screen.width*0.03f, Screen.height*0.03f+1, 200f, 24f), text, style);
			GUI.Label(new Rect(Screen.width*0.03f+1f, Screen.height*0.03f+1, 200f, 24f), text, style);
			GUI.color = new Color(color.r, color.g, color.b, color.a * Mathf.Clamp01(opacity));
			GUI.Label(new Rect(Screen.width*0.03f, Screen.height*0.03f, 200f, 24f), text, style);
			opacity -= Time.deltaTime * 0.5f;
		}
	}
}
