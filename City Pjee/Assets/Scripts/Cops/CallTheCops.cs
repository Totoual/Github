using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTheCops : MonoBehaviour {
	public static CallTheCops CallMeOver;

	public List<bool> positionsForCops = new List<bool>();
	public List<Vector3> positionsToSpawn = new List<Vector3> ();
	public Transform parentTransf;
	public Transform returnParent;
	public Vector3 originalPos;
	private bool runnedOnce = false;

	void Awake(){
		if (CallMeOver == null) {
			CallMeOver = this;
		} else if (CallMeOver != this) {
			Destroy (gameObject);
		}

		for (int i = 0; i < 3; i++) {
			positionsForCops.Add (false);
		}
	}


	public void CallSomeCops(){
//		if (!GameManager.instance.GameOver) {
//			parentTransf.gameObject.SetActive (true);
//			GameObject cop = ObjectPooling.SharedInstance.SearchCops ();
//			cop.transform.SetParent(parentTransf) ;
//			cop.transform.localPosition = Vector2.zero;
//			cop.transform.localScale = new Vector3 (1, 1, 1);
//			cop.SetActive (true);
//		}
		if (!GameManager.instance.GameOver) {
			parentTransf.gameObject.SetActive (true);
			//Debug.Log (" I AM CALLING THE COPS");
			GameObject cop = ObjectPooling.SharedInstance.SearchCops ();
			cop.SetActive (true);
			cop.transform.localScale = new Vector3 (1, 1, 1);
			cop.transform.SetParent (parentTransf);
			if (ReturnFreeSlot () != -1) {
				cop.GetComponent<CopsCollisionWithPlayer> ().id = ReturnFreeSlot ();
				cop.transform.localPosition = positionsToSpawn [ReturnFreeSlot ()];
				positionsForCops [cop.GetComponent<CopsCollisionWithPlayer> ().id] = true;

			}

		}
	}

	public void CallMoreCops(){
		if (!GameManager.instance.GameOver) {
			if (ReturnFreeSlot () != -1) {
				if (parentTransf.gameObject.activeInHierarchy && parentTransf.childCount < 3 && parentTransf.childCount >= 1) {
					GameObject cop = ObjectPooling.SharedInstance.SearchCops ();
					cop.transform.localScale = new Vector3 (1, 1, 1);
					cop.SetActive (true);
					cop.transform.SetParent (parentTransf);
					if (ReturnFreeSlot () != -1) {
						cop.GetComponent<CopsCollisionWithPlayer> ().id = ReturnFreeSlot ();
						cop.transform.localPosition = positionsToSpawn [ReturnFreeSlot ()];
						positionsForCops [cop.GetComponent<CopsCollisionWithPlayer> ().id] = true;

					}

//				if (parentTransf.childCount == 1) {
//					Debug.Log ("I am here" + "" + parentTransf.childCount);
//					GameObject cop = ObjectPooling.SharedInstance.SearchCops ();
//					cop.transform.position = new Vector2 (cop.transform.position.x, 1.5f);
//					cop.SetActive (true);
//					cop.transform.localScale = new Vector3 (1, 1, 1);
//					cop.transform.SetParent (parentTransf);
//					cop.transform.localPosition = new Vector2 (-0.8f, 1.5f);
//					//cop.transform.Translate(new Vector3(-1,1.7f,0)*Time.deltaTime*4,Space.Self);
//
//				} else if (parentTransf.childCount == 2) {
//					Debug.Log ("I am here" + "" + parentTransf.childCount);
//					GameObject cop = ObjectPooling.SharedInstance.SearchCops ();
//					cop.transform.position = new Vector2 (cop.transform.position.x, -1.5f);
//					cop.SetActive (true);
//					cop.transform.localScale = new Vector3 (1, 1, 1);
//					cop.transform.SetParent (parentTransf);
//					cop.transform.localPosition = new Vector2 (-0.8f, -1.5f);
//				} else if (parentTransf.childCount < 1) {
//					Debug.Log ("I am here" + "" + parentTransf.childCount);
//					parentTransf.transform.position = originalPos;
//					CallSomeCops ();
//				}

				} else if (parentTransf.childCount < 1) {
					parentTransf.transform.position = originalPos;
					CallSomeCops ();
				}
			}
		}
	}

	public void ReturnTheCop (GameObject cop, GameObject collidedObject){
		ReturnTheCopAfterVideo (cop);
		StartCoroutine (DisableExplosion (collidedObject));
		if (!runnedOnce) {
			StartCoroutine (LetThePartyBegin ());
			runnedOnce = true;
		}
	}
	public void ReturnTheCopAfterVideo( GameObject cop){
		cop.SetActive (false);
		cop.GetComponent<AudioSource> ().clip = GameObject.FindGameObjectWithTag ("cops").GetComponent<copsSounds> ().enableSoundClip;
		positionsForCops [cop.GetComponent<CopsCollisionWithPlayer> ().id] = false;
		cop.transform.SetParent (returnParent);
		cop.transform.localScale = new Vector3 (1, 1, 1);
		cop.transform.localPosition = Vector2.zero;
		GameManager.instance.AddToScore (3);
	}
	public IEnumerator DisableExplosion(GameObject col){
		yield return new WaitForSeconds (0.3f);
		if (!(col.gameObject.tag == "killerBoss")) {
			Debug.Log ("I didn t disable the collider");
			col.gameObject.GetComponent<Rigidbody2D> ().simulated = false;
		}
		GameManager.instance.fightCloud.SetActive (false);
	}

	public int ReturnFreeSlot(){
		for (int i = 0; i < positionsForCops.Count; i++) {
			if (positionsForCops [i] == false) {
				return i;
			}
		}
		return -1;
	}

	public IEnumerator LetThePartyBegin(){
		yield return new WaitForSeconds (10);
		//Debug.Log ("I am in the fucking co-routine");
		if (parentTransf.childCount < 1) {
			CallMoreCops ();
		}
		StartCoroutine (LetThePartyBegin ());
	}

}
