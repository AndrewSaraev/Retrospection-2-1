using UnityEngine;
using System.Collections;

public class LoadingBar : MonoBehaviour {

	public GUITexture bar;
	public GUITexture fade;

	bool ready = false;
	bool loading = false;
	float phase = 1f;

	void OnMouseDown() {
		loading = true;
		fade.enabled = true;
	}

	void Update () {
		if (!ready) {
			if (Application.CanStreamedLevelBeLoaded("Retrospection 2-1")) {
				bar.enabled = false;
				guiTexture.enabled = true;
				ready = true;
			}
			else {
				float progress = Application.GetStreamProgressForLevel("Retrospection 2-1");
				int width = Mathf.RoundToInt(360f * progress);
				Rect pixels = bar.pixelInset;
				pixels.width = width;
				bar.pixelInset = pixels;
				bar.border = new RectOffset(width, 0, 0, 0);
			}
		}
		if (loading) {
			phase += Time.deltaTime * 2f;
			if (phase >= 1f) {
				Application.LoadLevel("Retrospection 2-1");
			}
			else {
				fade.color = new Color(0f, 0f, 0f, 0.5f * phase);
			}
		}
		else {
			if (fade.enabled) {
				phase -= Time.deltaTime * 0.5f;
				if (phase <= 0f) {
					phase = 0f;
					fade.enabled = false;
				}
				fade.color = new Color(0f, 0f, 0f, 0.5f * phase);
			}
		}
	}
}
