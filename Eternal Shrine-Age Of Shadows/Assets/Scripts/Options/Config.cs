using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Config : MonoBehaviour {
	private Controllers controller;
	public string remapAction;
	public int temp;
	public bool shouldRemap, isActive;
	private GameObject tempObj;
	// Use this for initialization
	void Start () {
		controller = GetComponent<Controllers> ();
		shouldRemap = false;
	}
	
	// Update is called once per frame
	void Update () {	
		if (shouldRemap) {																							// An exoume kanei click se ena koubi
			for (int i = (int)KeyCode.Backspace; i <= (int)KeyCode.Joystick8Button19; i++) {						// Kanoume mia for gia na paroume ola ta koubia pou diatherti i unity
				if(Input.GetKeyDown((KeyCode)i) && Input.GetKeyDown((KeyCode)i) != Input.GetMouseButtonDown(0)){	// Elegxoume poio koubi exei patisei kai einai diaforetiko apo to aristero click
					controller.buttons [temp].key = (KeyCode)i;														// Orizoume sto sygekrimeno button to koubi pou patithike
					tempObj.GetComponentInChildren<Text> ().text = controller.buttons [temp].key.ToString();		// Allazoume to onoma tou koubiou stin othoni
					shouldRemap = false;																			// Thetoume tin metavliti shouldRemap se false
					isActive = false;																				// Kai tin isActive tin thetoume pali false;


				}

			}
		}
	}

	public void RemapKeys(int action, GameObject obj){																// Dexomaste enan integer kai ena gameobject
		if (!isActive) {																							// An to remaping einai active
			remapAction = controller.buttons [action].name;															// pernoume to name tou koubiou se mia proswrini metavliti
			temp = action;																							// Se mia proswrini metavliti orizoume kai to int pou peirame
			tempObj = obj;																							// To idio kai gia to adikeimeno (mas xriazete stin update)
			shouldRemap = true;																						// Thetoume true oti prepei na ginei remap
			obj.GetComponentInChildren<Text> ().text = "Press a button";											// Allazoume to onoma tou koubiou gia na dei o xristis ti prepei na kanei
		}


	}

	public void DefaultKeys(){
		GameObject buttonHandler;
		controller.buttons [0].key = KeyCode.Y;																		// Arxikopoioume tis times
		controller.buttons [1].key = KeyCode.B;																		// Arxikopoioume tis times
		controller.buttons [2].key = KeyCode.C;																		// Arxikopoioume tis times
		controller.buttons[3].key = KeyCode.Q;
		controller.buttons [4].key = KeyCode.W;
		controller.buttons [5].key = KeyCode.E;
		controller.buttons [6].key = KeyCode.R;
		controller.buttons [7].key = KeyCode.Escape;
		controller.buttons [8].key = KeyCode.D;
		controller.buttons [9].key = KeyCode.F;
		buttonHandler = GameObject.Find ("ButtonHolder").gameObject;												// Psaxnoume na vroume to adikeimeno pou exei ta koubia
		//Debug.Log (buttonHandler.name);	
		for (int i = 0; i < controller.buttons.Length; i++) {
			buttonHandler.transform.GetChild (i).GetChild (0).GetComponent<Text> ().text = controller.buttons [i].key.ToString();		// Allazoume ta onomata twn koubiwn
		}

		controller.SaveKeyConfiguration ();
	}
}
