using UnityEngine;
using System.Collections;

public class CharacterSpawn : MonoBehaviour {
	public Vector3 startingPos;
	public PlayerManager playerCreation;
	// Use this for initialization

	void Start () {
		ModelInstantiate ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/// <summary>
	/// Dimiourgoume to modelo Symfwna me ton xaraktira pou exei epileksi o paixtis. To modelo kaleitai
	/// apo ton fakelo Resources.
	/// </summary>
	void ModelInstantiate(){
		//Debug.Log (playerCreation.model_slug);
		GameObject playerChar = Instantiate<GameObject> (Resources.Load<GameObject>("Models/"+playerCreation.model_slug));
		playerChar.transform.localRotation = Quaternion.identity;
		playerChar.transform.localPosition = GameObject.FindGameObjectWithTag("StartingPos").transform.position;			// Prepei na to allaksw gia na einai poio elafri
		playerChar.tag = "Player";

	}
}
