using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopsBehavior : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;



	void OnEnable(){
		transform.position = new Vector3 (-12,0,0);
		for (int i = 0; i < 3; i++) {
			CallTheCops.CallMeOver.positionsForCops[i] = false;
		}
	}


	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= -9) {
			this.transform.Translate (Vector2.right * Time.deltaTime * 2f);
			this.transform.position = new Vector2 (this.transform.position.x, player.transform.position.y);
		}else{
			this.transform.Translate (Vector2.right * Time.deltaTime*0.1f);
			this.transform.position = new Vector2 (this.transform.position.x, player.transform.position.y);
		}
	}



}
