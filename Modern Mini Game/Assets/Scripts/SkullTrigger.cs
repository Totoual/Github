using UnityEngine;
using System.Collections;

public class SkullTrigger : MonoBehaviour {

	public void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "skull") {
			//Debug.Log (col.gameObject.tag);
			GameManager.instance.EnableSkullComponents ();
		
		}
	}
}
