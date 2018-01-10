using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Achievment  {


	private string name;
	private string description;
	private bool unlocked;
	private int points;
	private string slug;

	private GameObject achievmentRef;
	private List<Achievment> dependencies = new List<Achievment> ();
	private string child;

	#region Getters & Setters

	public string Name{
		get{ return name;}
		set{ name = value;}
	}

	public string Description{
		get{ return description;}
		set{ description = value;}
	}
	public bool Unlcoked{
		get{ return unlocked;}
		set{ unlocked = value;}
	}
	public int Points{
		get{ return points;}
		set{ points = value;}
	}
	public string Slug{
		get{ return slug;}
		set{ slug = value;}
	}
	public GameObject AchievmentRef{
		get{ return achievmentRef;}
		set{ achievmentRef = value;}
	}
	public string Child{
		get{ return child;}
		set{ child = value;}
	}

	#endregion


	public Achievment(string name, string description, int points, GameObject achievmentRef, string slug){


		this.name = name;
		this.description = description;
		this.points = points;
		this.unlocked = false;
		this.achievmentRef = achievmentRef;
		this.slug = slug;
		LoadAchievment ();
	}

	public void AddDependency(Achievment dependency){
		dependencies.Add (dependency);
	}

	public bool EarnAchievment(){
		if (!unlocked && !dependencies.Exists(x => x.unlocked == false)) {				// Psaxnoume se oli tin lista me ta dependencies na doume an ola ta achievment einai false
			this.AchievmentRef.GetComponent<Image> ().sprite = AchievmentManager.Instance.unlockedSprite;		// Allazoume to sprite sto parathyro me ta achievment
			SaveAchiev (true);															// Kaloume tin save function
			if (Child != null) {														// Elegxoume an to achievment mas einai child kapoioy allou achievment kai an einai keno
				AchievmentManager.Instance.EarnAchievment (Child);						// An den einai keno prospathoume na knaoume earn to achiev
			}
			return true;
		} 
		return false;
	}

	public void SaveAchiev(bool value){
		unlocked = value;
		// Vazoume ton kwdika gia na ginei to save.
		// Prepei na metrisoume an o paixtis exei kapoious podous kai na prosthesoume tous kainourgious
		// Swsoume to achievment me onoma kai an exei 0 i 1
	}

	public void LoadAchievment(){
		// Edw vazoume ton kwdika gia na knaoume load ta achievment symfwna me auta pou exoume apothikeusei.
		// Tha apothikeuoume apla an o paixteis exei kseklidwsei to achievment mallon :P

		if (unlocked) {
			// Orizoume ti ginetai an to achievment einai kseklidwmeno. Sygekrimena tha doume an tha valoume podous
			// I tha valoume mono to slide bar na auksanete.
		}
	}

}
