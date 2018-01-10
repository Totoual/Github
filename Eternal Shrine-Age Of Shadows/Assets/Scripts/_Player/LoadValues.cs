using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System.Collections.Generic;

public class LoadValues : MonoBehaviour {
	JsonData jsondata;
	PlayerManager playerMan;
	// Use this for initialization
	void Awake(){
		playerMan = GetComponent<PlayerManager>();
		//playerMan = GetComponent<PlayerManager> ();

	}
	void OnEnable(){
		

	}

	void Start () {
		LoadCharacter ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/// <summary>
	/// Kanoume Load ta stoixeia tou xaraktira pou tha xriastoume gia na ksekinisoume to game
	/// </summary>
	void LoadCharacter(){
		string jsonString = File.ReadAllText (Application.streamingAssetsPath + "/playerCharacter.json");
		//Debug.Log (jsonString);
		jsondata = JsonMapper.ToObject (jsonString);
		playerMan.Class = jsondata ["Class"].ToString();
		playerMan.model_slug = jsondata ["model_slug"].ToString ();
		playerMan.Level = (int) jsondata ["Level"];
		playerMan.Stamina = (int)jsondata ["Stamina"];
		playerMan.Strength = (int)jsondata ["Strength"];
		playerMan.Agility = (int)jsondata ["Agility"];
		playerMan.Intellect = (int)jsondata ["Intellect"];
		playerMan.Spirit = (int)jsondata ["Spirit"];
		playerMan.IronCurrency = (int)jsondata ["IronCurrency"];
		playerMan.WoodCurrency = (int)jsondata["WoodCurrency"];
		playerMan.ClothCurrency = (int)jsondata ["ClothCurrency"];
		for (int i = 0; i < playerMan.skills.Count; i++) {
			playerMan.skills [i].Name = jsondata ["skills"] [i] ["name"].ToString();
			playerMan.skills[i].Description = jsondata ["skills"] [i] ["description"].ToString();
			playerMan.skills[i].Skill_Slug = jsondata ["skills"] [i] ["skill_slug"].ToString();
			playerMan.skills[i].Damage =(int) jsondata ["skills"] [i] ["dmg"];
		}
		playerMan.MaxXp = (int)jsondata ["MaxXp"];
		playerMan.XpToLevelUp = (int)jsondata ["XpToLevelUp"];

	}
}
