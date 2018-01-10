using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	static bool sawOnce = false;
	public Text txt;

	// Use this for initialization
	void Start () {
		if (!sawOnce) {

			GetComponent<Image> ().enabled = true;
			Time.timeScale = 0;
		
		}

		sawOnce = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0 && (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0))) {
			Time.timeScale = 1;
			GetComponent<Image> ().enabled = false;
			txt.enabled = false;
		}
	}
}
