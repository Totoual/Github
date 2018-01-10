using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonId : MonoBehaviour, IPointerDownHandler {

	public int id;										// Orizoume ena ID gia to kathe koubi pou ginete instantiate apo to controller
	public Config configuration;						// pernoume ena instance tou config script

	void Start(){
		configuration = GameObject.Find ("Options").GetComponent<Config> ();
	}

	public void OnPointerDown(PointerEventData eventData){			// Otan to podiki kanei click panw sto koubi
		GameObject obj = this.gameObject;							// Orizoume mia proswrini metavliti typou gameObject gia na tin perasoume sto Remap
		configuration.RemapKeys (id , obj);							// Kaloume tin remapKeys
		configuration.isActive = true;								// Kai orizoume oti yparxei energo remaping

	}

}
