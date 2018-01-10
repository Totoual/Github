using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.FirstPerson
{
public class GameOver : MonoBehaviour {

	public GameObject canvas;
	public GameObject subCamera;
	public Text text;
	private GameManager gm;
	private bool gameEnded;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("_GameManager").GetComponent<GameManager> ();
		canvas.SetActive (false);
		subCamera.SetActive (false);
		gameEnded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameEnded) {
			GameObject.Find ("RigidBodyFPSController").GetComponent<RigidbodyFirstPersonController> ().enabled = false;
				Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			gameEnded = true;
			canvas.SetActive (true);
			subCamera.SetActive(true);
			text.text = gm.Touched ().ToString () + " Times";
			//Debug.LogError (gm.Touched ());
		}
	}
}
}
