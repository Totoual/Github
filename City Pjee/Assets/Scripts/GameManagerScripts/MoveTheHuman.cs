using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheHuman : MonoBehaviour {
	public Transform humanHolderLeft;
	public Transform humanHolderRight;

	private bool temp = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (HumanSpawn ());
	}

	void Update(){
		if (GameManager.instance.RewardedLife && !temp) {
			StartCoroutine (HumanSpawn ());
			temp = true;
		}
	}

	IEnumerator HumanSpawn(){
		if (!GameManager.instance.GameOver) {
			GameObject human = ObjectPooling.SharedInstance.GetPooledHuman ();
			if (human != null) {
				int rnd = Random.Range (0, 2);
				if (rnd == 0) {
					human.transform.SetParent (humanHolderLeft);
					human.GetComponent<HumanMovement> ().parent = humanHolderLeft.gameObject;

				} else {
					human.transform.SetParent (humanHolderRight);
					human.transform.localScale = new Vector3 (-1, 1, 1);
					human.GetComponent<HumanMovement> ().parent = humanHolderRight.gameObject;
				}

				human.transform.localPosition = Vector2.zero;
				human.SetActive (true);
			}

			float randomSec = Random.Range (3, 9);
			yield return new WaitForSeconds (randomSec);
			StartCoroutine (HumanSpawn ());
		}
	}
}
