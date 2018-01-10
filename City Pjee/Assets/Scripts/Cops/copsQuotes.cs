using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class copsQuotes : MonoBehaviour {
	public List<Sprite> quotes = new List<Sprite>();
	public GameObject copsHolder ;
	public Vector3 offset;

	private float timer =0;
	private bool runnedOnce = false;
	// Use this for initialization
	void Start () {
		//StartCoroutine (TellTheQuotes ());
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = copsHolder.transform.position + offset;
		if (copsHolder.transform.childCount < 1) {
			GetComponent<SpriteRenderer> ().sprite = quotes [Random.Range (0, 5)];
			this.GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			if (timer <= 10) {
				this.GetComponent<SpriteRenderer> ().enabled = true;
				timer += Time.deltaTime;
			} else{
				GetComponent<SpriteRenderer> ().enabled = false;
				GetComponent<SpriteRenderer> ().sprite = quotes [Random.Range (0, 5)];
				if (!runnedOnce) {
					runnedOnce = true;
					StartCoroutine (TellTheQuotes ());
				}
			}
		}
		//Debug.Log (timer);
	}


	public IEnumerator TellTheQuotes(){
		yield return new WaitForSeconds (5);
		timer = 0;
		runnedOnce = false;
	}
}
