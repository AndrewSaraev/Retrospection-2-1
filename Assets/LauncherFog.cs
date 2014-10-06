using UnityEngine;
using System.Collections;

public class LauncherFog : MonoBehaviour {

	public Material fog;
	public float speed;
	public float aspect;

	Vector2 offset;

	void Update () {
		offset.x = Mathf.Repeat(offset.x + Time.deltaTime * speed, 1f);
		offset.y = Mathf.Repeat(offset.y + Time.deltaTime * speed * aspect, 1f);
		fog.mainTextureOffset = offset;
	}
}
