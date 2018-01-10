using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Lootable : Interactable {
	private GameObject dropManager;
	public List<Item> myItems = new List<Item>();
	private int generateLootAmount;
	private bool dropedItemsOnce= false;
	private GameObject dropedItemParent;
	private GameObject dropedItemPrefab;
	public GameObject lootboxPanel;
	public int toDropOrNotToDrop;
	private QuestProgress questProg;
	public ItemDatabase itemDb;



	// Use this for initialization
	void Start () {
		itemDb = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ItemDatabase> ();
		questProg = GameObject.FindGameObjectWithTag ("QuestManager").GetComponent<QuestProgress>();
		dropManager = GameObject.FindGameObjectWithTag ("DropManager");
		dropedItemParent = dropManager.GetComponent<DropItemList> ().dropedItemParent;
		dropedItemPrefab = dropManager.GetComponent<DropItemList> ().dropedItemPrefab;
		lootboxPanel = dropManager.GetComponent<DropItemList> ().LootBoxFrame;
		dropManager.GetComponent<DropItemList> ().skeletonId = transform.parent.GetComponent<EnemyStats> ().id;
		//LootPanel.SetActive (false);
	}
	
	public override void Interact(){
		Debug.Log ("I am lootable now");
			if (!dropedItemsOnce) {
				DropAnItem ();
			} else {

				GenerateTheLootBox ();
			}

	}

	public void DropAnItem(){
		generateLootAmount = Random.Range (0, 4);

		for (int i = 0; i < generateLootAmount; i++) {
				myItems.Add (dropManager.GetComponent<DropItemList> ().DropAnItem ());
			}
		Debug.Log (questProg.CalculateDropChance ());
		if (questProg.CalculateDropChance ()) {
			Debug.Log ("I dropped a quest Item");
			questProg.AddToProgress (0);
			myItems.Add(itemDb.database[itemDb.database.Count-1]);
		}
			dropedItemsOnce = true;
		GenerateTheLootBox ();
	}

	public void GenerateTheLootBox(){
		if (myItems.Count > 0) {
			lootboxPanel.SetActive (true);
		}
		if (lootboxPanel.transform.GetChild (2).transform.childCount > 0) {
			for (int i = 0; i < lootboxPanel.transform.GetChild (2).transform.childCount; i++) {
				Destroy (lootboxPanel.transform.GetChild (2).transform.GetChild (i).gameObject);
			}
		}
		for (int i = 0; i < myItems.Count; i++) {
			GameObject generatedItem = (GameObject)Instantiate (dropedItemPrefab);
			generatedItem.transform.SetParent (dropedItemParent.transform);
			generatedItem.transform.localPosition = Vector3.zero;
			generatedItem.transform.localScale = new Vector3 (1, 1, 1);
			generatedItem.GetComponent<DropItemData> ().item = myItems [i];
			generatedItem.transform.GetChild (0).transform.GetChild(0).GetComponent<Image> ().sprite = myItems [i].sprite;
			generatedItem.transform.GetChild (1).GetComponent<Text>().text = myItems [i].title;
			generatedItem.transform.GetChild (2).GetComponent<Text> ().text = myItems [i].equipted_Slot;

		}
	}

	public void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Close the fucking window");
			lootboxPanel.SetActive (false);
		}
	}


}
