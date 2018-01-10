using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;			// Dimiourgoume to singleton

	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private GameObject scoreCanvas;
	[SerializeField] private Text deathScore;
	[SerializeField] private Text scorePanel;
	[SerializeField] private Text highScoreText;

	private bool playerActive = false;  				// Elegxoume an o paixtis patise click
	private bool gameOver = false; 						// Elegxoume an o paixtis exase
	private bool gameStarted = false;					// Elegxoume an to paixnidi arxise
	private bool playerGotPoint = false;				// Elegxoume an o paixtis pernei kapoio podo
	private bool skullReseted = false;					// Elegxoume an exei ksekinsei to game
	private int points=0;								// Oi pontoi tou paixnidiou
	private int targetScore = 30;						// Orizoume ena score gia na elegxoume
	private static float speed = 2;						// Orizoume mia statiki taxytita gia ta objects tou game
	private int highscore = 0;							// Oriszoume to high score gia to paixti


#region Getters
	public float Speed{
		get{ return speed;}
	}

	public bool SkullReseted{
		get{ return skullReseted;}
	}

	public bool PlayerActive{
		get { return playerActive;}						// Dimiourgoume ena getter gia na boroun na doun tin timi ta alla script
	}
	public bool GameOver{
		get{ return gameOver;}							// Dimiourgoume ena getter gia na boroun na doun tin timi ta alla script
	}

	public bool GameStarted{							// Dimiourgoume ena getter gia na boroun na doun tin timi ta alla script
		get{ return gameStarted;}
	}

	public bool PlayerGotPoint{
		get{ return playerGotPoint;}
	}
#endregion
	void Awake(){
		if (instance == null) {							// Elegxoume an einai keno
			instance = this;							// An nai tou thetoume oti einai auto to instance 
		} else if (instance != this) {					// Alliws
			Destroy (gameObject);						// To katastrefoume
		}

		//DontDestroyOnLoad (gameObject);					// Kai tou thetoume na min katastrefete sto load

		Assert.IsNotNull (mainMenu);
	}

	void Start(){
		scoreCanvas.SetActive (false);						// Orizoume to scoreCanvas sto false ( Gia na emfanistei to main menu
		highscore = PlayerPrefs.GetInt ("HighScore");		// Arxikopoioume to high score symfwna me to ti exei o xristis sto registri tou pc tou
		
	}
		
	// Update is called once per frame
	void Update () {			
		AddSpeed ();										// Kaloume tin addSpeed()
	}

	public void PlayerCollided(){						   // Methodos gia na allazoume tin timi tou game over
		gameOver = true;
		gameOverPanel.SetActive (true);					  				// Energopoioume to panel tou game over
		deathScore.text = points.ToString();			  				// Dinoume timi sto text stou gameOver 
		GetComponent<AudioSource> ().pitch = 0.49f;		  				// Meiwnoume to pitch tou ixou gia na fenete ligo diaforetikos
		scoreCanvas.SetActive (false);					  				// Apenergopoioume to canva tou score
		highScoreText.text = CalculateHighScore ().ToString ();			// Ypologizoume to high score
	}

	public int CalculateHighScore(){					
		if (points >= highscore) {									// Elegxoume an oi pontoi einai megaliteroi apo to highscore
			highscore = points;										// Orizoume oti to high score einai idio me tous pontous
			PlayerPrefs.SetInt ("HighScore", highscore);			// Kanoume save to high score sto registry tou xristi
			return highscore;										// Epistrefoume to high score
		}
			return highscore;										// An den einai oi pontoi einai mikroteroi apo to high score apla epistrefoume to high score me tin timi pou eixe


	}

	public void PlayerStartedGame(){					   // Methodos gia na allazoume tin timi tou playerActive
		playerActive = true;								
		scoreCanvas.transform.GetChild (3).gameObject.SetActive (false);			//Apenergopoioume to score gia na min fenete sto main menu
	}

	public void EnterGame(){										// Methodos pou dilwnmei oti o xristis patise play
		GetComponent<AudioSource> ().pitch = 1;						// Allazoume to pitch tou ixou gia na einai fisiologiko
		mainMenu.SetActive (false);									// Orizoume to main menu ws false
		scoreCanvas.SetActive (true);								// Energopoioume to score
		gameStarted = true;											// Dilwnoume oti to paixnidi arxise
	}

	public void ReplayGame(){										// Methodos gia epanalipsi tou paixnidiou
		mainMenu.SetActive (false);									// Kleinoume to main menu ksana
		gameOverPanel.SetActive (false);						    // kai to panel tou game over
		speed = 2;													// Orizoume tin taxytita sto 2 ( Gia na tin kanoume reset)
		Application.LoadLevel (0);									// Kai kanoume load to level;
	}

	public void PlayerCollideWithSkull(){							// Methodos gia na elegxoume an o paixtis ekane collide me kapoio skull
		playerGotPoint = true;										
	}			

	public void OnQuit(){											// Methodos gia to quit
		Application.Quit ();
	}

	public void EnableSkullComponents(){							// Methodos gia na elegxoume an prepei na knaoume enable ta component tou kathe skull
		skullReseted = true;
	}
	public void DisableSkullComponents(){							// Methodos gia na elegxoume an prepei na kanoume disable ta component tou kathe skull
		skullReseted = false;
	}

	public void AddPoints(int points){								// Methodos pou prosthetei tous podous sto game;
		this.points += points;
		scorePanel.text = this.points.ToString();

	}

	public void AddSpeed(){											// Methodos pou prosthetei taxytita sto game;
		if (CanAddSpeed ()) {										// Elegxoume an prepei na valoume taxytita
			speed += 0.5f;
		}

	}

	public bool CanAddSpeed	(){										// Elegxos gia to an prepei na valoume taxytita
		if (points >= targetScore) {								// Elegxoume an to score pou exoume twra einai idio i megalitero me to epithymito score
			targetScore += 30;										// An einai sto epithymito score prosthetoume 30 pontous
			Debug.Log (targetScore);
			return true;											// Kai epistrefoume true pou simainei oti boroume na prosthesoume taxytita
		}
		return false;												// Alliws epistrefoume false;
	}


}
