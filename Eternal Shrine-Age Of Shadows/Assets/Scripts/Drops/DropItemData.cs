using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropItemData : MonoBehaviour , IPointerDownHandler {
	public Item item;
	private Inventory inv;
	private DropItemList dropList;
	private AchievmentGenerator achievmentManager;
	// Use this for initialization
	void Start () {
		inv = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory> ();
		dropList = GameObject.FindGameObjectWithTag ("DropManager").GetComponent<DropItemList> ();
		achievmentManager = GameObject.FindGameObjectWithTag ("AchievmentManager").GetComponent<AchievmentGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerDown(PointerEventData eventData){
		if (eventData.clickCount > 1) {
			Debug.Log ("I clicked the fucking item");
			inv.AddItem (item.id);
			achievmentManager.EarnLootingAchievment ();
			dropList.RemoveFromTheSkeletonDropList (item.id);
			Destroy (this.gameObject);
			eventData.clickCount = 0;
		}
	}
}
