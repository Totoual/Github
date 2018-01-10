using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class VendorItemData : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler {

	public Item item;
	public int amount;
	public int slotID; // To id tou slot pou exoume
	public string type;
	public string parent;



	private ToolTip tooltip;
	private Inventory inventory;
	private PlayerManager playerMan;
	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory> ();
		tooltip = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ToolTip> ();
		playerMan = GameObject.FindGameObjectWithTag ("PlayerChar").GetComponent<PlayerManager> ();
	
	}
	public void OnPointerEnter(PointerEventData eventData){
		tooltip.Activate (item);
	}

	public void OnPointerExit(PointerEventData eventData){
		tooltip.Deactivate ();
	}
	public void OnPointerDown(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Right && playerMan.IronCurrency>= item.iron_Currency 
			&& playerMan.WoodCurrency >= item.leather_Currency && playerMan.ClothCurrency>=item.cloth_Currency ) 
		{
			inventory.AddItem (item.id);
			playerMan.IronCurrency -= item.iron_Currency;
			playerMan.WoodCurrency-= item.leather_Currency;
			playerMan.ClothCurrency -= item.cloth_Currency;

		}
	}
}
