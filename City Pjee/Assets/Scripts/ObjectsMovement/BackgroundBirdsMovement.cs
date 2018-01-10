using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBirdsMovement : MonoBehaviour {
	[SerializeField]private float objectSpeedToRight;
	[SerializeField]private float objectSpeedToLeft;

	public GameObject parent;
	// Use this for initialization
	void OnEnable () {
		if (GameManager.instance.GameOver != true && GameManager.instance.PlayerActive) {
			objectSpeedToLeft = Random.Range (-6, -9);
		} else {
			objectSpeedToLeft = Random.Range (-2,-3.9f);
		}

		objectSpeedToRight = Random.Range (4, 6);
	}
	
	// Update is called once per frame
	void Update () {
		if (parent.tag == "toRight") {
			transform.Translate (Vector2.right * (objectSpeedToRight * Time.deltaTime));
		} else if (parent.tag == "toLeft") {
			transform.Translate (Vector2.right * (objectSpeedToLeft * Time.deltaTime));
		}
	}
}
