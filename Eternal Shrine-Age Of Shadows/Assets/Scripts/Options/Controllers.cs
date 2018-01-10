using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Controllers : MonoBehaviour {

	public UIHandler uiHandler;
	public ButtonInfo[] buttons;
	public GameObject buttonHolder;
	public GameObject titleHolder;
	public GameObject button;
	public GameObject title;
	public Config config;
	public bool firstTime = true;
	JsonData keyMaping;

//	private GameObject player;

	void Awake(){
		//DontDestroyOnLoad (this.gameObject);
	}

	void OnEnable(){
		if (File.Exists (Application.persistentDataPath + "/keymapping.json"))
			LoadKeyConfiguration ();
		
	}

	void Start(){
		config = new Config ();
		for (int i = 0; i < buttons.Length; i++) {
			GameObject buttonInstantiated = (GameObject)Instantiate (button);    									 // Dimiourgoume ena koubi gia to kathe adikeimeno pou einai mesa ston pinaka mas
			buttonInstantiated.transform.SetParent (buttonHolder.transform);		 									 // Orizoume ws parent to panel pou theloume na einai i thesi tous sto hierarchy
			buttonInstantiated.transform.localPosition = Vector2.zero;			 									 // Thetoume to local position sto 0 gia na paei stin swsti thesi
			buttonInstantiated.transform.GetChild (0).GetComponent<Text> ().text = buttons [i].key.ToString ();		 // Allazoume to text tou koubiou sto koubi pou einai ston pinaka.
			buttonInstantiated.name = buttons [i].name + "_Button";													 // Allazoume to name sto hierarchy gia na kseroume ti ginetai
			buttons [i].id = i;																						 // Orizoume ena id ston pinaka
			buttonInstantiated.GetComponent<ButtonId> ().id = buttons [i].id;											 // Orizoume ena id sta object gia na boroume na milame me ton pinaka.

			GameObject titleInstantiated = (GameObject)Instantiate (title);											  // Dimourgoume ena Text gia ton titlo tou koubiou
			titleInstantiated.transform.SetParent (titleHolder.transform);											  // Tou orizoume to parent tou
			titleInstantiated.transform.localPosition = Vector2.zero;												  // Tou thetoume to local location 0
			titleInstantiated.GetComponent<Text> ().text = buttons [i].name;										  // Allazoume to keimeno gia na adiproswpeuei to koubi
			titleInstantiated.name = buttons [i].name + "_Text";													  // Allazoume name sto hierarchy
		}
	}

	void Update(){
		AchievmentHotKey ();
		InventoryHotKey ();
		CharacterHotKey ();
		ActionBarFirstHotKey ();
		ActionBarSecondHotKey ();
		ActionBarThirdHotKey ();
		ActionBarForthHotKey ();
		PauseHotKey ();
		ActionBarFifthHotKey ();
		ActionBarForthHotKey ();
	}

	public void SaveKeyConfiguration(){

		keyMaping = JsonMapper.ToJson(buttons);
		File.WriteAllText (Application.persistentDataPath + "/keymapping.json", keyMaping.ToString());
		//Debug.Log (keyMaping.ToString());
	}

	public void LoadKeyConfiguration(){
		string jsonString = File.ReadAllText (Application.persistentDataPath + "/keymapping.json");
		keyMaping = JsonMapper.ToObject (jsonString);
		//Debug.Log (keyMaping);
		for (int i = 0; i < keyMaping.Count; i++) {
			buttons [i].id = (int) keyMaping[i]["id"];
			int temp = (int)keyMaping [i] ["key"];
			buttons [i].key = (KeyCode)temp;
			buttons [i].name = keyMaping [i] ["name"].ToString ();
		
		}

	}


	public void AchievmentHotKey(){
		if(Input.GetKeyDown(buttons[0].key) && uiHandler.canUseHotkeys){
			uiHandler.OnAchievmentClick ();
		}
	}

	public void InventoryHotKey(){
		if(Input.GetKeyDown(buttons[1].key)&& uiHandler.canUseHotkeys){
			uiHandler.OnInventoryClick();
		}
	}

	public void CharacterHotKey(){
		if (Input.GetKeyDown (buttons [2].key)&& uiHandler.canUseHotkeys) {
			uiHandler.OnCharacterShitClick ();
		}
	}

	public void ActionBarFirstHotKey(){
		if (Input.GetKeyDown (buttons [3].key) && uiHandler.canUseHotkeys) {
			uiHandler.OnHowlClick ();
		}
	}
	public void ActionBarSecondHotKey(){
		if (Input.GetKeyDown (buttons [4].key) && uiHandler.canUseHotkeys) {
			uiHandler.OnDragonSwipClick ();
		}
	}
	public void ActionBarThirdHotKey(){
		if (Input.GetKeyDown (buttons [5].key) && uiHandler.canUseHotkeys) {
			uiHandler.OnCleaveClick ();
		}
	}
	public void ActionBarForthHotKey(){
		if (Input.GetKeyDown (buttons [6].key) && uiHandler.canUseHotkeys) {
			uiHandler.OnChargeClick ();
			//Debug.Log ("I am hitting the 4 hotkey");
		}
	}
	public void PauseHotKey(){
		if (Input.GetKeyDown (buttons [7].key)) {
			uiHandler.OnPauseClick ();
			//Debug.Log ("I am hitting the escape hotkey");
		}
	}
	public void ActionBarFifthHotKey(){
		if (Input.GetKeyDown (buttons [8].key) && uiHandler.canUseHotkeys) {
			//Debug.Log (" I am hitting " + buttons [6].key);
			uiHandler.OnPotionClick ();
		}
	}



}
