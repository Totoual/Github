using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollisionForProps : MonoBehaviour {
	public Transform originalTransform;
	public Transform birdOriginalTransform;
	public Transform originalHumanTransform;
    public Transform copsOriginalTransform;

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "props") {
			ReturnObject (col, originalTransform);
		} else if (col.gameObject.tag == "bgBirds") {
			ReturnObject (col, birdOriginalTransform);
			ObjectPooling.SharedInstance.ShufledList (ObjectPooling.SharedInstance.birdsPooled);
		} else if (col.gameObject.tag == "humans" || col.gameObject.tag == "granny") {
			ReturnObject (col, originalHumanTransform);
			ObjectPooling.SharedInstance.ShufledList (ObjectPooling.SharedInstance.humanPooled);
		} else if (col.gameObject.tag == "cops") {
			col.gameObject.GetComponent<Rigidbody2D> ().simulated = false;
			//GameManager.instance.CopsStopFollowing ();
			//GameManager.instance.lightsHolder.SetActive (false);
			// Here we have to find every child of this item in hierarchy and enable it before disable the hole object
		} else if (col.gameObject.tag == "killerCop") {
			ReturnObject (col, copsOriginalTransform);
			ObjectPooling.SharedInstance.ShufledList (ObjectPooling.SharedInstance.enemyBirdsPooled);
		} else if (col.gameObject.tag == "killerBird" || col.gameObject.tag == "powerUp") {
			//Debug.Log ("Helloooooo");
			ReturnObject (col, copsOriginalTransform);
			ObjectPooling.SharedInstance.ShufledList (ObjectPooling.SharedInstance.enemyBirdsPooled);
			col.gameObject.transform.localScale = new Vector3 (-1, 1, 1);
		} else if (col.gameObject.tag == "corn" || col.gameObject.tag == "noCops") {
			ReturnObject (col, copsOriginalTransform);
		} else if (col.gameObject.tag == "killerBoss") {
			Debug.Log ("Boss Collided");
			col.gameObject.SetActive (false);
		}

	}
		


	protected void ReturnObject(Collider2D col,Transform parent){
		col.gameObject.SetActive (false);
		col.gameObject.transform.SetParent (parent);
		col.gameObject.transform.localPosition = Vector2.zero;
		col.gameObject.transform.localScale = new Vector3 (1, 1, 1);

	}

}
