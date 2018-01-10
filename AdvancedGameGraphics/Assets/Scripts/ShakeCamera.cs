using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Shakeit ());
	}
	
	IEnumerator Shakeit(){
		yield return new WaitForSeconds (4.5f);
		GetComponent<Animator> ().SetTrigger ("Shake");
	}
}
