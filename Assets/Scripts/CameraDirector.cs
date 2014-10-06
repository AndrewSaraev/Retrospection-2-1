using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FieldOfView))]
public class CameraDirector : MonoBehaviour {

	static public CameraDirector main;

	public AudioSource chair;

	public float speed;

	public enum Spot { Table, Terminal, Reading, Instruction };
	static Spot state = Spot.Reading;
	static public Spot State {
		get {
			return state;
		}
		set {
			if (!main.chair.isPlaying) {
				if (value == Spot.Table && state == Spot.Terminal) {
					main.chair.pitch = 0.9f;
					main.chair.time = 0f;
					main.chair.Play();
				}
				if (value == Spot.Terminal && state == Spot.Table) {
					main.chair.pitch = 1.1f;
					main.chair.time = 0f;
					main.chair.Play();
				}
			}
			state = value;
			switch (state) {
			case Spot.Table:
				main.GetComponent<SlerpTransform>().AddTarget(main.tableCamera.transform.position, main.tableCamera.transform.rotation, main.speed);
				main.camera.eventMask = 1;
				Mouse.ClearOverride();
				break;
			case Spot.Terminal:
				main.GetComponent<SlerpTransform>().AddTarget(main.terminalCamera.transform.position, main.terminalCamera.transform.rotation, main.speed);
				main.camera.eventMask = 1 << LayerMask.NameToLayer("Archive");
				break;
			case Spot.Instruction:
				main.GetComponent<SlerpTransform>().AddTarget(main.instructionCamera.transform.position, main.instructionCamera.transform.rotation, main.speed);
				main.camera.eventMask = 1 << LayerMask.NameToLayer("Archive");
				break;
			case Spot.Reading:
				main.GetComponent<SlerpTransform>().AddTarget(main.readingCamera.transform.position, main.readingCamera.transform.rotation, main.speed);
				main.camera.eventMask = 1 << LayerMask.NameToLayer("Reading");
				Mouse.Override(Mouse.Mode.ZoomOut);
				break;
			}
		}
	}
	
	public FieldOfView tableCamera;
	public FieldOfView terminalCamera;
	public FieldOfView instructionCamera;
	public FieldOfView readingCamera;

	void Awake() {
		main = this;
		main.camera.eventMask = 1 << LayerMask.NameToLayer("Reading");
		Mouse.Override(Mouse.Mode.ZoomOut);
	}
	
}