using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	private AnimationHandler animHandler;

	void Start(){
		animHandler = GameObject.Find ("EventSystem").GetComponent<AnimationHandler>();
	}


	public void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			animHandler.Death ();
		}
	}
}
