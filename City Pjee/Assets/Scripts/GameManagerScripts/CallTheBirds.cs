using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTheBirds : MonoBehaviour {
	public Transform rightHolder;
	public Transform leftHolder;


	private bool temp = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (BirdsSpawn ());
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.RewardedLife && !temp) {
			StartCoroutine (BirdsSpawn ());
			temp = true;
		}
	}

	IEnumerator BirdsSpawn(){
		if (!GameManager.instance.GameOver) {
			GameObject bird = ObjectPooling.SharedInstance.GetPooledBird ();
			int randNum = Random.Range (0, 2);
			if (randNum == 0) {
				bird.transform.SetParent (rightHolder);
				bird.GetComponent<BackgroundBirdsMovement> ().parent = rightHolder.gameObject;
				bird.transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				bird.GetComponent<BackgroundBirdsMovement> ().parent = leftHolder.gameObject;
				bird.transform.SetParent (leftHolder);
			}
			bird.transform.localPosition = new Vector2 (0, Random.Range (-8, 5));
			bird.SetActive (true);
			float rndSec = Random.Range (GameManager.instance.minTime, GameManager.instance.maxTime);
			yield return new WaitForSeconds (rndSec);
			StartCoroutine (BirdsSpawn ());
		}
	}
}
