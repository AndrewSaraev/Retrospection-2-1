using UnityEngine;
using System.Collections;

public class Tape : MonoBehaviour {
	
	public Material tapeMaterial;
	public Material sideTapeMaterial;
	public Texture2D stateTexture;

	Color[] stateColors = new Color[38];

	float position;
	public float Position {
		get {
			return position;
		}
		set {
			position = Mathf.Repeat(value, 19f);
			sideTapeMaterial.mainTextureOffset = new Vector2(position/19f, 0f);
			tapeMaterial.mainTextureOffset = new Vector2(position/19f, 0f);
			tapeMaterial.SetTextureOffset("_State", new Vector2(position/19f, 0f));
			tapeMaterial.SetTextureOffset("_Pattern", new Vector2(position, 0f));
		}
	}

	public bool isMoving;
	int startingPosition;
	float movingPhase;
	public float tapeSpeed = 4;
	bool isDirty = false;

	public AudioClip sound;

	public void Print(int index, Color color) {
		if (index == 0) {
			if (Mathf.RoundToInt(Position) > 1) {
				stateColors[Mathf.RoundToInt(Position)-2] = color;
			}
			else {
				stateColors[Mathf.RoundToInt(Position)+17] = color;
			}
		}
		else {
			if (Mathf.RoundToInt(Position) > 1) {
				stateColors[Mathf.RoundToInt(Position)+17] = color;
			}
			else {
				stateColors[Mathf.RoundToInt(Position)+36] = color;
			}
		}
//		stateTexture.SetPixel(Mathf.RoundToInt(Position)+17, index, color);
		isDirty = true;
	}

	public void SingleMove() {
		if (!isMoving) {
			audio.PlayOneShot(sound);
			startingPosition = Mathf.RoundToInt(Position);
			movingPhase = 0f;
			isMoving = true;
			ArchiveButton.blockSources++;
			stateColors[startingPosition] = Color.black;
			stateColors[startingPosition+19] = Color.black;
//			stateTexture.SetPixels(startingPosition, 0, 1, 2, new Color[] { Color.black, Color.black });
			isDirty = true;
		}
	}

	void Start () {
		Position = 0f;

		stateTexture = new Texture2D(19, 2, TextureFormat.RGB24, false, true);
		stateTexture.filterMode = FilterMode.Point;
		stateTexture.wrapMode = TextureWrapMode.Repeat;
		stateTexture.SetPixels(new Color[38]);
		stateTexture.Apply();
		tapeMaterial.SetTexture("_State", stateTexture);
	}
	
	void Update () {
		if (isDirty) {
			stateTexture.SetPixels(stateColors);
			stateTexture.Apply();
		}
		if (isMoving) {
			movingPhase += Time.deltaTime * tapeSpeed;
			if (movingPhase >= 1f) {
				movingPhase = 1f;
				isMoving = false;
				ArchiveButton.blockSources--;
			}
			Position = startingPosition + movingPhase * movingPhase;
		}
	}
}
