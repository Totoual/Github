using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class History : MonoBehaviour {

	public GameObject mainMenu;		// Pernoume to mainMenu object gia na to apanergopoioume otan kanei click kapoios sto koubi stats
	public GameObject nameDbHolder;
	public GameObject holder;
	public GameObject gameManager;

	public Player[] pl;

	//public List<PlayerTest> pl = new List<PlayerTest> ();
	string path;
	string jsonString;

	// Use this for initialization
	void Start () {
		path = gameManager.GetComponent<PlayerDatabase> ().path;
		jsonString = File.ReadAllText (path);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStatsClick(){
		mainMenu.SetActive (false);
		nameDbHolder.SetActive (true);
		ShowSavedPlayers ();
	}


	public void ShowSavedPlayers(){
		pl = JsonHelper.getJsonArray<Player> (jsonString);
		for (int i = 0; i < pl.Length; i++) {
			Debug.Log (pl [i].name);
		}

	}
}

