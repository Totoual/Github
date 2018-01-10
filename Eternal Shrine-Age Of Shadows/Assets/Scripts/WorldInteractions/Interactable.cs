using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	public NavMeshAgent playerAgent;
	private bool hasInteracted;

	public virtual void MoveToIntreraction(NavMeshAgent playerAgent){

		hasInteracted = false;
		this.playerAgent = playerAgent;
		playerAgent.stoppingDistance = 3f;
		playerAgent.destination = this.transform.position;

	}

	void Update(){
		if(playerAgent != null && !playerAgent.pathPending){
			if(!hasInteracted && playerAgent.remainingDistance <= playerAgent.stoppingDistance ){
				Interact ();
				hasInteracted = true;
			}
		}

	
	}
	public void CalculateDistance(){
		
	}


	public virtual void Interact(){
		Debug.Log ("Interacting with base class.");
	}
}
