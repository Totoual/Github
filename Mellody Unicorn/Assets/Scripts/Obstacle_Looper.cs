using UnityEngine;
using System.Collections;

public class Obstacle_Looper : MonoBehaviour {

	int obstacleNum = 4;				// O arithos twn ebodiwn pou exoume valei stin skini
	float MaxY = 1.85f;					// To megisto ypsos sto opoio boroume na pame to ebodio
	float MinY = 0.33f;					// To elaxisto ypsos sto opoio boroume na pame to ebodio
	public Sprite[] imageSwap;			// Array me ta diaforetika sprites pou xrisimopoioun ta ebodia
	SpriteRenderer temp;				// O sprite render.


	void Start(){
	
		GameObject[] lollipops = GameObject.FindGameObjectsWithTag ("Obstacle");   // Pinakas me ta adikemena me tag Obstacle.
		GameObject[] lol = GameObject.FindGameObjectsWithTag ("Lollipop");			// Pinakas me adikeimena me tag Lollipo ( Stin ousia einai child tou apo panw alla den douleve alliws )


		foreach (GameObject lollipop in lollipops) {								// Epanaliptiki diadikasia gia na orisoume random ypsos tou empdiou

			Vector3 pos = lollipop.transform.position;								// Pernoume to position tou ebodiou
			pos.y = Random.Range (MinY, MaxY);										// Kai tou orizoume randome ypsos metaksi min kai max

			foreach (GameObject tmp in lol) {										// Epanaliptiki diadikasia gia na allaksoume ta sprite sta ebodia.
				temp = tmp.GetComponent<SpriteRenderer> ();							// Pernoume to sprite renderer to tmp
				temp.sprite= imageSwap[Random.Range(0,5)];							// kai orizoume randome sprite symfwna me ton pinaka pou tou exoume dwsei kai ta stoixeia pou exei mesa 
		
			} 

			lollipop.transform.position = pos;										// Orizoume tin kainourgia thesi twn ebodiwn


			//Debug.Log (temp.sprite);

		
		}
	
	}

	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log ("Triggered"+ col.name);

		float windofBg = ((BoxCollider2D)col).size.x;						// Opws kai sto looper elexoume to width tou collider me typecast se box collider
		Vector3 pos = col.transform.position;								// Apothykeuoume to position sto pos

		pos.x += windofBg * obstacleNum ;									// Prosthetoume ston euato tou to width * twn aritho twn ebodiwn.

		if (col.tag == "Obstacle") {
		
			pos.y = Random.Range (MinY, MaxY);								// Orizoume randome ypsos kathe fora pou tou allazoume to position gia na min fenetai idio
		
		}

		//Debug.Log ("I tried to change sprite");
		temp = col.GetComponentInChildren<SpriteRenderer> ();			// Orizoume sto temp to sprite renderer twn Child twn ebodiwn
		temp.sprite= imageSwap[Random.Range(0,5)];						// Orizoume random sprite symfwna me ton pinaka pou exoume orisei.

		col.transform.position = pos;									// Thetoume to kainourgio position sta ebodia.



	}
}
