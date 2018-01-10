using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float minPos = -0.4697f;			// To mikrotero simeio ston aksona x pou borei na paei o paixtis
	private float centerPos = 0.07f;			// To kedriko simeio ston aksona x
	private float maxPos = 0.63f;				// To megisto simeio ston aksona x pou borei na paei o paixtis
	private Animator anime;
	public float jumpForce;						// I dynami me tin opoia kanei jump
	private int flag;							// Flag gia to jump

	public GameManager gmManager;
	// Use this for initialization
	void Start () {
		anime = GetComponent<Animator> ();
		gmManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	/// <summary>
	/// Elegxoume kathe fora poio koubi pataei o xristis kai orizoume mia diadromi
	/// Episeis checkaroume oti exei ksekinisei to paixnidi kai den einai nekros o paixtis
	/// </summary>
	void Update () {
		if (gmManager.startGame && gmManager.notDead) {
			if (Input.GetKeyDown (KeyCode.A)) {
				ChangeDirection (-1);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				ChangeDirection (1);
			}
			if (Input.GetKeyDown (KeyCode.Space) && flag == 0) {
				GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
				anime.SetTrigger ("Jumped");
				flag++;
			}
		}
	}

	/// <summary>
	/// Simfwna me tin didromi pou kanei o paixtis checkaroume pros ta poia kateuthinsi paei kai orizoume
	/// tin thesi tou ston aksona x me vasi to min , max, kai center pos
	/// </summary>
	/// <param name="dir">Dir.</param>
	void ChangeDirection(int dir){

		if (Mathf.Abs(this.transform.position.x-centerPos) <= 0.3f && dir == -1) {
			this.transform.position = new Vector3 (minPos, this.transform.position.y, this.transform.position.z);
		} else if (Mathf.Abs(this.transform.position.x-centerPos) <= 0.3f && dir == 1) {
			this.transform.position = new Vector3 (maxPos, this.transform.position.y, this.transform.position.z);
		} else if (Mathf.Abs(this.transform.position.x-maxPos) <= 0.3f && dir == -1) {
			this.transform.position = new Vector3 (centerPos, this.transform.position.y, this.transform.position.z);
		}else if (Mathf.Abs(this.transform.position.x-minPos) <= 0.3f && dir == 1) {
			this.transform.position = new Vector3 (centerPos, this.transform.position.y, this.transform.position.z);
		}
	}

	//Midezw to flag kathe fora pou kanei collide me kati o paixtis auto ginetai gia na min mporei na kanei double jump
	void OnCollisionEnter(Collision col){
		flag = 0;
	}




}
