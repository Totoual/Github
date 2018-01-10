using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterClick : MonoBehaviour {

	public Camera camera;
	public Transform positionTransform;
	public Transform startingArea;
	public GameObject character;
	public Text description;
	public Text classTitle;
	public Text story;
	public Text[] skillDescription;
	public GameObject skillHolder;
	public GameObject skillInstance;
	public GameObject flame;


	public DragonSlayer dragonSlayer;
	private bool isClicked = false;
	private int counter;
	private bool playedOnce = false;

	// Use this for initialization
	void Start () {
		dragonSlayer = GameObject.Find("ClassHolder").GetComponent<DragonSlayer> ();
		flame.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		ClickToFind ();		// Theloume na elegxoume pou clickarei o paixtis
		MoveCharacter ();	// Theloume otan clickarei se kapoio char na proxwrisei brosta
	}

	/// <summary>
	/// Dimourgoume ena raycast gia na paroume dedomena apo ton paixti stin ousia. Theloume na elegxoume an to 
	/// adikeimeno pou clickarei o paixtis einai kapoios character. An einai theloume na doume to tag tou. Etsi
	/// wste na allaksoume plirofories sto UI symfwna me ta stoixeia tou class. 
	/// </summary>
	public void ClickToFind(){
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log (hit.collider.gameObject);
				if (hit.collider.tag == "Dragon Slayer") {
					counter++;
					character = hit.collider.gameObject;
					ChangeUI ();
					isClicked = true;


				} 
			}
		}
	}
	/// <summary>
	/// Metakinoume ton xaraktira poio koda stin camera gia na ton dei o paixtis 
	/// </summary>
	public void MoveCharacter(){
		if (isClicked) {
			//character.transform.position = Vector3.Lerp (character.transform.position, positionTransform.position, Time.deltaTime);
			if (!playedOnce) {
				character.GetComponent<Animator> ().SetTrigger ("CanHowl");
				character.GetComponent<ButtonMusicPlayer> ().PlayPlayerHowlClip ();
				playedOnce = true;
				StartCoroutine (StartMovingToThePoint ());
			}

//			character.GetComponent<Animator> ().SetBool ("CanRun", true);
//			if (Vector3.Distance (character.transform.position, positionTransform.position) <=	1f) {
//				flame.SetActive (true);
//				character.GetComponent<Animator> ().SetBool ("CanRun", false);
//			}

		}

	}

	IEnumerator StartMovingToThePoint(){
		yield return new WaitForSeconds (2f);
		character.GetComponent<NavMeshAgent> ().destination = positionTransform.position;
		character.GetComponent<Animator> ().SetTrigger("Run");
		character.GetComponent<Animator> ().SetBool("CanRun",true);
		if (character.GetComponent<NavMeshAgent> ().remainingDistance <=	0.05f) {
			flame.SetActive (true);
			yield return new WaitForSeconds (2.4f);
			character.GetComponent<Animator> ().SetBool ("CanRun", false);
		}

	}
	/// <summary>
	/// Allazoume ta stoixeia tou UI wste na exoume plirofories sxetika me to class 
	/// </summary>
	public void ChangeUI(){
		classTitle.text = dragonSlayer.className;
		description.text = dragonSlayer.classDescription;
		if (counter <= 1) {
			for (int i = 0; i < skillDescription.Length; i++) {
				skillDescription [i].text = dragonSlayer.skill [i].Description;
				GameObject skillImage = Instantiate (skillInstance);
				skillImage.transform.SetParent (skillHolder.transform);
				skillImage.transform.localPosition = Vector2.zero;
				skillImage.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Class/" + dragonSlayer.skill [i].Skill_Slug);

			}
		}
	
	}

}
