using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	public GameManager gm;

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Ground") {
			Destroy (gm.inGameObject [0].gameObject);
			gm.inGameObject.Remove (gm.inGameObject [0].gameObject);
			gm.InstantiateLastItem ();
		}
	}
}
