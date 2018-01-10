using UnityEngine;
using System.Collections;

public class Vendor : Interactable {

	public GameObject vendorPanel;
	public BoxCollider triggerBoxCollider;

	private bool playedOnce = false;

	public override void Interact(){
		Debug.Log ("Interacting with Vendro");
		vendorPanel.SetActive (true);
		GetComponent<ButtonMusicPlayer> ().PlayVendorGreetingClip ();
		triggerBoxCollider.enabled = true;
		playedOnce = false;
	}


	public void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player" && !playedOnce) {
			vendorPanel.SetActive (false);
			GetComponent<ButtonMusicPlayer> ().PlayVendorFarewellClip ();
			triggerBoxCollider.enabled = false;
			playedOnce = true;
		}
	}
}
