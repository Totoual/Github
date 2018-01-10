using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

	private AnimationHandler animHandler;

	void Start(){
		animHandler = GameObject.Find ("EventSystem").GetComponent<AnimationHandler> ();
	}


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Obstacle") {
			animHandler.Death ();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("You took 10 points");
		if (col.gameObject.tag == "Ground")
			animHandler.score += 10;
	}
}
