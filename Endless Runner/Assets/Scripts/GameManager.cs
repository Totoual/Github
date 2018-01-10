using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool startGame = false;		// bool metavliti gia to an exei ksekinisei to game
	public int points = 0;				// Oi podoi tou paixti
	public GameObject player;			// O paixtis
	public GameObject instanceOfPlayer;	// To instance tou paixti sto main menu
	public GameObject gameOverScene;	// To gameOVer gameObject
	public GameObject mainMenu;			// To mainMenu gameObject
	public GameObject activeGameCanvas;	// O canvas pou einai energopoiimenos kata tin diarkeia tou game
	public bool notDead = true;			// Bool gia to an o paixtis einai nekros
	public Text txt;					// To score gia to gameOver Scene
	public Text activeScore;			// To score gia to active Scene
	// Use this for initialization


	public void OnStart(){
		instanceOfPlayer.GetComponent<Animator> ().SetTrigger ("CanPlayTheAnim");		// Pernoume ton animator Controller gia na paiksei to animation otan patei  o xristis start
		instanceOfPlayer.GetComponent<AudioSource> ().Play ();							// Paizoume ton ixo gia to start
		StartCoroutine (WaitSomeSeconds ());											// Perimenoume merika deuterolepta prin energopoiisoume tin alli kamera gia na dei o xrists to animation kai na akousei ton ixo
	}

	void Update(){
		activeScore.text = points.ToString ();					// Orizoume to score
	}



	IEnumerator WaitSomeSeconds(){
		yield return new WaitForSeconds (1.2f);
		mainMenu.SetActive (false);						// Apenergopoioume to mainMenu
		activeGameCanvas.SetActive (true);				// Energopoioume to score 
		startGame = true;								// Orizoume an exei ksekinisei to game
	}

	public void Quit(){
		Application.Quit ();
	}

	public void Restart(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

	public void Death(){
		gameOverScene.SetActive (true);				// Energopoioume to gameOver object
		activeGameCanvas.SetActive (false);			// Apenergopoioume ta points tou gia na ta emfanisoume se allo simeio
		player.SetActive (false);					// Apenergopoioume ton paixti
		txt.text = points.ToString ();				// Emfanizoume to score tou se allo simeio stin othoni
	}
}
