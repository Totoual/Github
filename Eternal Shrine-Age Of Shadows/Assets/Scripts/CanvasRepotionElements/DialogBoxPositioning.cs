using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogBoxPositioning : MonoBehaviour {

	public GameObject WorldObject;
	public RectTransform UI_Element;
	public RectTransform CanvasRect;
	public float offsetY;

	// Use this for initialization
	void Start () {
		CanvasRect = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CalculatePosition ();
	}

	void CalculatePosition(){
		Vector2 ViewPortPosition = Camera.main.WorldToViewportPoint (WorldObject.transform.position);
		Vector2 WorldObject_ScreenPosition = new Vector2 (
			                                     ((ViewPortPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
			                                     ((ViewPortPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
		UI_Element.anchoredPosition = WorldObject_ScreenPosition + new Vector2(0,offsetY);
	}
}
