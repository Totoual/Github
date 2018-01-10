using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	private GameObject player;
	private bool flag;

	void Start(){
		player = GetComponent<Enemy> ().player;
	}

	public void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player" && GetComponent<EnemyStats>().runnedOnce && !GetComponent<EnemyStats>().immobilised && GetComponent<EnemyStats>().isNotDead) {
			Debug.Log ("I am not stunned i can attack");
			//Debug.Log(Vector3.Distance (this.transform.position, col.gameObject.transform.position));
			if (Vector3.Distance (this.transform.position, col.gameObject.transform.position) <= 12f) {
				flag = false;
				GetComponent<EnemyStats> ().StartCoroutine ("Attack", col.gameObject);
			} 
			if(Vector3.Distance (this.transform.position, col.gameObject.transform.position) >= 5f){
				//GetComponent<NavMeshAgent> ().destination = player.transform.position;
				//GetComponent<NavMeshAgent> ().stoppingDistance = 3f;
				flag = true;
				Debug.Log (flag);
			}


		}
	}

	public void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			flag = true;
		}
	}

	void Update(){
		
		if (flag && GetComponent<EnemyStats>().isNotDead) {
			if((Vector3.Distance(player.transform.position,this.transform.position)>=5f) && !GetComponent<EnemyStats>().immobilised){
				GetComponent<Animation> ().Play (animation: "run");
			}

			GetComponent<NavMeshAgent> ().destination = player.transform.position;
			GetComponent<NavMeshAgent> ().stoppingDistance = 3f;
		}
	}

//	public void OnTriggerStay(Collider col){
//		if (col.gameObject.tag == "Player" && !GetComponent<EnemyStats>().immobilised) {
//			Debug.Log ("I am not stunned i can follow");
//			GetComponent<NavMeshAgent> ().destination = col.gameObject.transform.position;
//			GetComponent<NavMeshAgent> ().stoppingDistance = 3f;
//			//StartCoroutine (FollowPlayer (col.gameObject));
//
//		}
//	}

	public void  FollowPlayer(GameObject player){
		if (Vector3.Distance (this.transform.position, player.transform.position) >=20) {
			GetComponent<NavMeshAgent> ().destination = player.transform.position;
		}

	}
}
