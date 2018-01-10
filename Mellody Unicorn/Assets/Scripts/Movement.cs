using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	//Vector3 velocity = Vector3.zero;
	public float flapSpeed = 500f; 			// Einai i dinami me tin opoia anevainei o monokeros
	public float moveForward = 1f; 			// I taxytita me tin opoia proxwrame
	bool didFlap = false;					// Elegxoume an o paixtis exei patisei mia fora to click gia na petaksei
	Rigidbody2D bird;						// Psaxnoume to rigidbody tou player
	bool dead = false;						// Elegxoume an exei pethanei o paixtis
	public AudioClip audiotr;				// Orizoume enan ixo apo to instpector
	AudioSource audioSrc;					// Kai psaxnoume to audioSource. Tha to arxikopoiisoume stin start
	public bool godMode = false;			// Cheat gia to testing :P
	float deathCd;							// Xronos gia na kanoume respawn

	Animator anim;

	// Use this for initialization
	void Start () {
		bird = GetComponent<Rigidbody2D>();							// Pernoume To rigidbody tou object
		anim = GetComponentInChildren<Animator> ();					// Pernoume ton animator tou object
		audioSrc = GetComponent<AudioSource> ();					// Pernoume to Audio Source tou object
	}

	void Update(){
		if (dead) {													// Elegxoume an einai nekros
			deathCd -= Time.deltaTime;								// An einai tote meiwnoume to couldown tou cd
			if (deathCd <= 0) {										// Elegxouyme an einai mikrotero apo to miden 
				
				UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);  // Kanoume reset tin skini
				Score.score = 0;									// Midenizoume to score.

			}
		} else {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {			// Elegxoume an yparxei input apo ton paixti
				//Debug.LogError("I hit click");

				didFlap = true;																// An yparxei input tote kanoume tin metavliti true.
					
		
			}
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (dead) {																			// Elegxoume an eimaste nekri an nai tote kanoume return den yparxei logos na paei parakatw
			return;
		}
		bird.AddForce (Vector2.right * moveForward);										// Alliws orizoume mia dinami me tin opoia paei deksia o paixtis

		if (didFlap == true) {																// An exoume input apo ton paixti 
			//Debug.LogError ("I am adding force");
			bird.AddForce (Vector2.up * flapSpeed);											// Tote tou orizoume mia dynami pros ta panw wste na min xasei
			didFlap = false;																// Gyrname tin didFlap se false.
			anim.SetTrigger ("DoFlap");														// Kai paizoume to animation pou kanei oti petaei
			audioSrc.PlayOneShot (audiotr , 0.7f);											// O ixos gia apo ta ftera

		}
		if (bird.velocity.y > 0) {
			transform.rotation = Quaternion.Euler (0, 0, 0);								// An o paixtis paei pros ta panw tou orizoume na min borei na gyrisei
		}
		else
		{
			float angle = Mathf.Lerp (0, -90, (-bird.velocity.y / 6f));						// Orizoume tin gonia me tin opoia theloume na gyrizei o paixtis otan peftei
			transform.rotation = Quaternion.Euler (0, 0, angle);							// Kai tin kanoume Transform
		
		
		}

	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (godMode) {																		// Checkaroume an exoume valei to GOD MODE gia to test an nai tote den exoume collisions
			return;
		}
		anim.SetTrigger ("Death");															// Alliws an kanoume collide me kati pethainei o paixtis
		dead = true;																		// Kanoume tin metavliti true
		deathCd = 1.5f;																		// Kai to cooldown to orizoume sto 1.5f
	}
}
