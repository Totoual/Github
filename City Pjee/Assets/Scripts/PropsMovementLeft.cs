using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsMovementLeft : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x >= 0f) {
			transform.Translate (Vector2.left * Time.deltaTime * speed);
		} else if (transform.localPosition.x <= 0.0f && enabled == true) {
			this.gameObject.SetActive (false);
		}
	}
}
