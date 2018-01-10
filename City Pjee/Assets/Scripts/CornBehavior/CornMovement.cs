using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.left * (GameManager.instance.cornSpeed * Time.deltaTime));
	}
}
