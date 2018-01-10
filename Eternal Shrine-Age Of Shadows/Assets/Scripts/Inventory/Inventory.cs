using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public GameObject inventoryPanel;
	public GameObject slotPanel;
	public ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
	public GameObject vendorPanel;

	private int slotAmount;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();



	void Start(){
		slotAmount = 20;
		database = GetComponent<ItemDatabase> ();

		for (int i = 0; i < slotAmount; i++) {
			items.Add (new Item ()); 							// Gemizoume tin lista me ta items me kena adikimena
			slots.Add (Instantiate (inventorySlot)); 			// Dimiourgoume ta slot sto inventory
			slots[i].GetComponent<InvSlot>().id = i;			// Orizoume id sto inventory slot ( Auto tha allaksei gia na bei kai to character shit);
			slots[i].transform.SetParent(slotPanel.transform);	// Allazoume to parent sto hierarchy
			slots[i].transform.localPosition = Vector3.zero;	// Allazoume to LocalPosition tou adikeimenou se 0,0,0
			slots[i].transform.localScale = new Vector3(1,1,1);	// Allazoume to localScale gia na einai swsto otan kanei resize

		}

		AddItem (0);
		//Debug.Log (database.FetchItemByID(0).title);
	}

	//Bazoume ena adikeimeno sto inventory me vasi to ID tou
	public void AddItem(int id)
	{
		Item itemToAdd = database.FetchItemByID (id);             	// Dimiourgoume ena adikeimeno gia na bei sto inventory
		//Debug.Log(itemToAdd.title);
		if (itemToAdd.stackable && CheckIfItemIsInInventory (itemToAdd)) {								// Elegxoume an to adikeimeno einai stackable kai an yparxei sto inventory
			for (int i = 0; i < items.Count; i++) {													// Psaxnoume to inventory
				if (items [i].id == id) {															// Vriskoume to item
					ItemData data = slots [i].transform.GetChild (2).GetComponent<ItemData> ();		// Pernoume to script ItemData apo to item
					data.amount++;																	// Tou auksanoume to amount 
					data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();	// Kanoume update to Amount sto GUI
					break;
				}
			}

		} else {
			for (int i = 0; i < items.Count; i++) {						  		// Psaxnoume tin prwti keni thesi sto inventory
				if (items [i].id == -1) {							  		// Auto elegxete me vasi to ID tou an eiani -1
					items [i] = itemToAdd;							 		// Kai thetoume stin lista twn items to sygekrimeno adikeimeno 
					GameObject itemObj = Instantiate (inventoryItem);		// Dimiourgoume to UI tou ( to prefab pou exoume dwsei stin arxi)
					itemObj.GetComponent<ItemData>().item = itemToAdd;		// Enimerwnoume to script tou adikeimenou gia to poio adikeimeno einai
					itemObj.GetComponent<ItemData>().amount = 1;			// Enimerwnoume oti to amount einai 1 
					itemObj.GetComponent<ItemData>().type = itemToAdd.equipted_Slot;
					itemObj.GetComponent<ItemData> ().parent = "invSlot";
					itemObj.GetComponent<ItemData>().slotID = i;			// Vriksoume se poio slot einai to adikeimeno
					itemObj.transform.SetParent (slots [i].transform);		// Kai tou orizoume ws parent to slot sto prwto slot pou exoume vrei oti einai keno
					itemObj.transform.localPosition = Vector3.zero;			// Orizoume tin arxiki tou thesi ws 0,0,0
					itemObj.transform.localScale = new Vector3 (1,1,1);		// Orizoume to arxiko scale tou adikeimenou
					itemObj.GetComponent<Image> ().sprite = itemToAdd.sprite; // Orizoume tin eikona tou adikeimenou gia na bei sto inventory
					itemObj.name = itemToAdd.title;

					break;

				}
			}
		}
	}
	/// <summary>
	/// Psaxnoume na vroume sto inventory an yparxei to adikeimeno
	/// </summary>
	/// <returns><c>true</c>, if if item is in inventory was checked, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	public bool CheckIfItemIsInInventory(Item item){
		for (int i = 0; i < items.Count; i++) {
			if (items [i].id == item.id) {
				return true;
			}
		}
		return false;
	}
}
