using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VendorManager : MonoBehaviour {
	
	public GameObject vendorPanel;
	public GameObject slotPanel;
	public ItemDatabase database;
	public GameObject vendorSlot;
	public GameObject vendorItem;
	public QuestProgress questProg;
	public QuestDatabase questDb;

	public List<GameObject> vendorslots = new List<GameObject> ();
	public List<Item> vendorItems = new List<Item> ();


	public int slotAmount;
	// Use this for initialization
	void Start () {
		slotAmount = 20;
		for (int i = 0; i < slotAmount; i++) {
			vendorItems.Add (new Item ());
			vendorslots.Add(Instantiate (vendorSlot));
			vendorslots [i].GetComponent<VendorSlot> ().id = i;
			vendorslots[i].transform.SetParent(slotPanel.transform);	// Allazoume to parent sto hierarchy
			vendorslots[i].transform.localPosition = Vector3.zero;	// Allazoume to LocalPosition tou adikeimenou se 0,0,0
			vendorslots[i].transform.localScale = new Vector3(1,1,1);	// Allazoume to localScale gia na einai swsto otan kanei resize
		}

		AddItemToVendor (14);
	
	}

	public void AddItemToVendor(int id){
		Item itemToAdd = database.FetchItemByID (id);
		if(!CheckIfItemIsInInventory(itemToAdd)){
			for (int i = 0; i < vendorItems.Count; i++) {						  	// Psaxnoume tin prwti keni thesi sto inventory
				if (vendorItems [i].id == -1) {							  			// Auto elegxete me vasi to ID tou an eiani -1
					vendorItems [i] = itemToAdd;							 		// Kai thetoume stin lista twn items to sygekrimeno adikeimeno 
					GameObject itemObj = Instantiate (vendorItem);					// Dimiourgoume to UI tou ( to prefab pou exoume dwsei stin arxi)
					itemObj.GetComponent<VendorItemData> ().item = itemToAdd;				// Enimerwnoume to script tou adikeimenou gia to poio adikeimeno einai
					itemObj.GetComponent<VendorItemData> ().amount = 1;					// Enimerwnoume oti to amount einai 1 
					itemObj.GetComponent<VendorItemData> ().type = itemToAdd.equipted_Slot;
					itemObj.GetComponent<VendorItemData> ().parent = "vendSlot";
					itemObj.GetComponent<VendorItemData> ().slotID = i;					// Vriksoume se poio slot einai to adikeimeno
					itemObj.transform.SetParent (vendorslots [i].transform);				// Kai tou orizoume ws parent to slot sto prwto slot pou exoume vrei oti einai keno
					itemObj.transform.localPosition = Vector3.zero;					// Orizoume tin arxiki tou thesi ws 0,0,0
					itemObj.transform.localScale = new Vector3 (1, 1, 1);			// Orizoume to arxiko scale tou adikeimenou
					itemObj.GetComponent<Image> ().sprite = itemToAdd.sprite; 		// Orizoume tin eikona tou adikeimenou gia na bei sto inventory
					itemObj.name = itemToAdd.title;

					break;

				}
			}
		}
	}
	public bool CheckIfItemIsInInventory(Item item){
		for (int i = 0; i < vendorItems.Count; i++) {
			if (vendorItems [i].id == item.id) {
				return true;
			}
		}
		return false;
	}

	public void RepairEquipment(){
		if (questDb.activeQuest.Count >= 2) {
			questProg.AddToProgress (1);
			questDb.activeQuest [1].ChainId = 1;
			
		}
	}
}
