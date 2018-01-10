using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "humans" || col.gameObject.tag == "granny") {
			//col.gameObject.GetComponent<Rigidbody2D> ().simulated = true;
			//Debug.Log ("Fucking Explosion YEAHHH");
			Vector2 target = col.gameObject.transform.position;
			Vector2 bomb = this.transform.position;
			//Debug.Log (target - bomb);
			Vector2 direction = new Vector2 ((target.x-bomb.x), (target.y-bomb.y)+1)*2000;
			col.gameObject.GetComponent<Rigidbody2D> ().AddForce (direction,ForceMode2D.Force);
		}
		StartCoroutine (CountDown ());
		if (col.gameObject.tag == "killerBird"){
			Vector2 bomb = this.transform.position;
		}
	
	}



	IEnumerator CountDown(){
		yield return new WaitForSeconds (0.8f);

		this.gameObject.SetActive(false);
		this.transform.localPosition = Vector2.zero;
	}
}
