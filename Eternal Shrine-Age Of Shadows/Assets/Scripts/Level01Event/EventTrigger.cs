using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {

	public MoveNPCtoCharacter skeleton;




	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			skeleton.triggered = true;
		}
	}
}
