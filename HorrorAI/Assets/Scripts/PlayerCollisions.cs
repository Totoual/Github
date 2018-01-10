using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {
	private GameManager gm;

	void Start(){
		gm = GameObject.Find ("_GameManager").GetComponent<GameManager> ();
	}


	public void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Zombie") {
			gm.IsTouched ();
		}
	}
}
