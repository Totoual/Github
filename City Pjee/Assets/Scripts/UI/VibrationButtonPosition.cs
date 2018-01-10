using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationButtonPosition : MonoBehaviour {
	public GameObject onOffSlider;
	// Use this for initialization
	void OnEnable(){
		Debug.Log ("test");
		if (!GameManager.instance.vibrate) {
			onOffSlider.transform.localPosition =
				new Vector3 (-36.55f
					,onOffSlider.transform.localPosition.y
					,onOffSlider.transform.localPosition.z);
		} else {
			onOffSlider.transform.localPosition =
				new Vector3 (36.55f
					, onOffSlider.transform.localPosition.y
					, onOffSlider.transform.localPosition.z);
		}
	}
}
