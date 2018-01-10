using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheCars : MonoBehaviour {
	public Transform carHolderToRight;
	public Transform carHolderToLeft;

	private bool temp = false;

	// Use this for initialization
	void Start () {
		StartCoroutine (CarSpawn ());

	}

	void Update(){
		if (GameManager.instance.RewardedLife && !temp) {
			StartCoroutine (CarSpawn ());
			temp = true;
		}
	}

	IEnumerator CarSpawn(){
		if (!GameManager.instance.GameOver) {	
			GameObject car = ObjectPooling.SharedInstance.GetPooledObject ();
			if (car != null) {
				int randNum = Random.Range (0, 2);
				if (randNum == 0) {
					car.transform.SetParent (carHolderToRight);
					car.GetComponent<CarMovement> ().parent = carHolderToRight.gameObject;
				} else {
					car.transform.SetParent (carHolderToLeft);
					car.GetComponent<CarMovement> ().parent = carHolderToLeft.gameObject;
					car.transform.localScale = new Vector3 (-1, 1, 1);
				}

				car.transform.localPosition = Vector2.zero;
				car.SetActive (true);
			}
			float randomSec = Random.Range (3, 15);
			yield return new WaitForSeconds (randomSec);
			StartCoroutine (CarSpawn ());

		}
	}

}
