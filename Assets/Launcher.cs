using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Launcher : MonoBehaviour {

	public Texture2D logo;
	public GUISkin skin;

	void OnGUI() {
		GUI.skin = skin;

		GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
		GUILayout.FlexibleSpace();

		GUILayout.BeginVertical(GUILayout.Height(Screen.height));
		GUILayout.FlexibleSpace();

		GUILayout.Box(logo);

		GUILayout.Space(50f);

		GUILayout.Label("Качество графики");

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		int quality = QualitySettings.GetQualityLevel();
		int newQuality = GUILayout.SelectionGrid(quality, new string[] { "Быстрое", "Хорошее", "Лучшее" }, 3);
		if (quality != newQuality) {
			QualitySettings.SetQualityLevel(newQuality);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.Space(100f);
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		if (Application.CanStreamedLevelBeLoaded("Retrospection 2-1")) {
			if (GUILayout.Button("Начать")) {
				Application.LoadLevel("Retrospection 2-1");
			}
		}
		else {
			float progress = Application.GetStreamProgressForLevel("Retrospection 2-1");
			GUILayout.Label((Mathf.Round(progress * 1000f)*0.1f).ToString() + "%");
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}
}
