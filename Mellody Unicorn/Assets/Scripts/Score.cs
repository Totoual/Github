using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	static public int score = 0;				// Orizoume mia metavliti gia to score
	static public int hightScore = 0;			// Orizoume mia metavliti gia to highscore
	Text txt;									// kai mia Metavliti typou text gia na boresoume na typosoume to score stin othoni


	void Start(){

		txt = GetComponent<Text> ();								// Pernoume ta stoixeia tou text apo to object
		hightScore = PlayerPrefs.GetInt ("HightScore", 0);			// Thetoume to highscore iso me auto pou exoume apothikeusi stin syskeui i to pc tou paixti
	
	}
	// Update is called once per frame
	void Update () {
		txt.text = "Score: " + score + "\nHigh Score: "+ hightScore;		// Typonoume to score stin othoni.

	}
	/// <summary>
	/// Einai ypeuthini gia na anevazei to score tou paixti kathe fora pou tin kaloume.
	/// kai na elegxei an exoume kanei kainourgio high score
	/// </summary>
	static public void AddPoint(){
		score++;											// Anevazei to score kata ena kathe fora							
		if (score > hightScore) {							// Elegxoume an exoume kainourgio high Score

			hightScore = score;								// Orizoume sto high score to score
			//Debug.Log ("I am trying to add points");
		}
	}

	void OnDestroy(){										
						
		PlayerPrefs.SetInt ("HightScore", hightScore);  	// Otan xanoume apothykeyoume to highScore
	
	}
}
