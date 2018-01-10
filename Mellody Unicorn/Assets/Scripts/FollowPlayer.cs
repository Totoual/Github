using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	Transform player;							//Metavliti gia na paroume to transform tou paixti
	float offSetx;								// Einai i apostasi apo ta aristera pou theloume na exoume kathe fora

	// Use this for initialization
	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag ("Player"); // In case it fales to find the game object
		if(player_go == null){												// An den exoume vrei ton paixti epistrefoume 

			Debug.LogError ("I couldn t find an object with tag 'Player'!");
			return;
		}
		player = player_go.transform;										// Orizoume stin metavliti tou player to transformation tou game object
		offSetx = transform.position.x - player.position.x;					// Orizoume to offset pou theloume na exoume kathe fora
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {												// An o paixtis den einai keno
			Vector3 pos = transform.position;								// Orizoume mia metavliti typou vector3 gia na apothikeusoume to position
			pos.x = player.position.x + offSetx;							// Kai thetoume to x tou offset iso me to x tou paixti + to offset 
			transform.position = pos;										// Orizoume tin kainourgia thesi
		}

	}
}
