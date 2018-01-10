using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour {
	public Transform cornParent;
	public GameObject anarchistBoss;
	public Transform bossOriginalTransf;
	public GameObject mainCamera;


	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "killerCop") {
			CallTheCops.CallMeOver.CallMoreCops ();
            GetComponent<Player> ().timer = 0;
		}
		if (col.gameObject.tag == "killerCop" || col.gameObject.tag == "killerBird") {
			GetComponent<Player> ().counter = 0;
			GetComponent<AudioSource> ().Play ();
			GetComponent<Player> ().timer = 0;
		}
		if (col.gameObject.tag == "corn") {
			GameManager.instance.AddToCorns ();
			col.gameObject.SetActive (false);
			col.gameObject.transform.SetParent (cornParent);
			col.gameObject.transform.localPosition = Vector2.zero;
			col.gameObject.transform.localScale = new Vector3 (1, 1, 1);
		}
		if (col.gameObject.tag == "noCops") {
			anarchistBoss.transform.position = bossOriginalTransf.position;
			anarchistBoss.SetActive (true);
			mainCamera.GetComponent<CameraShake> ().enabled = true;
			col.gameObject.SetActive (false);
			col.gameObject.transform.SetParent (cornParent);
			col.gameObject.transform.localPosition = Vector2.zero;
			col.gameObject.transform.localScale = new Vector3 (1, 1, 1);
		}

    }
}
