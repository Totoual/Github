using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour {

	//private Item item;
	private string data;
	public GameObject tooltip;

	void Start(){
		//tooltip = GameObject.Find ("ToolTip");
		tooltip.SetActive (false);
	}

	void Update(){
		if (tooltip.activeSelf) {
			tooltip.transform.position = Input.mousePosition;
		}
	}

	public void Activate(Item item){
		//this.item = item;
		ConstructDataString (item);
		tooltip.SetActive (true);

	}

	public void Deactivate(){

		tooltip.SetActive (false);
	}

	public void ConstructDataString(Item item){

		if (item.rarity == "common") {
			data = "<b><color=#FFFFFFFF>" + item.title + "</color></b>";
		} else if (item.rarity == "un-common") {
			data = "<b><color=#5AF11EFF>" + item.title + "</color></b>";
		} else if (item.rarity == "rare") {
			data = "<b><color=#2A5099FF>" + item.title + "</color></b>";
		} else if (item.rarity == "epic") {
			data = "<b><color=#912CEE>" + item.title + "</color></b>";
		} else if (item.rarity == "legendary") {
			data = "<b><color=#8C1717>" + item.title + "</color></b>";
		}
		tooltip.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = data;
		data = "<color=#A6A6A6>" + item.type + "\n";
		if (item.equipted_Slot == "weapon") {
			data += item.min_Damage + " -" + item.max_Damage + " Damage" + "\t\t" + "Speed " + item.speed.ToString ("F1") + "\n";
			data += "(" + CalculateWeaponDPS (item).ToString ("F1") + " damage per second)" + "</color>";
		}
		tooltip.transform.GetChild (0).GetChild (1).GetComponent<Text> ().text = data;
		//data += "<font_size=5>" +item.type + "</font>";
		//data = "<b><color=#2A5099FF>" + item.title +"</color></b>\n\n" + "Test test";

	}

	public float CalculateWeaponDPS(Item item){
		return ((item.min_Damage + item.max_Damage) / 2) / item.speed;
	}
}
