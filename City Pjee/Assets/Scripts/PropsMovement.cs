using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsMovement : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x <= 28f) {
			transform.Translate (Vector2.right * Time.deltaTime * speed);
		} else if (transform.localPosition.x >= 28f && enabled == true) {
			this.gameObject.SetActive (false);
		}
	}
}
