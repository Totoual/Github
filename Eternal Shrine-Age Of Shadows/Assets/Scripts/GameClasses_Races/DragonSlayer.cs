using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
[System.Serializable]
public class DragonSlayer: MonoBehaviour{


	public string	className = "Dragon Slayer";
	public string classDescription = "The dragon slayer is a massive, heavily-armed warrior, a wanderer from a tribe that once guarded the sacred kingom of kernal. \n " +
						   		"Fights savagely with melee weapons. Utilizes brute strength to wield mighty two-handed weapons, a weapon in both hands, or a weapon and shield.\n" +
						   		"Builds up Rage when taking or dealing damage, then unleashes it in devastating attacks. ";
	public int strength = 19;
	public int agility = 26;
	public int stamina = 21;
	public int intellect = 20;
	public int spirit = 20;
	public string slug = "dragon_slayer";
	public List<Skill> skill = new List<Skill>();
	private JsonData itemData;



	void Start(){
		Debug.Log (skill [0].Description);
	}








}



