using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {
	[SerializeField] private float force =100f;
	[SerializeField] private AudioClip sfxJump;
	[SerializeField] private AudioClip sfxDeath;
	[SerializeField] private int skullPoints = 10;
	[SerializeField] private int crystalPoints = 1;

	private Animator anim;					// pernoume ton animator tou game object
	private Rigidbody rigidbd;				// Pernoume to rigidbody tou game object
	private bool jump = false;				// Orizoume mia bool gia na doume pote kanei jump
	private AudioSource audioSource;		// pernoume to rigidbody tou game object


	void Awake(){
		Assert.IsNotNull (sfxJump);			// Elegxoume oti exoume valei tous ixous
		Assert.IsNotNull (sfxDeath);		// Elegxoume oti exoume valei tous ixous
	}



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigidbd = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> (); 

	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.GameOver && GameManager.instance.GameStarted) {	
			if (Input.GetMouseButtonDown (0)) {					// Elegxoume an o xristis pataei to aristero click
				GameManager.instance.PlayerStartedGame();		// Dinoume ston game manager na katalavei oti arxisame to game
				anim.Play ("Jump");								// Paizoume to animation jump
				audioSource.PlayOneShot (sfxJump);				// Paizoume ton ixo tou jump
				rigidbd.useGravity = true;						// Thetoume tin varitita true
				jump = true;									// Kai enimerwnoume tin bool oti o xristis patise to click
			}
		}
	}

	void FixedUpdate(){
		if (jump) {											// An o paixtis exei patisei to click
			jump = false;									// Orizoume oti einai false
			rigidbd.velocity = new Vector2 (0, 0);			// midenizoume tin dinami pou eoxume gia na ksekinaei apo to 0 (giati otan peftei i dinami paei se arnitiki timi)
			rigidbd.AddForce (new Vector2(0,force),ForceMode.Impulse);		// Tou dynoume mia dinami gia na pidiksei.
		}

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "obstacle") {								// Elegxoume an kanoume collide me adikeimeno me tag obstacle
			rigidbd.AddForce (new Vector2 (-50, 20), ForceMode.Impulse);	// an kanoume tou dinoume mia aditheti dinami
			rigidbd.detectCollisions = false;								// Orizoume ta collition sto false
			audioSource.PlayOneShot (sfxDeath);								// kai paizoume ton ixo tou death
			GameManager.instance.PlayerCollided();							// Dinoume ston game manager na katalavei oti xasame
		}


	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "skull") {								// Elegxoume an to gameobject exei tag skull
			GameManager.instance.PlayerCollideWithSkull ();					// Enimerwnoume oti o paxtis ekame collide me to skull
			GameManager.instance.AddPoints (skullPoints);					// Prosthetoume tous adistoixous podous 
			col.gameObject.GetComponent<AudioSource> ().PlayOneShot (col.gameObject.GetComponent<AudioSource> ().clip);   //  Paizoume ton ixo tou skull
			col.gameObject.GetComponent<MeshRenderer> ().enabled = false;				// Apenergopoioume ton meshRenderer
			col.gameObject.GetComponent<Rigidbody> ().detectCollisions = false;			// Apenergopoioume ta collisions
			col.gameObject.transform.GetChild (0).gameObject.SetActive (false);			// Apenergopoioume to particle
			StartCoroutine (EnableTrigger(col.gameObject));								// Ksekiname tin coroutine
			GameManager.instance.DisableSkullComponents ();								// Enimerwnoume oti apenergopoiisame ta components
		}
		if (col.gameObject.tag == "obstacle") {								 // An to adikeimeno exei tag  obstacle
			GameManager.instance.AddPoints (crystalPoints);					 // Tote apla prosthetoume podous
		}
	}
	IEnumerator EnableTrigger(GameObject gameobj){
		yield return new WaitForSeconds (2f);								// Afou perimenoume 2 deuterolepta	
		gameobj.GetComponent<Rigidbody> ().detectCollisions = true;			// Epanaferoume ta collisions sto skull gia na boroume na ta kanoume reset
	}
}
