using UnityEngine;
using System.Collections;

public class Folder : MonoBehaviour {

	public Control openControl;
	public Control closeControl;

	public SlerpTransform fold;
	public SlerpTransform[] papers;
	public Transform[] inFolder;
	public Transform[] onTable;

	public AudioClip openSound;
	public AudioClip closeSound;

	void Distribute() {
		for (int i = 0; i < papers.Length; i++) {
			papers[i].GetComponent<Paper>().CancelInvoke();
			papers[i].AddTarget(onTable[i].position, onTable[i].rotation, 2f);
			papers[i].GetComponent<Control>().enabled = true;
		}
	}

	void Close() {
		fold.AddTarget(new Vector3(7.5f, 0.03f, 0f), new Quaternion(0f, 0f, -0.01745241f, 0.9998477f), 1.5f);
	}

	void Collect() {
		audio.PlayOneShot(closeSound);
		CancelInvoke();
		for (int i = 0; i < papers.Length; i++) {
			papers[i].GetComponent<Paper>().CancelInvoke();
			papers[i].AddTarget(inFolder[i].position, inFolder[i].rotation, 2f);
			papers[i].GetComponent<Control>().enabled = false;
		}
		Invoke("Close", 0.2f);
	}
	
	void Open() {
		audio.PlayOneShot(openSound);
		CancelInvoke();
		fold.AddTarget(new Vector3(7.5f, 0.03f, 0f), new Quaternion(0f, 0f, 1f, 0f), 1.5f);
		Invoke("Distribute", 0.35f);
	}

	void OnEnable() {
		openControl.onClick += Open;
		closeControl.onClick += Collect;
	}
}
