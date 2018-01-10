using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyCollision : MonoBehaviour {

	private GameObject expl;
	public float radius = 5.0f;
	public float power = 10.0f;


	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "humans") {
			GameManager.instance.AddToScore (1);
			ReturnToHolder (col);
		} else if (col.gameObject.tag == "granny") {
			GameManager.instance.AddToScore (2);
			ReturnToHolder (col);
		} else if (col.gameObject.tag == "killerBird") {
			ReturnToHolder (col);
			col.gameObject.SetActive (false);
			col.gameObject.transform.localPosition = new Vector2 (0, 0);
			col.gameObject.transform.localScale = new Vector3 (-1, 1, 1);
			ObjectPooling.SharedInstance.ShufledList (ObjectPooling.SharedInstance.enemyBirdsPooled);
			CheckForCorns ();
		} else if (col.gameObject.tag == "killerCop") {
			GameManager.instance.AddToScore (3);
			ReturnToHolder (col);
			col.gameObject.SetActive (false);
			col.gameObject.transform.localPosition = new Vector2 (0, 0);
			CallTheCops.CallMeOver.CallMoreCops ();
			ObjectPooling.SharedInstance.ShufledList (ObjectPooling.SharedInstance.enemyBirdsPooled);
		} else if (col.gameObject.tag == "powerUp") {
			ReturnToHolder (col);
			col.gameObject.SetActive (false);
			col.gameObject.transform.localPosition = new Vector2 (0, 0);
			col.gameObject.transform.localScale = new Vector3 (-1, 1, 1);
			GameManager.instance.powerUp = true;
			ObjectPooling.SharedInstance.ShufledList (ObjectPooling.SharedInstance.enemyBirdsPooled);
			if(GameManager.instance.environmentSpeed <= 6) {
				//Debug.Log ("I add to score");
				GameManager.instance.AddToScore (5);
			}
		}

		GameManager.instance.AddToScorePoints ();
	}
	 
	public void ReturnToHolder(Collider2D col){
		expl = ObjectPooling.SharedInstance.GetPooledExpl ();
		expl.transform.position = col.gameObject.transform.position;
		expl.SetActive (true);
		this.transform.SetParent (GameObject.FindGameObjectWithTag ("shitHolder").transform);
		this.transform.localPosition = Vector2.zero;
		this.gameObject.SetActive (false);
		expl.GetComponent<AudioSource> ().PlayOneShot (expl.GetComponent<AudioSource> ().clip);
	}

	public void CheckForCorns(){
		float rnd = Random.Range (0, 1.1f);
		if (rnd > 0 && rnd < 0.6f) {
			GameManager.instance.GetComponent<CallTheCorns> ().CallTheCorn (1);
		} else if (rnd >= 0.6f && rnd <= 0.8f) {
			GameManager.instance.GetComponent<CallTheCorns> ().CallTheCorn (2);
		}
		//Debug.Log ("I hit a bird " + rnd);
	}

}
