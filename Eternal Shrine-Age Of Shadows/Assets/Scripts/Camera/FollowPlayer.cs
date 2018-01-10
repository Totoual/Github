using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public GameObject player;
	public Vector3 cameraOffset;
	public Vector3 temp;
	//public Terrain mTerrain;
	// Use this for initialization
	void Start () {
		this.GetComponent<FollowPlayer> ().enabled = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		this.GetComponent<FollowPlayer> ().enabled = true;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		CalcualteCameraMove ();

	}

	void CalcualteCameraMove(){
		temp = player.transform.position + cameraOffset;
		transform.position = Vector3.Lerp (transform.position, temp,0.2f);
	}


}
