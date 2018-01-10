using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Item {  									// Dimiourgoume tin class ITEM gia na boresoume na ftiaksoume to database

	[SerializeField]	public int id;
	[SerializeField]	public string title;
	[SerializeField]	public string slug;
	[SerializeField]	public string type;
	[SerializeField]	public  Sprite sprite;
	[SerializeField]	public int level_Requirment;
	[SerializeField]	public string equipted_Slot;
	[SerializeField]	public int min_Damage;
	[SerializeField]	public int max_Damage;
	[SerializeField]	public int armor;
	[SerializeField]	public int strength;
	[SerializeField]	public int agility;
	[SerializeField]	public int stamina;
	[SerializeField]	public int intellect;
	[SerializeField]	public int spirit;
	[SerializeField]	public float speed;
	[SerializeField]	public string rarity;
	[SerializeField]	public int iron_Currency;
	[SerializeField]	public int leather_Currency;
	[SerializeField]	public int cloth_Currency;
	[SerializeField]	public bool stackable;
	[SerializeField]	public int durability;



	public Item(int id, string title, string slug, string type, int level_Requirment, string equipted_Slot, string rarity, bool stackable,int min_Damage = 0, int max_Damage=0,int armor=0,
		int strength = 0, int agility = 0, int stamina = 0,int intellect =0, int spirit = 0,int iron_Currency = 0, int leather_Currency = 0, int cloth_Currency=0,int durability=0){

		this.id = id;
		this.title = title;
		this.slug = slug;
		this.type = type;
		this.sprite = Resources.Load<Sprite> ("Sprites/Items/" + slug);
		this.level_Requirment = level_Requirment;
		this.equipted_Slot = equipted_Slot;
		this.min_Damage = min_Damage;
		this.max_Damage = max_Damage;
		this.armor = armor;
		this.strength = strength;
		this.agility = agility;
		this.stamina = stamina;
		this.intellect = intellect;
		this.spirit = spirit;
		this.speed = Random.Range(1.1f,1.8f);
		this.rarity = rarity;
		this.iron_Currency = iron_Currency;
		this.leather_Currency = leather_Currency;
		this.cloth_Currency = cloth_Currency;
		this.stackable = stackable;
		this.durability = durability;
	}

	public Item(){
		this.id = -1;
	}
}
