using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {
	/// <summary>
	/// To script exei topothetithei sta obstacles san ena empty object
	/// me box collider 2d pou einai trigger. Skopos tou einai otan o paixtis 
	/// pernaei apo auto to collider na kalei tin addPoint pou einai sto script Score.cs
	/// kai na auksanei tous vathmous tou paixti.
	/// </summary>
	/// <param name="col">Col.</param>


	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			//Debug.Log ("I called AddPoint");
			Score.AddPoint ();


		}
		
	}
}
