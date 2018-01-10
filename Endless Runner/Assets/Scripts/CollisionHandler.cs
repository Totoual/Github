using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	public GeneratorManager generator;

	/// <summary>
	/// Exoume valei ena plain sto game sto opoio otan ta props kanoun collide me auto katastrefode.
	/// Perissoteres plirofories stin methodo sto generator script 
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Props") {
			Debug.Log (col.gameObject.tag);
			generator.DestoryFirstObjectOfTheList ();
		}
	}
}
