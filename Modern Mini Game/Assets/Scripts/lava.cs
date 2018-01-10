using UnityEngine;
using System.Collections;

public class lava : MonoBehaviour {
	[SerializeField] private static float objectSpeed = 0.5f;
	[SerializeField] private float resetPosition = -156f;
	[SerializeField] private float startPosition = 180f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-Vector3.forward * (objectSpeed* Time.deltaTime)); 								// Metaferoume tin platforma aristera kata ena pososto 
		if(transform.localPosition.z <= resetPosition){														// Elegxoume kathe fora an to transform tou object einai sto reset Position
			Vector3 newPos = new Vector3(transform.position.x, transform.position.y , startPosition);		// An einai dimiourgoume mia kainourgia topothesia gia na to steiloume
			transform.position = newPos;																	// Kai tou orizoume tin kainourgia topothesia.

		}
	}
}
