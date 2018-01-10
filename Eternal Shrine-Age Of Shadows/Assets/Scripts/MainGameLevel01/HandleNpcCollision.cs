using UnityEngine;
using System.Collections;

public class HandleNpcCollision : MonoBehaviour {

	public GameObject canvas;
		
	public void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "MoveOut" && GetComponent<MoveNPCtoCharacter>().canKill) {
			//Debug.Log ("I can kill my self");
			Destroy (this.gameObject);
			canvas.GetComponent<DialogBoxPositioning> ().enabled = false;
			GetComponent<MoveNPCtoCharacter> ().dialogBox.SetActive (false);
		}
	}
		
}
