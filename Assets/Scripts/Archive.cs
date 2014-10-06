using UnityEngine;
using System.Collections;
using System.Xml;

public class Archive : MonoBehaviour {

	public ArchiveButton clearButton;
	public ArchiveButton enterButton;
	public ArchiveButton button0;
	public ArchiveButton button1;
	public ArchiveButton button2;

	public TextAsset codebase;
	XmlDocument xml = new XmlDocument();
	XmlNode activeNode;

	public string code;

	public AudioClip[] audioRecords;
	AudioClip GetClip(string clipName) {
		for (int i = 0; i < audioRecords.Length; i++) {
			if (audioRecords[i] != null && audioRecords[i].name == clipName) {
				return audioRecords[i];
			}
		}
		return null;
	}

	void Clear() {
		code = string.Empty;
	}

	void Enter() {
		switch (code) {
		case "":
			break;
		case "0":
			XmlNode parent = activeNode.ParentNode;
			if (activeNode != xml.DocumentElement) {
				activeNode = parent;
			}
			LoadActiveNode();
			break;
		case "00":
			activeNode = xml.DocumentElement;
			LoadActiveNode();
			break;
		default:
			XmlNode codeAttribute;
			if (!activeNode.HasChildNodes) {
				activeNode = activeNode.ParentNode;
			}
			if (activeNode != null) {
				XmlNode nextNode = null;
				foreach (XmlNode child in activeNode.ChildNodes) {
					if (child.Attributes == null) continue;
					codeAttribute = child.Attributes.GetNamedItem("code");
					if (codeAttribute != null && codeAttribute.Value == code) {
						nextNode = child;
						break;
					}
				}
				if (nextNode != null) {
					activeNode = nextNode;
					LoadActiveNode();
				}
				else {
					LoadErrorMessage();
				}
			}
			break;
		}
		Clear();
	}

	void LoadErrorMessage() {
		AudioClip newClip = null;
		XmlAttributeCollection collection = activeNode.Attributes;
		if (collection != null) {
			XmlNode clipNode = collection.GetNamedItem("error");
			if (clipNode != null) {
//				newClip = Resources.Load<AudioClip>(clipNode.Value);
				newClip = GetClip(clipNode.Value);
			}
		}
		GetComponent<Speaker>().LoadSound(newClip);
	}

	void LoadActiveNode() {
		AudioClip newClip = null;
		XmlAttributeCollection collection = activeNode.Attributes;
		if (collection != null) {
			XmlNode clipNode = collection.GetNamedItem("audio");
			if (clipNode != null) {
//				newClip = Resources.Load<AudioClip>(clipNode.Value);
				newClip = GetClip(clipNode.Value);
			}
		}
		GetComponent<Speaker>().LoadSound(newClip);
	}

	void Print0() {
		code += '0';
	}

	void Print1() {
		code += '1';
	}

	void Print2() {
		code += '2';
	}

	void Start () {
		clearButton.onRelease += Clear;
		enterButton.onRelease += Enter;
		button0.onRelease += Print0;
		button1.onRelease += Print1;
		button2.onRelease += Print2;

		xml.LoadXml(codebase.text);
		activeNode = xml.DocumentElement;
	}
}