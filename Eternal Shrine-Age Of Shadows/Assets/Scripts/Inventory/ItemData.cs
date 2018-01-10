using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler{

	public Item item;
	public int amount;
	public int slotID; // To id tou slot pou exoume
	//public int charSlotID;
	public string type;
	public string parent;





	private ToolTip tooltip;
	private Inventory inventory;
	private Vector2 offset;
	private CharacterSheet charSheet;
	private VendorManager vendorMan;
	private Transform originalParent;
	private GameObject dropBoxPanel;
	private GameObject vendorPanel;
	private PlayerManager playerMan;


	public void Start(){
		inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory> ();
		tooltip = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ToolTip> ();
		charSheet = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<CharacterSheet> ();
		vendorMan = GameObject.FindGameObjectWithTag ("VendorManager").GetComponent<VendorManager> ();
		dropBoxPanel = GameObject.FindGameObjectWithTag ("DropBox");
		playerMan = GameObject.FindGameObjectWithTag ("PlayerChar").GetComponent<PlayerManager> ();
		vendorPanel = inventory.vendorPanel;







	}

	public void OnBeginDrag(PointerEventData eventdata){
		originalParent = this.transform.parent.transform;
		Debug.Log (originalParent);
		if (item != null && eventdata.pointerCurrentRaycast.gameObject.tag != "VendorSlot" ) 
		{
			offset = eventdata.position - new Vector2 (this.transform.position.x , this.transform.position.y);
			this.transform.SetParent (this.transform.parent.parent.parent.parent);  // Orizoume sto hierarchy to parent wste na fenete swsta
			this.transform.position = eventdata.position; //- offset; // Otan ksekiname na travame to adikeimeno tou orizoume to position idio me tou mouse
			dropBoxPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}

		//Debug.Log(charSheet.FindCharacterSLotIDbyType (type));
	}

	public void OnDrag(PointerEventData eventdata){
		if (item != null && eventdata.pointerCurrentRaycast.gameObject.tag != "VendorSlot") {
			this.transform.position = eventdata.position - offset;  // Otan ksekiname na travame to adikeimeno tou orizoume to position idio me tou mouse
			//ClearSlot();
		}
	}

	public void OnEndDrag(PointerEventData eventdata){
		//Debug.Log(eventdata.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<CharSlot> ().type);
		//Debug.Log(eventdata.pointerCurrentRaycast.gameObject.tag);
		if (eventdata.pointerCurrentRaycast.gameObject.tag == "InventorySlot") {
			
			this.transform.SetParent (inventory.slots [slotID].transform);  // Orizoume sto hierarchy to parent wste na fenete swsta
			this.transform.position = inventory.slots [slotID].transform.position; // Orizoume to position tou adikeimenou na einai sto sygekrekrimeno postion.

			//Elegxoume an to game object pou exei to adikeimeno pou kanoume raycast einai to character slot kai an to type tou einai idio me tou adikeimenou.
		} else if (eventdata.pointerCurrentRaycast.gameObject.tag == "CharSheetSlot") {
			if(eventdata.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<CharSlot> ().type == this.type){
				this.transform.SetParent (charSheet.charSlot [charSheet.FindCharacterSLotIDbyType (type)].transform);			// An einai tote to thetoume sto kainourgio parent tou 
				this.transform.position = charSheet.charSlot [charSheet.FindCharacterSLotIDbyType (type)].transform.position;	// Kai tou allazoume to position gia na paei stin kainourgia thesi
			}else{
				this.transform.SetParent (originalParent);
				this.transform.localPosition = Vector3.zero;
			}
		}else if (eventdata.pointerCurrentRaycast.gameObject.tag == "DropBox"){
//			Debug.Log ("Test");
//			Debug.Log (eventdata.pointerCurrentRaycast.gameObject.tag);
			this.transform.SetParent (originalParent);
			this.transform.localPosition = Vector3.zero;
			
		}
			

			// Na ftiaksw to panel Pou tha rwtaei ton xristi an thelei na kanei delete to ITEM;
		for(int i =0; i<charSheet.charSlot.Count; i++){
			if (charSheet.charSlot [i].transform.childCount <= 2) {
				charSheet.equiptedItems [i] = new Item ();
			}

		}

		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		dropBoxPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnPointerEnter(PointerEventData eventData){
		tooltip.Activate (item);
	}

	public void OnPointerExit(PointerEventData eventData){
		tooltip.Deactivate ();
	}

	public void OnPointerDown(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Right && vendorPanel.activeSelf == true && parent == "invSlot") {
			vendorMan.AddItemToVendor (item.id);
			inventory.items [slotID] = new Item ();
			Destroy (this.gameObject);
			playerMan.IronCurrency += item.iron_Currency;
			playerMan.WoodCurrency += item.leather_Currency;
			playerMan.ClothCurrency += item.cloth_Currency;
			tooltip.Deactivate ();

		}

	}


	public void ClearSlot(){
		if (parent == "charSlot") {
			charSheet.equiptedItems [charSheet.FindCharacterSLotIDbyType (type)] = new Item ();
		} else if (parent == "invSlot") {
			inventory.items [slotID] = new Item ();
		}
	}




}
