using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CharSlot : MonoBehaviour, IDropHandler {
	public int id;
	public string type;
	private Inventory inventory;
	private CharacterSheet charSheet;

	void Start(){
		inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory> ();
		charSheet =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<CharacterSheet> ();
	}

	public void OnDrop(PointerEventData eventData)
	{	//GameObject droppedObj = eventData.pointerDrag.gameObject;	
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData> ();	 // Pernoume to ItemData script apo to adikeimeno pou travaei o pointer
		if(charSheet.equiptedItems[id].id == -1 && droppedItem.type == type){	 //	Elegxoume kai an o typos tou adikeimenou einai o idios.
			inventory.items [droppedItem.slotID] = new Item ();					 // Thetoume tin thesi sto inventory keni!
			charSheet.equiptedItems [id] = new Item ();
			charSheet.equiptedItems [id] = droppedItem.item;					 // Orizoume stin sygkekrimeni thesi tis listas to adikeimeno pou traviksame


			droppedItem.parent = "charSlot";
		}else if (droppedItem.slotID != id && droppedItem.type == type) {

			Transform itemTransform = this.transform.GetChild (2);

			string parentTemp = this.transform.GetChild (2).GetComponent<ItemData> ().parent;
			itemTransform.GetComponent<ItemData> ().slotID = droppedItem.slotID;
			itemTransform.GetComponent<ItemData> ().parent = droppedItem.parent;
			itemTransform.SetParent (inventory.slots [droppedItem.slotID].transform);
			itemTransform.transform.position = inventory.slots [droppedItem.slotID].transform.position;
			//Debug.Log (droppedItem.parent);

			droppedItem.GetComponent<ItemData> ().parent = parentTemp;
			droppedItem.transform.SetParent (charSheet.charSlot [charSheet.FindCharacterSLotIDbyType (droppedItem.type)].transform);
			droppedItem.transform.position = charSheet.charSlot [charSheet.FindCharacterSLotIDbyType (droppedItem.type)].transform.position;

			inventory.items [droppedItem.slotID] = itemTransform.GetComponent<ItemData> ().item;
			charSheet.equiptedItems [id] = droppedItem.item;
			//Debug.Log ("I am on the chest");
		}
	}

}
