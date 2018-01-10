using UnityEngine;
using System.Collections;

public class PatrolSystem : MonoBehaviour {

	public GameObject endPatrol;
	public Transform startPatrol;
	private float temp;
	private float distance;


	// Use this for initialization
	void Start () {
		//startPatrol = this.transform;
		StartCoroutine (PatrolBiatch (endPatrol.transform));
	
	}
	
	// Update is called once per frame

	IEnumerator PatrolBiatch(Transform goThere){
	if (GetComponent<EnemyStats> ().isNotDead) {
		this.GetComponent<Animation> ().Play (animation: "idle");
		//yield return new WaitForSeconds (3f);
		GetComponent<NavMeshAgent> ().destination = goThere.position;
			if (!GetComponent<EnemyStats> ().immobilised) {
				this.GetComponent<Animation> ().Play (animation: "run");
			} else {
				GetComponent<Animation> ().Play (animation: "idle");
			}
		if (!GetComponent<NavMeshAgent> ().pathPending) {
			if (GetComponent<NavMeshAgent> ().remainingDistance <= GetComponent<NavMeshAgent> ().stoppingDistance) {
				if (!GetComponent<NavMeshAgent> ().hasPath || GetComponent<NavMeshAgent> ().velocity.sqrMagnitude == 0f) {
					//Debug.Log ("I reached the destination");
					this.GetComponent<Animation> ().Stop ();
					this.GetComponent<Animation> ().Play (animation: "idle");
				}
			}
		}
		yield return new WaitForSeconds (5f);
		if (goThere==endPatrol.transform && Vector3.Distance(this.transform.position,endPatrol.transform.position)<=0.2f) {
			this.GetComponent<Animation> ().Stop ();
			this.GetComponent<Animation> ().Play (animation: "idle");
			yield return new WaitForSeconds (3f);
			goThere = startPatrol;
		} else if (goThere == startPatrol && Vector3.Distance(this.transform.position,startPatrol.transform.position)<=0.2f) {
			this.GetComponent<Animation> ().Stop ();
			this.GetComponent<Animation> ().Play (animation: "idle");
			yield return new WaitForSeconds (3f);
			goThere = endPatrol.transform;
		}
		//Debug.Log (goThere.position + "Start Pos" + startPatrol.position + " End Pos" + endPatrol.transform.position);

			StartCoroutine (PatrolBiatch (goThere));
		}

	}
}
