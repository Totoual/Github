using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTheCorns : MonoBehaviour {
	public void CallTheCorn(int id){
		if (!GameManager.instance.GameOver) {
			if (id == 1) {
				GameObject corn = ObjectPooling.SharedInstance.SerachPooledCorns ();
				corn.transform.position = new Vector3 (corn.transform.position.x, Random.Range (3.32f, -4f), corn.transform.position.z);
				corn.SetActive (true);
			} else if (id == 2) {
				GameObject corn = ObjectPooling.SharedInstance.SearchForNoCops ();
				corn.transform.position = new Vector3 (corn.transform.position.x, Random.Range (3.32f, -4f), corn.transform.position.z);
				corn.SetActive (true);
			}
		}
	}
}
