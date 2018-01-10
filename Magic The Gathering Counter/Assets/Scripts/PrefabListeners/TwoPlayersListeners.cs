using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TwoPlayersListeners : MonoBehaviour {

	private bool whiteActive = false;
	private bool blueActive	 = false;
	private bool redActive	 = false;
	private bool blackActive = false;
	private bool greenActive = false;
	private bool colorlessActive = false;

	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();							// Psaxnoume sto hierarchy ton Game Manager 
		Debug.Log(gm.tag);
		DrawHealth (); 																								// Orizoume to life tou ekastote paixti me vasi to health pou exei o 
																													// paixtis stin lista
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (delegate() {							// Vazoume listerner sto name gia na allazei otan to clickaroume
			EnableInputField();
		});

		transform.GetChild (1).transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (delegate() {
			ImageSwapForWhiteDeck();
		});
		transform.GetChild (1).transform.GetChild (1).GetComponent<Button> ().onClick.AddListener (delegate() {
			ImageSwapForBlueDeck();
		});
		transform.GetChild (1).transform.GetChild (2).GetComponent<Button> ().onClick.AddListener (delegate() {
			ImageSwapForRedDeck();
		});
		transform.GetChild (1).transform.GetChild (3).GetComponent<Button> ().onClick.AddListener (delegate() {
			ImageSwapForBlackDeck();
		});
		transform.GetChild (1).transform.GetChild (4).GetComponent<Button> ().onClick.AddListener (delegate() {
			ImageSwapForGreenDeck();
		});
		transform.GetChild (1).transform.GetChild (5).GetComponent<Button> ().onClick.AddListener (delegate() {
			ImageSwapForColorlessDeck();
		});
		transform.GetChild (3).GetComponent<Button> ().onClick.AddListener (delegate() {
			AddToLife(1);
		});
		transform.GetChild (4).GetComponent<Button> ().onClick.AddListener (delegate() {
			AddToLife(5);
		});
		transform.GetChild (5).GetComponent<Button> ().onClick.AddListener (delegate() {
			RemoveFromLife(1);
		});
		transform.GetChild (6).GetComponent<Button> ().onClick.AddListener (delegate() {
			RemoveFromLife(5);
		});
		transform.GetChild (10).GetComponent<Button> ().onClick.AddListener (delegate() {
			RemovePoisonCounter();
		});
		transform.GetChild (11).GetComponent<Button> ().onClick.AddListener (delegate() {
			AddPoisonCounter();
		});


	}
	
	// Update is called once per frame
	void Update () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();							// Psaxnoume sto hierarchy ton Game Manager 

		}
	}

	public void EnableInputField(){
		transform.GetChild (12).gameObject.SetActive (true);
		transform.GetChild (12).GetComponent<InputField> ().onEndEdit.AddListener (delegate(string arg0) {
			transform.GetChild (0).GetComponent<Text> ().text = arg0.ToString ();
			gm.activePlayers[GetComponent<PrefabId>().id].name = arg0.ToString();
			transform.GetChild(12).gameObject.SetActive(false);
		});
	}

#region Deck Icons Image Swap and methods for the listeners
	public void ImageSwapForWhiteDeck(){

		if (!whiteActive) {
			transform.GetChild (1).transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("White");
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Add (0);
			whiteActive = !whiteActive;



		} else {
			transform.GetChild (1).transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("_White");
			whiteActive = !whiteActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Remove (deckType.white);

		}
	}
	public void ImageSwapForBlueDeck(){
		if (!blueActive) {
			transform.GetChild (1).transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Blue");
			blueActive = !blueActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Add (deckType.blue);


		} else {
			transform.GetChild (1).transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("_Blue");
			blueActive = !blueActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Remove (deckType.blue);
		}
	}
	public void ImageSwapForRedDeck(){
		if (!redActive) {
			transform.GetChild (1).transform.GetChild (2).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Red");
			redActive = !redActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Add (deckType.red);

		} else {
			transform.GetChild (1).transform.GetChild (2).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("_Red");
			redActive = !redActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Remove (deckType.red);

		}
	}
	public void ImageSwapForBlackDeck(){
		if (!blackActive) {
			transform.GetChild (1).transform.GetChild (3).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Black");
			blackActive = !blackActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Add (deckType.black);

		} else {
			transform.GetChild (1).transform.GetChild (3).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("_Black");
			blackActive = !blackActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Remove (deckType.black);

		}
	}
	public void ImageSwapForGreenDeck(){
		if (!greenActive) {
			transform.GetChild (1).transform.GetChild (4).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Green");
			greenActive = !greenActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Add (deckType.green);

		} else {
			transform.GetChild (1).transform.GetChild (4).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("_Green");
			greenActive = !greenActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Remove (deckType.green);

		}
	}
	public void ImageSwapForColorlessDeck(){
		if (!colorlessActive) {
			transform.GetChild (1).transform.GetChild (5).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Colorless");
			colorlessActive = !colorlessActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Add (deckType.colorless);

		} else {
			transform.GetChild (1).transform.GetChild (5).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("_Colorless");
			colorlessActive = !colorlessActive;
			gm.activePlayers [GetComponent<PrefabId> ().id].activeDeck.Remove (deckType.colorless);

		}
	}

#endregion
//========================================= ADD AND REMOVE  LIFE AND POISON FUNCTIONS ======================================================

	public void AddToLife(int points){
		gm.activePlayers [GetComponent<PrefabId> ().id].health += points;
		DrawHealth ();
	}
	public void RemoveFromLife(int points){
		gm.activePlayers [GetComponent<PrefabId> ().id].health -= points;
		if (gm.activePlayers [GetComponent<PrefabId> ().id].health <= 0) {
			gm.activePlayers [GetComponent<PrefabId> ().id].health = 0;
			DrawHealth ();
			Death();
		}
		DrawHealth ();
	}

	public void AddPoisonCounter(){
		gm.activePlayers [GetComponent<PrefabId> ().id].currentPoisonCounter++;
		if (gm.activePlayers [GetComponent<PrefabId> ().id].currentPoisonCounter >= gm.activePlayers[GetComponent<PrefabId>().id].maxPoisonCounter) {
			gm.activePlayers [GetComponent<PrefabId> ().id].currentPoisonCounter = gm.activePlayers[GetComponent<PrefabId>().id].maxPoisonCounter;
			DrawPoison ();
			Death();
		}
		DrawPoison ();
	}
	public void RemovePoisonCounter(){
		gm.activePlayers [GetComponent<PrefabId> ().id].currentPoisonCounter--;
		if (gm.activePlayers [GetComponent<PrefabId> ().id].currentPoisonCounter <= 0) {
			gm.activePlayers [GetComponent<PrefabId> ().id].currentPoisonCounter = 0;
		}
		DrawPoison ();
	}



//========================================= DRAW LIFE AND POISON FUNCTIONS =====================================================================
	public void DrawHealth(){
		transform.GetChild (2).GetComponent<Text> ().text = gm.activePlayers [GetComponent<PrefabId> ().id].health.ToString();
	}
	public void DrawPoison(){
		transform.GetChild (8).GetComponent<Text> ().text = gm.activePlayers [GetComponent<PrefabId> ().id].currentPoisonCounter.ToString ();
	}

	public void Death(){
		foreach (Player pl in gm.activePlayers) {
			if (pl.health == 0 || pl.currentPoisonCounter == pl.maxPoisonCounter) {
				pl.dead = true;
				gm.EndOfTheMatch ();
			}
		}
	}



}
