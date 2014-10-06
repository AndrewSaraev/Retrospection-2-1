using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	static Mouse _main;
	public enum Mode { Arrow, ZoomIn, Hand, Grab, ZoomOut };
	static Mode _hovered = Mode.Arrow;
	static Mode _actual = Mode.Arrow;
	static bool _isOverriden = false;

	static public Mode Hovered {
		get {
			return _hovered;
		}
		set {
			_hovered = value;
		}
	}

	static public void Override(Mode cursor) {
		if (_main != null) {
			_main.SetCursor(cursor);
		}
		_isOverriden = true;
	}

	static public void ClearOverride() {
		_isOverriden = false;
	}



	public Texture2D arrow;
	public Texture2D glass;
	public Texture2D grab;
	public Texture2D hand;
	public Texture2D zoomOut;



	void OnDisable() {
		SetCursor(Mode.Arrow);
		_main = null;
	}

	void OnEnable() {
		_main = this;
	}

	void SetCursor(Mode cursor) {
		switch(cursor) {
			case Mode.Arrow:
				Cursor.SetCursor(arrow, new Vector2(1f, 1f), CursorMode.Auto);
				break;
			case Mode.ZoomIn:
				Cursor.SetCursor(glass, new Vector2(11f, 11f), CursorMode.Auto);
				break;
			case Mode.Grab:
				Cursor.SetCursor(grab, new Vector2(13f, 16f), CursorMode.Auto);
				break;
			case Mode.Hand:
				Cursor.SetCursor(hand, new Vector2(13f, 16f), CursorMode.Auto);
				break;
			case Mode.ZoomOut:
				Cursor.SetCursor(zoomOut, new Vector2(11f, 11f), CursorMode.Auto);
				break;
			default:
				break;
		}
		_actual = cursor;
	}

	void Update() {
		if (!_isOverriden && _hovered != _actual) {
			SetCursor(_hovered);
		}
	}

}
