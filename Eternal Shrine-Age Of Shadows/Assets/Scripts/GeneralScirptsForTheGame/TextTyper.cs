using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTyper : MonoBehaviour {
	public float letterPause = 0.2f;
	public float fadeInSeconds;
	public bool isEnded = false;		// Gia na elegxoume an exei teleiwsei to coroutine


	public string message;				// to message pou exoume grapsei
	public Text textComp;				// Gia na paroume to text apo to hierarchy pou exoume grapsei
	private Color currentColor = Color.black;

	// Use this for initialization
	void Start () {
		textComp = GetComponent<Text> ();		// Pernoume to text apo to adikeimeno
		message = textComp.text;				// kai to orizoume stin metavliti mas 
		textComp.text = null;						// Adeiazoume to text sto hierarchy
		StartCoroutine (TypeText ());			// Ksekiname tin coroutine
		currentColor.a = 0;

	
	}

	void Update(){
		FadeInForText ();
	
	}

	IEnumerator TypeText(){
		textComp.text = "";
		foreach (char letter in message.ToCharArray()) {			// Efoson exoume orisei ena string syberiferete san pinakas me chars opote boroume na xrisimopoiisoume for
			textComp.text += letter;								// kai synthetoume to keimeno 
			isEnded = false;										// Tou leme oti den exei teleiwsei
			yield return 0;											// Epistrefoume 0
			yield return new WaitForSeconds (letterPause);			// Perimenoume analoga me to float pou tou exoume orisei
			isEnded = true;											// Orizoume tin isEnded san true giati teleiwse to coroutine
		
		}	
	
	}
	/// <summary>
	/// Ksekiname to typing tou keimenou me kapoio fade in gia na min fenetai asximo
	/// O xronos pou tha kanei fade orizetai apo to inspector.
	/// </summary>
	public void FadeInForText(){
		if (Time.timeSinceLevelLoad < fadeInSeconds) {						
			float alphaChange = Time.deltaTime /fadeInSeconds;					
			//Debug.Log (alphaChange);

			currentColor.a += alphaChange;
			textComp.color = currentColor;
		
		}

		
	
	}
	
}

