using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public Vector2 force;
	public  bool isGrounded = true;
	public int jumpCounter=0;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && jumpCounter <=2) {
			isJumping ();
			jumpCounter++;
		}
	}


	void isJumping(){
		anim.SetBool ("Jump", true);
		anim.SetBool ("Grounded", false);
		if (isGrounded && jumpCounter < 2) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			GetComponent<Rigidbody2D> ().AddForce (force, ForceMode2D.Impulse);
		} else if (!isGrounded && jumpCounter < 2) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			GetComponent<Rigidbody2D> ().AddForce (force, ForceMode2D.Impulse);
		}
	}


	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			isGrounded = true;
			anim.SetBool ("Grounded", isGrounded);
			anim.SetBool ("Jump", false);
			jumpCounter = 0;
		}
	}
}
