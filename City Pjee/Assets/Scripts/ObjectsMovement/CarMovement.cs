using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {
	public float objectSpeedToRight = 10;
	public float objectSpeedToLeft = 30;
	public float resetPosition;
	public float startPosition;

	public Transform resetPos;

	public GameObject parent;

	void Update () {

			if (parent.tag == "toRight") {
				transform.Translate (Vector2.right * (GameManager.instance.carSpeed * Time.deltaTime));
			} else if (parent.tag == "toLeft") {
			transform.Translate (Vector2.right * (-(GameManager.instance.carSpeed*1.5f) * Time.deltaTime));
			}
		}	
	}

