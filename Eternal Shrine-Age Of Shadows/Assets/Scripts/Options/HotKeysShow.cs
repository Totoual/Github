using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HotKeysShow : MonoBehaviour {

	public Controllers buttonsList; 
	public int keyID;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ChangeText ();
	}

	public void ChangeText(){
		GetComponent<Text> ().text = buttonsList.buttons [keyID].key.ToString ();
	}
}
