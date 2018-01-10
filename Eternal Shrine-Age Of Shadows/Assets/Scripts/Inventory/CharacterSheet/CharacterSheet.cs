using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour {

	public ItemDatabase database;
	public GameObject inventoryItem;

	public GameObject charSlotPanel;
	public List<GameObject> charSlot = new List<GameObject>();
	public List<Item> equiptedItems = new List<Item>();


	// Use this for initialization
	void Start () {

		database = GetComponent<ItemDatabase> ();

		for (int i = 0; i < charSlot.Count; i++) {
			equiptedItems.Add( new Item ());				//Gemizoume tin lista me ta equipted Items me kena adikeimena.
			charSlot[i].GetComponent<CharSlot>().id =i;     // Thetoume ena id sto kathe slot;
		}

		EquiptedItems (14);
//		EquiptedItems (4);
//		EquiptedItems (1);
//		EquiptedItems (2);
//		EquiptedItems (5);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EquiptedItems(int id){

		Item itemToAdd = database.FetchItemByID (id);             	// Dimiourgoume ena adikeimeno gia na bei sto inventory
		//Debug.Log(itemToAdd.type);
		for (int i = 0; i < charSlot.Count; i++) {						  		// Psaxnoume tin prwti keni thesi sto inventory
			if (equiptedItems [i].id == -1) {							  		// Auto elegxete me vasi to ID tou an eiani -1
				if (itemToAdd.equipted_Slot == charSlot [i].GetComponent<CharSlot> ().type) {
					equiptedItems [i] = itemToAdd;							 		// Kai thetoume stin lista twn items to sygekrimeno adikeimeno 
					GameObject itemObj = Instantiate (inventoryItem);		// Dimiourgoume to UI tou ( to prefab pou exoume dwsei stin arxi)
					itemObj.GetComponent<ItemData> ().item = itemToAdd;		// Enimerwnoume to script tou adikeimenou gia to poio adikeimeno einai
					itemObj.GetComponent<ItemData> ().amount = 1;			// Enimerwnoume oti to amount einai 1 
					itemObj.GetComponent<ItemData> ().type = itemToAdd.equipted_Slot;
					itemObj.GetComponent<ItemData> ().parent = "charSlot";
					itemObj.GetComponent<ItemData> ().slotID = FindCharacterSLotIDbyType (itemToAdd.equipted_Slot);			// Vriksoume se poio slot einai to adikeimeno
					itemObj.transform.SetParent (charSlot [FindCharacterSLotIDbyType (itemToAdd.equipted_Slot)].transform);		// Kai tou orizoume ws parent to slot sto prwto slot pou exoume vrei oti einai keno
					itemObj.transform.localPosition = Vector3.zero;			// Orizoume tin arxiki tou thesi ws 0,0,0
					itemObj.transform.localScale = new Vector3(1,1,1) ;        // Orizoume to arxiko scale tou adikeimenou
					itemObj.GetComponent<Image> ().sprite = itemToAdd.sprite; // Orizoume tin eikona tou adikeimenou gia na bei sto inventory
					itemObj.name = itemToAdd.title;

					break;
				}
			}
		}
	}


	public int FindCharacterSLotIDbyType(string type){
		
		for(int i=0; i<charSlot.Count; i++){
			if(charSlot[i].GetComponent<CharSlot>().type == type){
				return i;
		}
		}

		return -1;

	}
}
