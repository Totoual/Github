using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {
	public GameManager gmManager;
	// Use this for initialization
	void Start () {
		gmManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();

	}

	/// <summary>
	/// Otan o paixtis erxete se epafi me to sygekrimeno adikeimeno pethainei kai 
	/// kaloume tin methodo Death() apo ton game Manager
	/// </summary>
	/// <param name="col">Col.</param>

	public void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Player pasted from here");
			gmManager.notDead = false;
			gmManager.Death ();
		}
	}
}
