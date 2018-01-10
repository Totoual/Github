using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleAlphaChannel : MonoBehaviour {

	public float fadeInTime;

	public GameObject fadePanel;					// Pernoume to panel apo to hierarcy
	private Color currentColor = Color.black;		// Thetoume to xrwma se mavro
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		FadeIn ();									// Kaloume to method
	}

	void FadeIn(){
		if (Time.timeSinceLevelLoad < fadeInTime) {							// Elegxoume an to fade in einai megalitero apo ton xrono pou exoume thesei gai to nexti level to load
				float alphaChange = Time.deltaTime / fadeInTime;			// Dimiourgoume ena float gia na alazoume to alpha tis eikonas
				currentColor.a -= alphaChange;								// Allazoume to alpha chanel
				fadePanel.GetComponent<Image> ().color = currentColor;		// Orizoume to xrwma simfwna me to xrwma pou exoume orisei
		}  else {
			fadePanel.gameObject.SetActive (false);							// Thetoume to panel inactive
		}

	
	}
}
