using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public Dropdown playersAmount;
	public List<Player> activePlayers = new List<Player> ();
	public GameObject characterAmount;
	public GameObject twoPlayersPrefab;
	public GameObject eightPlayersPrefab;
	public GameObject twoPlayersParent;
	public GameObject eightPlayersParent;

	public GameObject playerHolders;
	public GameObject endScreen;

	protected float seconds;
	protected int minutes;
	protected int hours;

	// Use this for initialization
	void Start () {
		
	}
	
	public void CreatePlayers(int amount){
		for (int i = 0; i < amount; i++) {
			activePlayers.Add (new Player ());
			activePlayers [i].id = i;
		}
	}

	public void OnNextClick(){
		CreatePlayers (playersAmount.value+2);
		characterAmount.SetActive (false);
		GeneratePlayerPortrait ();
		StartCoroutine (Timer ());
	}

	public void GeneratePlayerPortrait(){
		for (int i = 0; i < activePlayers.Count; i++) {
			if (activePlayers.Count == 2) {
				ChooseThePrefab (twoPlayersPrefab, twoPlayersParent,i);
			} else if (activePlayers.Count == 8) {
				ChooseThePrefab (eightPlayersPrefab, eightPlayersParent,i);
			}
		}
	}

	public void ChooseThePrefab(GameObject prefab, GameObject parent, int i){
		GameObject tempPlayerPortrait = (GameObject)Instantiate (prefab);
		tempPlayerPortrait.transform.SetParent (parent.transform);
		tempPlayerPortrait.transform.localPosition = Vector2.zero;
		tempPlayerPortrait.transform.localScale = new Vector3 (1, 1, 1);
		tempPlayerPortrait.GetComponent<PrefabId> ().id = i;
	}

	public void EndOfTheMatch(){
		int counter=0;
		int id = 0;
		foreach (Player pl in activePlayers) {
			if (pl.dead != true) {
				counter++;
				id = pl.id;
			}
		}
		if (counter == 1) {
			Debug.Log ("Winner is " + activePlayers [id].name);
		}
	}

	public IEnumerator Timer(){
		yield return new WaitForSecondsRealtime(1f);
		seconds++;
		if (seconds >= 60) {
			seconds = 0;
			minutes++;
		}
		if (minutes >= 60) {
			minutes = 0;
			hours++;
		}

		//Debug.Log(hours+":"+minutes+":"+seconds.ToString("F1"));
		StartCoroutine (Timer ());
	}
}
