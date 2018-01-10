using UnityEngine;
using System.Collections;

public class SoulsGathering : MonoBehaviour {
	private GameManager gmManager;
	// Use this for initialization
	void Start () {
		gmManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			gmManager.points += 10;
			Debug.Log (gmManager.points);
			Destroy (this.gameObject);
		}
	}
}
