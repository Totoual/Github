using UnityEngine;
using System.Collections;

public class Looper : MonoBehaviour {
	int numPanels = 2;						// Orizoume ton arithmo twn panel pou exoume valei stin skini
	void OnTriggerEnter2D(Collider2D col){				// Otan kanoume trigger
		//Debug.Log ("Triggered"+ col.name);

		float windofBg = ((BoxCollider2D)col).size.x;			// Orizoume stin metavliti windofBg to megethos pou exei sto x ( Typecast tou collider2D se box collider pou exoume valei sto background)
		Vector3 pos = col.transform.position;					// Orizoume to position  tou collider

		pos.x += windofBg * numPanels - 0.5f;					// orizoume to kainourgio x pou theloume na metaferoume to background symfwna me to width tou background ton arithmo twn background pou exoume valei kai
		col.transform.position = pos;							// to offset pou exoume orizei( -0.5f) gia na min afinei kena. kai kanoume transform to kainourgio position.

	}
}
