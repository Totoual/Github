using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorManager : MonoBehaviour {

	public GameManager gmManager;
	public GameObject[] tiles;
	public GameObject instantiateThere;
	public List<GameObject> InGameObjects = new List<GameObject>();
	private float nextPos = 0f;
	private int generatedNumber;
	private int previousGeneratedNumber;
	private bool firstRun = true;

	// Use this for initialization
	/// <summary>
	/// Dimiourgoume tin arxiki platforma. I platforma apoteleitai apo 30 komatia tis gefyras
	/// To kathe ena topothetite dipla sto allo.
	/// </summary>
	void Start () {
			for (int i = 0; i < 30; i++) {
				GameObject temp = (GameObject)Instantiate (tiles [CalculateRandomNumber ()]);		// Kanoume instantiate to prwto item apo ton pinaka me vasi enan random arithmo
				if (i == 0) {																		// Topothetoume to element stin thesi 0 stin arxi tou paixnidiou
					temp.transform.position = Vector3.zero;											// Orizoume to transform  stin thesi 0,0,0
				} else {
					temp.transform.position = new Vector3 (0, InGameObjects [i - 1].transform.GetChild (1).position.y, nextPos); // Ta epomena element topothetoude sto ypsos kai to vathos tou proigoumenou adikeimenou pou yparxei stin lista
				}
				nextPos += temp.transform.GetChild (0).GetComponent<MeshRenderer> ().bounds.size.z;			// To epomeno z einai iso me ton euato tou + to width tou collider tou adikeimenou pou valame
				InGameObjects.Add (temp);																// Vazoume to adikeimeno mesa stin lista.

			}

	}

	/// <summary>
	/// Paragoume enan random arithmo.
	/// An einai i arxi tou paixnidiou orizoume ws generatednumber to 0 gia na 
	/// mporesoume na knaoume instantiate tin prwti platforma pou einai eutheia
	/// Topothetoume ton arithmo pou exoume paragei se mia temp metavliti gia na boroume na elegxoume
	/// an o proigoumenos generated number einai idios me auton pou paragame. an isxyei auto ksanakanoume generate arithmo
	/// </summary>
	/// <returns>The random number.</returns>
	public int CalculateRandomNumber(){
		generatedNumber =  Mathf.RoundToInt (Random.Range (0, tiles.Length));
		//Debug.Log (generatedNumber);
		if (firstRun) {
			firstRun = false;
			generatedNumber = 0;
			return generatedNumber;
		}
		if (generatedNumber != previousGeneratedNumber) {
			previousGeneratedNumber = generatedNumber;
			return generatedNumber;
		} else {
			//Debug.Log ("I call the method again");
			return CalculateRandomNumber ();
		}
	}

	/// <summary>
	/// Afou exoume kanei destroy ena item apo tin lista mas
	/// Dimiourgoume ena neo adikeimeno stin thesi pou exei oristei kai to kanoume add stin lista me ta adikeimena pou
	/// yparxoun sto paixnidi.
	/// </summary>
	public void InstantiateOnDestroy(){
		GameObject temp = (GameObject)Instantiate (tiles [CalculateRandomNumber ()]);
		temp.transform.position = new Vector3 (0, InGameObjects [28].transform.GetChild (1).position.y, instantiateThere.transform.position.z + 1f);
		//nextPos +=  temp.transform.GetChild (0).GetComponent<MeshRenderer> ().bounds.size.z;
		InGameObjects.Add (temp);
		Debug.Log ("I try to create the fucking bridge");

	}

	/// <summary>
	/// Otan kanapoio kommati tis gefyras kanei collide sto simeio pou exei oristei sto game katastrefetai.
	/// Etsi opws exei ftiaxtei to kommati pou kanei collide prwto einai to prwto stoixeio tis gefyras Opote to aferoume ki olas.
	/// kai meta kaloume tin methodo InstantiateOnDestroy()
	/// </summary>
	public void DestoryFirstObjectOfTheList(){
		Debug.Log ("I ve been called to destroy the item ");
		//nextPos -= InGameObjects [0].gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().bounds.size.z;
		Destroy (InGameObjects[0].gameObject);
		InGameObjects.Remove (InGameObjects [0].gameObject);
		InstantiateOnDestroy ();

	
	}
}
