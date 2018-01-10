using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBorderCollision : MonoBehaviour {
	public Transform shitHolder;
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "ammo") {
			col.gameObject.SetActive (false);
			col.gameObject.transform.SetParent (shitHolder);
			col.gameObject.transform.localPosition = Vector2.zero;
		}

		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			GameManager.instance.Death ();
		}
	}
		


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
