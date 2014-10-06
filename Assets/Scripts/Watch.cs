using UnityEngine;
using System.Collections;

public class Watch : MonoBehaviour {

	public Transform hours;
	public Transform minutes;
	public Transform seconds;

	void Start () {
		Update();
	}
	
	void Update () {
		hours.localEulerAngles = new Vector3(0f, 0f, System.DateTime.Now.Hour * 30f + System.DateTime.Now.Minute * 0.5f + System.DateTime.Now.Second * 0.008333334f);
		minutes.localEulerAngles = new Vector3(0f, 0f, System.DateTime.Now.Minute * 6f + System.DateTime.Now.Second * 0.1f);
		seconds.localEulerAngles = new Vector3(0f, 0f, System.DateTime.Now.Second * 6f);
	}
}
