using UnityEngine;
using System.Collections;

public class QualityManager : MonoBehaviour {

	public delegate void ChangeQuality(int level);

	static public ChangeQuality onQualityChange;

	bool _pressed = false;

	static public int Level {
		get {
			return QualitySettings.GetQualityLevel();
		}
		set {
			int newQuality = Mathf.Clamp(value, 0, 2);
			string notification = "Качество графики: ";
			switch (newQuality) {
			case 0:
				notification += "быстрое";
				break;
			case 1:
				notification += "хорошее";
				break;
			case 2:
				notification += "лучшее";
				break;
			}
			Notification.Text = notification;
			QualitySettings.SetQualityLevel(newQuality, SystemInfo.graphicsShaderLevel <= 20);
			if (onQualityChange != null) {
				onQualityChange(newQuality);
			}
		}
	}

	void Update () {
		int axis = Mathf.RoundToInt(Input.GetAxisRaw("Graphics Quality"));
		if (_pressed && axis == 0) {
			_pressed = false;
		}
		if (!_pressed && axis != 0) {
			_pressed = true;
			Level += axis;
		}
	}
}
