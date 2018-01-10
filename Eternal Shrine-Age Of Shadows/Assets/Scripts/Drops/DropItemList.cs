using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropItemList : MonoBehaviour {

	public List<Item> dropItems = new List<Item>();
	public List<int> dropedItemsForEachSkeleton = new List<int>();
	public GameObject LootBoxFrame;
	public ItemDatabase itemDb;
	public PlayerManager playerMan;
	public GameObject dropedItemParent;
	public GameObject dropedItemPrefab;

	public int skeletonId;

	private GameObject player;
	private int generatedNumber;
	private int generateNumberForAmountOfDrops;
	private QuestProgress questProg;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		questProg = GameObject.FindGameObjectWithTag ("QuestManager").GetComponent<QuestProgress>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Item DropAnItem(){
		generatedNumber = Random.Range (0, 101);
		string rarity = CalculateRarity (generatedNumber);
		foreach (Item item in itemDb.database) {
			if ((item.level_Requirment >= playerMan.Level + 1 || item.level_Requirment <= playerMan.Level - 1) && item.rarity == rarity && item.type !="quest item") {
				if (dropItems != null) {
					if(FetchDropListById(item.id)){
						dropItems.Add (item);
						Debug.Log ("I added an item in the database");
					}
				} else {
					
					dropItems.Add (item);
				}
			}
		}

		int temp = Random.Range (0, dropItems.Count);
		return dropItems[temp];
	}

	public bool FetchDropListById(int id){
		for (int i = 0; i < dropItems.Count; i++) {
			if (dropItems [i].id == id) {
				return false;
			}
		}
		return true;
	}


	public string CalculateRarity(int generatedNumber){
		if (generatedNumber >= 51 && generatedNumber <= 85) {
			return "un-common";
		} else if (generatedNumber >= 86 && generatedNumber <= 95) {
			return "rare";
		} else if (generatedNumber >= 96 && generatedNumber <= 99) {
			return "epic";
		} else if (generatedNumber >= 99 && generatedNumber <= 100) {
			return "legendary";
		} else {
			return "common";
		}

	}
	/// <summary>
	/// Pernoume to id tou skeleton apo to lootable script tin wra pou energopoiitai giati den exoume kapoio allo tropo na kseroume 
	/// poios skeletos mas erikse ta adikeimena wste na boresoume na exoume prosvasi stin lista tou. Afou paroume to id tote psaxnoume ton skeleto
	/// apo tin lista me tous exthrous pou exei o paixtis kai etsi vriskoume ton exthro. Stin synexeia pernoume to empty object pou exei to script lootable
	/// kai psaxnoume na doume an stin lista yparxei to id pou mas exei dosei to dropItemData (to id tou adikeimenou) an yparxei to aferoume apo tin lista 
	/// </summary>
	/// <param name="id">Identifier.</param>
	public void RemoveFromTheSkeletonDropList(int id){
		if (player.GetComponent<WorldInteractions> ().enemies [skeletonId].GetChild (2).GetComponent<Lootable> ().myItems.Count > 0) {
			for (int i = 0; i < player.GetComponent<WorldInteractions> ().enemies [skeletonId].GetChild (2).GetComponent<Lootable> ().myItems.Count; i++) {
				if (player.GetComponent<WorldInteractions> ().enemies [skeletonId].GetChild (2).GetComponent<Lootable> ().myItems [i].id == id) {
					player.GetComponent<WorldInteractions> ().enemies [skeletonId].GetChild (2).GetComponent<Lootable> ().myItems.Remove (
								player.GetComponent<WorldInteractions> ().enemies [skeletonId].GetChild (2).GetComponent<Lootable> ().myItems [i]);
				}
				if (player.GetComponent<WorldInteractions> ().enemies [skeletonId].GetChild (2).GetComponent<Lootable> ().myItems == null) {
					LootBoxFrame.SetActive (false);
				}
			}


		}
	}



}
