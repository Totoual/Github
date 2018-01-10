using UnityEngine;
using System.Collections;

public class BarrelOverKill : MonoBehaviour {

	public GameManager gmManager;

	// Use this for initialization
	void Start () {

		gmManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	
	}

	/// <summary>
	/// Otan o paixtis akoubaei se kapoio vareli pethainei. 
	/// </summary>
	/// <param name="col">Col.</param>
	public void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 0, -10), ForceMode.Impulse);
			gmManager.Death ();

		}
	}
}
