using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public string Class;
	public string model_slug;
	public int Level;
	public int Stamina;
	public int Strength;
	public int Agility;
	public int Intellect;
	public int Spirit;
	public int IronCurrency;
	public int WoodCurrency;
	public int ClothCurrency;
	public List<Skill> skills = new List<Skill>();
	public int MaxXp;
	public int XpToLevelUp;
	public GameObject model;

	void Start(){
		model = Resources.Load<GameObject>("Models/Class/"+ model_slug);
	}


}
