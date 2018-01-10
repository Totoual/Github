using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;


public class ItemDatabase : MonoBehaviour {
	[SerializeField]	public List<Item> database = new List<Item>();

	private JsonData itemData;

	void Start(){

		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath+"/StreamingAssets/Items.json"));
		ConstructItemDatabase ();
		//Debug.Log (FetchItemByID (1).title.ToString());
	}

	/// <summary>
	/// Fetchs the item by Id.
	/// Psaxnoume ena adikimeno mesa sto database me vasi to ID tou
	/// </summary>
	/// <returns>The item by I.</returns>
	/// <param name="id">Identifier.</param>
	public Item FetchItemByID(int id){
		for (int i = 0; i < database.Count; i++) {
			if (database [i].id == id) {
				return database [i];
			}
		}
		return null;
	}

	public int FetchItemBySlug(string slug){
		for (int i = 0; i < database.Count; i++) {
			if (database [i].slug == slug) {
				return database [i].id;
			}
		}
		return -1;
	}

	void ConstructItemDatabase(){
		for (int i = 0; i < itemData.Count; i++) {
			database.Add (new Item ((int)itemData [i] ["id"],itemData[i]["title"].ToString(),itemData[i]["slug"].ToString(),itemData[i]["type"].ToString(),(int)itemData [i] ["level_Requirment"],itemData[i]["equipted_Slot"].ToString(),
				itemData[i]["rarity"].ToString(),(bool)itemData[i]["stackable"],(int)itemData [i] ["min_Damage"],(int)itemData [i] ["max_Damage"],(int)itemData [i] ["armor"],
				(int)itemData [i] ["strength"],(int)itemData [i] ["agility"],(int)itemData [i] ["stamina"],(int)itemData [i] ["intellect"],(int)itemData [i] ["spirit"],(int)itemData [i] ["iron_Currency"],
				(int)itemData [i] ["leather_Currency"],(int)itemData [i] ["cloth_Currency"],(int)itemData [i] ["durability"]));
		}
	}


}
