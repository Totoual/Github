using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class InvSlot : MonoBehaviour, IDropHandler{

	public int id; // Einai to id tou slot
	private Inventory inventory;
	private CharacterSheet charSheet;

	void Awake(){
		inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory> ();
		charSheet =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<CharacterSheet> ();
	}

	void Start(){
		
	}

	public void OnDrop(PointerEventData eventData){
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData> ();	 // Pernoume to ItemData script apo to adikeimeno pou travaei o pointer

		if (inventory.items [id].id == -1) {									// Elegxoume an to id tou sygkekrimenou adikeimenou einai -1
			inventory.items [droppedItem.slotID] = new Item ();					// Thetoume tin thesi keni ;
			inventory.items [id] = droppedItem.item;								// Vazoume stin lista items to kainourgio adikeimeno
			droppedItem.slotID = id;											// Orizoume to id tou slot sto id tou sygkekrimenou slot;
			droppedItem.parent = "invSlot";

		} else if (droppedItem.parent == "invSlot") {
			Transform itemTransform = this.transform.GetChild (2);
			//Debug.Log ("I am in the else if ");
			if (droppedItem.slotID != id) {
				itemTransform.GetComponent<ItemData> ().slotID = droppedItem.slotID;   // Orizoume tin thesei tou item transform stin thesi tou idiou me auto pou travame
				itemTransform.SetParent (inventory.slots [droppedItem.slotID].transform); // Orizoume tin nea thesi
				itemTransform.transform.position = inventory.slots [droppedItem.slotID].transform.position;



				droppedItem.slotID = id;
				droppedItem.transform.SetParent (this.transform);
				droppedItem.transform.position = this.transform.position;

				inventory.items [droppedItem.slotID] = itemTransform.GetComponent<ItemData> ().item;
				inventory.items [id] = droppedItem.item;
			} 
		}
		else if (droppedItem.parent =="charSlot" ) {
				Transform itemTransform = this.transform.GetChild (2);
				//Debug.Log ("I am in the else statment 2");
				string parentTemp = this.transform.GetChild (2).GetComponent<ItemData> ().parent;

				itemTransform.GetComponent<ItemData> ().parent = droppedItem.parent;
				itemTransform.SetParent(charSheet.charSlot[charSheet.FindCharacterSLotIDbyType (droppedItem.type)].transform);
				itemTransform.transform.position = charSheet.charSlot [charSheet.FindCharacterSLotIDbyType (droppedItem.type)].transform.position;

				droppedItem.GetComponent<ItemData> ().slotID = itemTransform.GetComponent<ItemData> ().slotID;
				droppedItem.GetComponent<ItemData> ().parent = parentTemp;
				droppedItem.transform.SetParent (inventory.slots [droppedItem.slotID].transform);
				droppedItem.transform.position = inventory.slots [droppedItem.slotID].transform.position;

				charSheet.equiptedItems [charSheet.FindCharacterSLotIDbyType (droppedItem.type)] = itemTransform.GetComponent<ItemData> ().item;
				Debug.Log (itemTransform.GetComponent<ItemData> ().item.slug);
				inventory.items [droppedItem.slotID] = droppedItem.item;
				}
		}
	}

	

