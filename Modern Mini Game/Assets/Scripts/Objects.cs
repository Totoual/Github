using UnityEngine;
using System.Collections;

public class Objects : MonoBehaviour {

	[SerializeField] private static float objectSpeed = 2;
	[SerializeField] private float resetPosition = -31.0f;
	[SerializeField] private float startPosition = 69.47f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	protected virtual void Update () {
		if(GameManager.instance.GameOver !=true){
			transform.Translate(Vector3.left * (GameManager.instance.Speed* Time.deltaTime));			 		// Metaferoume tin platforma aristera kata ena pososto 
			if(transform.localPosition.x <= resetPosition){														// Elegxoume kathe fora an to transform tou object einai sto reset Position
				Vector3 newPos = new Vector3(startPosition, transform.position.y , transform.position.z);		// An einai dimiourgoume mia kainourgia topothesia gia na to steiloume
				transform.position = newPos;																	// Kai tou orizoume tin kainourgia topothesia.

			}
		}

	}
}
