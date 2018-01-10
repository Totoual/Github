using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour {

	public GameObject button;					// Pernoume to koubi pou theloume
	public TextTyper textyper;					// Pernoume to keimeno pou exoume sto inspector kai kanei type
	// Use this for initialization
	void Start () {
		button.GetComponent<Button> ().interactable = true;			// Orizoume oti to koubi den einai interactable
	}
	
	// Update is called once per frame
//	void Update () {
//		if (textyper.isEnded) {											// Elegxoume an i coroutine exei teleiwsei etsi wste na energopoiisoume to koubi pou theloume na einai interactable.
//			button.GetComponent<Button> ().interactable = true;
//		}
//	}
}
