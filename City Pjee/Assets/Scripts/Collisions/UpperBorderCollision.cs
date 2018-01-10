using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperBorderCollision : MonoBehaviour {
	public GameObject stars;
	public float timeInSeconds;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col){
			if (col.gameObject.tag == "Player") {
			GameManager.instance.stunned = true;
			StartCoroutine (TimerToEnableBack ());
			stars.SetActive (true);
		}
	 }

	public IEnumerator TimerToEnableBack(){
		yield return new WaitForSeconds (timeInSeconds);
		GameManager.instance.stunned = false;
		stars.SetActive (false);
	}

}


