using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveNPCtoCharacter : MonoBehaviour {
	NavMeshAgent npcNavMesh;
	public GameObject player;
	public bool triggered = false;
	public Transform moveOutPosition;
	public GameObject dialogBox;
	public bool canKill=false;
	private bool flag = true;
	private bool canCalculate= false;
	// Use this for initialization


	void Start () {
		npcNavMesh = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		dialogBox.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (triggered) {
			MoveNPCto ();
		}
		if (dialogBox.transform.GetChild (0).GetComponent<TextTyper> ().message.Length == dialogBox.transform.GetChild (0).GetComponent<Text> ().text.Length && flag) {
			//Debug.Log ("You can call the other function");
			ChangeDirection ();
		}
		if (canCalculate) {
			//Debug.Log (" I am calculating");
			CalculateDistance ();
		}


	}

	void MoveNPCto(){
		npcNavMesh.destination = player.transform.position - new Vector3(-10,0,0);
		if (Vector3.Distance (player.transform.position, this.transform.position) <= 20f) {
			//dialogBox.SetActive (true);
			if (Vector3.Distance(this.transform.position,player.transform.position) <= 10f) {
				dialogBox.SetActive (true);
				dialogBox.transform.GetChild (0).GetComponent<TextTyper> ().StartCoroutine ("TypeText");
				//Debug.Log ("It's alive");
				triggered = false;
				canCalculate = true;
			}		
					
		}
	}

	void ChangeDirection(){
		npcNavMesh.destination = moveOutPosition.position;
		npcNavMesh.stoppingDistance = 0.2f;
		flag = false;
		//Debug.Log (Vector3.Distance (this.transform.position, moveOutPosition.position));
	}
	void CalculateDistance(){
		if(Vector3.Distance (this.transform.position, moveOutPosition.position)<=5f){
			canKill = true;
		}
	}
}
