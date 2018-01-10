using UnityEngine;
using System.Collections;

public class HandleCollision : MonoBehaviour {

	public void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {

			GetComponent<AIPath> ().following = true;
			GetComponent<AIPath> ().speed = 2f;
		}
	}
}
