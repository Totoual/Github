using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour {
	[SerializeField]private float objectSpeedToRight;
	[SerializeField]private float objectSpeedToLeft;

	public GameObject parent;
	// Use this for initialization
	void OnEnable () {
			objectSpeedToLeft = Random.Range (-(GameManager.instance.humansSpeed-2), -(GameManager.instance.humansSpeed+2));
			objectSpeedToRight = Random.Range ((GameManager.instance.humansSpeed-2), GameManager.instance.humansSpeed);
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
