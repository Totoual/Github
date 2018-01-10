using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopsCollisionWithPlayer : MonoBehaviour {
	public int id;
	public GameObject copsHolder;

	void OnEnable(){
		copsHolder = GameObject.FindGameObjectWithTag ("cops");
	}
	public void OnCollisionEnter2D	(Collision2D col){
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<CircleCollider2D>().enabled = false;
			GameManager.instance.Death ();
			copsHolder.GetComponent<AudioSource> ().clip = copsHolder.GetComponent<copsSounds> ().playerCaughtClip;
			copsHolder.GetComponent<AudioSource> ().PlayOneShot (copsHolder.GetComponent<AudioSource> ().clip);
		}

		if (col.gameObject.tag == "killerBird" || col.gameObject.tag == "killerBoss") {
			if (col.gameObject.tag != "killerBoss") {
				//Debug.Log ("I collided with the boss");
				col.gameObject.GetComponent<Rigidbody2D> ().simulated = false;
			}
			PlaySound (copsHolder.GetComponent<copsSounds> ().collisionSoundClip);
			GameManager.instance.fightCloud.transform.position = this.gameObject.transform.position;
			GameManager.instance.fightCloud.SetActive (true);
			CallTheCops.CallMeOver.ReturnTheCop (this.gameObject,col.gameObject);

		}

	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "granny") {
			PlaySound (copsHolder.GetComponent<copsSounds> ().collisionSoundClip);
			GameManager.instance.fightCloud.transform.position = this.gameObject.transform.position;
			GameManager.instance.fightCloud.SetActive (true);
			CallTheCops.CallMeOver.ReturnTheCop (this.gameObject,col.gameObject);
		}
	}

	public void PlaySound(AudioClip clip){
		this.GetComponent<AudioSource> ().clip = clip;
		this.GetComponent<AudioSource> ().PlayOneShot (GetComponent<AudioSource> ().clip);
	}



}
