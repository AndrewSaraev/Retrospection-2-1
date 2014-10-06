using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	void Update () {
		if (Input.GetAxisRaw("Quit") > 0f) {
			Application.Quit();
		}
#if UNITY_WEBPLAYER
#else
		if (Input.GetAxisRaw("Screenshot") > 0f) {
			string datetime = System.DateTime.Now.Year.ToString("D4") + "-" + System.DateTime.Now.Month.ToString("D2") + "-" + System.DateTime.Now.Day.ToString("D2") + " " + System.DateTime.Now.Hour.ToString("D2") + "-" + System.DateTime.Now.Minute.ToString("D2") + "-" + System.DateTime.Now.Second.ToString("D2");
#if UNITY_EDITOR
			print("Screenshot " + datetime + ".png");
#else
			Application.CaptureScreenshot("Screenshot " + datetime + ".png");
#endif
			Notification.Text = "Скриншот сохранен";
		}
#endif
	}
}
