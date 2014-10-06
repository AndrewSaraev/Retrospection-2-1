using UnityEngine;
using System.Collections;

public class Tooltips : MonoBehaviour {

	static public string leftButton;
	static public string rightButton;

	public Texture button;
	public Color color;
	public Color shadowColor;
	public GUIStyle leftStyle;
	public GUIStyle rightStyle;
	public float showSpeed = 5f;
	public float hideSpeed = 5f;

	string lastLeftButton;
	string lastRightButton;
	float leftOpacity;
	float rightOpacity;

	void OnGUI() {
		GUI.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, shadowColor.a * leftOpacity);
		GUI.Label(new Rect(Screen.width*0.5f-100f, Screen.height*0.03f, 200f, 24f), lastLeftButton, leftStyle);
		GUI.Label(new Rect(Screen.width*0.5f-99f, Screen.height*0.03f, 200f, 24f), lastLeftButton, leftStyle);
		GUI.Label(new Rect(Screen.width*0.5f-100f, Screen.height*0.03f, 200f, 24f), lastLeftButton, leftStyle);
		GUI.Label(new Rect(Screen.width*0.5f-99f, Screen.height*0.03f, 200f, 24f), lastLeftButton, leftStyle);
		GUI.color = new Color(color.r, color.g, color.b, color.a * leftOpacity);
		GUI.Label(new Rect(Screen.width*0.5f-100f, Screen.height*0.03f, 200f, 24f), lastLeftButton, leftStyle);
	}

	void Update() {
		if (lastLeftButton != leftButton) {
			leftOpacity -= Time.deltaTime * hideSpeed;
			if (leftOpacity <= 0f) {
				lastLeftButton = leftButton;
				leftOpacity = 0f;
			}
		}
		else if (leftOpacity != 1f && lastLeftButton != null) {
			leftOpacity += Time.deltaTime * showSpeed;
			if (leftOpacity > 1f) {
				leftOpacity = 1f;
			}
		}
		if (lastRightButton != rightButton) {
			rightOpacity -= Time.deltaTime * hideSpeed;
			if (rightOpacity <= 0f) {
				lastRightButton = rightButton;
				rightOpacity = 0f;
			}
		}
		else if (rightOpacity != 1f && lastRightButton != null) {
			rightOpacity += Time.deltaTime * showSpeed;
			if (rightOpacity > 1f) {
				rightOpacity = 1f;
			}
		}
	}
}