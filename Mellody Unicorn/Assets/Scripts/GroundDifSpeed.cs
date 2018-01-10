using UnityEngine;
using System.Collections;

public class GroundDifSpeed : MonoBehaviour {
	/// <summary>
	/// Skopos tou script einai na orizoume diaforetiki taxytita sto background se ola epipeda wste na yparxei
	/// kapoio idos parallex effect kai na min fenode ola stasima. Epidi exoume multy layerd background exoume kanei
	/// to speed public gia na to orizoume diaforetika analoga to adikimeno pou theloume. 
	/// </summary>
 public	float speed = 0.9f;

	Rigidbody2D player;
	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag ("Player"); // In case it fales to find the game object
		if(player_go == null){

			Debug.LogError ("I couldn t find an object with tag 'Player'!");
			return;
		}
		player = player_go.GetComponent<Rigidbody2D>();						// Pernoume to Rigidbody tou paixti

	}
	// Update is called once per frame
	void FixedUpdate () {

		//Vector3 pos = transform.position;
		//pos.x += speed * Time.deltaTime;

		//transform.position = pos;

		float vel = player.velocity.x* speed;												// Orizoume to vellocity pou theloume symfwna me to speed tou paixti * tin taxytita pou exoume orisei sto inspector

		transform.position = transform.position + Vector3.right * vel * Time.deltaTime;		// Orizoume to vellocity sta object
	
	}
}
