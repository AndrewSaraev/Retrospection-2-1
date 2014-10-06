using UnityEngine;
using System.Collections;

public class QualityAdapter : MonoBehaviour {

	void Start () {
		if (SystemInfo.graphicsShaderLevel >= 30) {
			if (QualitySettings.GetQualityLevel() > 0) {
				GetComponent<AntialiasingAsPostEffect>().enabled = true;
			}
			if (QualitySettings.GetQualityLevel() > 1) {
				GetComponent<SSAOEffect>().enabled = true;
			}
		}
		QualityManager.onQualityChange += OnQualityChange;
	}

	void OnQualityChange(int level) {
		if (SystemInfo.graphicsShaderLevel >= 30) {
			switch (level) {
			case 0:
				GetComponent<AntialiasingAsPostEffect>().enabled = false;
				GetComponent<SSAOEffect>().enabled = false;
				break;
			case 1:
				GetComponent<AntialiasingAsPostEffect>().enabled = true;
				GetComponent<SSAOEffect>().enabled = false;
				break;
			case 2:
				GetComponent<AntialiasingAsPostEffect>().enabled = true;
				GetComponent<SSAOEffect>().enabled = true;
				break;
			}
		}
	}
}
