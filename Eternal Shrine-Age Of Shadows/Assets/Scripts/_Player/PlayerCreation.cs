using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class PlayerCreation : MonoBehaviour {
	public CharacterClick charClicked;
	private PlayerManager playerMan;
	private PlayerCalculations playerCalc;


	JsonData data;

	// Use this for initialization
	void Start () {
		playerMan = GetComponent<PlayerManager>();
		playerCalc = GetComponent<PlayerCalculations> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/// <summary>
	/// Arxikopoioume tis times tou playerManager gia na boroume na tis kanoume load otan ksekinisei to game
	/// I logiki einai oti an exoume diaforetika classes to playermanager tha exei diaforetikes times. Profanws kai mia if pou 
	/// tha elegxei to index pou mas dinei to raycast. Gia na boroume stin epomeni skini na kalesoume ta swsta dedomena
	/// </summary>
	public void OnStartTheGame(){
		playerMan.Class = charClicked.dragonSlayer.className.ToString();
		playerMan.Stamina = charClicked.dragonSlayer.stamina;
		playerMan.Strength = charClicked.dragonSlayer.strength;
		playerMan.Agility = charClicked.dragonSlayer.agility;
		playerMan.Intellect = charClicked.dragonSlayer.intellect;
		playerMan.Spirit = charClicked.dragonSlayer.spirit;
		playerMan.Level = 1;
		playerMan.IronCurrency = 0;
		playerMan.ClothCurrency = 0;
		playerMan.WoodCurrency = 0;
		playerMan.XpToLevelUp = 0;
		playerMan.MaxXp = playerCalc.CalculateXP (playerMan.Level);
		playerMan.model_slug = charClicked.dragonSlayer.slug;
		//playerMan.AttackPower = CalculateAttackPower (playerMan.Strength);

		for (int i = 0; i < charClicked.dragonSlayer.skill.Count; i++) {
			playerMan.skills[i].Name = charClicked.dragonSlayer.skill[i].Name;
			playerMan.skills[i].Description = charClicked.dragonSlayer.skill[i].Description;
			playerMan.skills[i].Skill_Slug = charClicked.dragonSlayer.skill[i].Skill_Slug;
			playerMan.skills[i].Damage = charClicked.dragonSlayer.skill[i].Damage;
		}
	}


	/// <summary>
	/// Apothikevoume ta stoixeia tou xaraktira mas
	/// </summary>
	public void SaveCharacter(){
		string jsonData = JsonUtility.ToJson (playerMan, true);
		File.WriteAllText (Application.streamingAssetsPath + "/playerCharacter.json", jsonData);
		//Debug.Log (jsonData);
	}
}
