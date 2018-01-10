using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChangeCollision : MonoBehaviour {
	//40.65 / 0 / 221.31
	public Vector3 newPos;
	//public string loadScene;
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<NavMeshAgent> ().Stop();
			col.gameObject.GetComponent<NavMeshAgent> ().enabled = false;
			col.gameObject.transform.localPosition = newPos;
			col.gameObject.GetComponent<NavMeshAgent> ().enabled = true;
		}
	}
}
