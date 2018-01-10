using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GameManager : MonoBehaviour {
#region Variables
	public static GameManager instance = null; // Dimiourgoume to Singleton;
	public GameObject mainCamera;

	public GameObject fightCloud;
	public bool powerUp = false;
	public string leaderboard;
	public AudioClip copsNotFollowingClip;
	public AudioClip copsFollowingClip;
	public float copsSpeed = 13;
	public float eBirdsSpeed = 7;
	public float environmentSpeed = 2;
	public float cornSpeed = 7;
	public float humansSpeed = 6;
	public float carSpeed = 5;
	public GameObject logo;
	public GameObject mainMenuButton;
	public GameObject infoPanel;
	public GameObject score;
	public GameObject deathPanel;
	public GameObject jailImg;
	public Text scoreToScreen;
	public Text currentScoreToScreen;
	public GameObject loseScreen;
	public GameObject statsSceen;
	public Text timerText;
	public GameObject copsHodler;
	public GameObject lightsHolder;
	public GameObject[] lights=new GameObject [2];
	public GameObject eventManager;
	public GameObject player;
	public float minTime = 5f;
	public float maxTime = 12f;
	public Text cornAmount;
	public GameObject instractionsBorder;
	public bool vibrate;
	private int vibrateValue;
	public bool stunned = false;



	private bool playerActive = false;
	private bool gameOver = false;
	private bool gameStarted = false;
	private bool mustFollow = false;
	private bool tutorial = false;
	private bool rewardedLife = false;
	private bool copsCanFollow=false;
	private int meters;

	private int bounty;
	private int currentScore;

	private int copsHited; 
	private int grannyHited;
	private int humanHited;
	private int statueHited;
	private int boosts;
	private int corns;

	private int totalCopsHited; 
	private int totalGrannyHited;
	private int totalHumanHited;
	private int totalStatueHited;
	private int totalBount;
	private int totalMeters;
	private int totalBoosts;
	private int totalCorns;


	private int hightScore;

	//private float soundTimerForCops=0;

	private bool tutorialBool = true;
	public bool canContinueAfterDeath;
	public GameObject MafiaPigeonAfterDeath;

	public bool revived = false;




#endregion 



	#region Getters
	public int TotalCopsHited{get{ return totalCopsHited;}}
	public int TotalGrannyHited{get{return totalGrannyHited;}}
	public int TotalHumanHited{get{return totalHumanHited;}}
	public int TotalStatueHited{get{return totalStatueHited;}}
	public int TotalBounty{get{return totalBount;}}
	public int TotalMeters{get{return totalMeters;}}
	public int TotalBoosts{get{return totalBoosts;}}
	public int TotalCorns{get{return totalCorns;}}
	public int CopsHited{get{return copsHited;}}
	public int GrannyHited{get{return grannyHited;}}
	public int HumanHited{get{return humanHited;}}
	public int Meters{get{return meters;}}
	public int HightScore{get{return hightScore;}}
	public int Bounty{get{return bounty;}}
	public int Corns{get{return corns;}}

	public bool PlayerActive{get{ return playerActive;}}

	public bool GameOver{
		get{ return gameOver;}
	}

	public bool GameStarted{
		get{ return gameStarted;}
	}
	public bool MustFollow{
		get{ return mustFollow;}
	}
	public bool Tutorial{
		get{ return tutorial;}
	}
	public bool RewardedLife{
		get{ return rewardedLife;}
	}
	public bool CopsCanFollow{
		get{ return copsCanFollow;}
	}
	#endregion
	/// <summary>
	/// We give the singlenton a value if it is null else we destroy it.
	/// </summary>
	void Awake(){
		//PlayerPrefs.DeleteAll ();
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		Load ();

		//Delete before Publish


    }

    void Start(){
		//Activate Google Play
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;

		lightsHolder.transform.GetChild (0).gameObject.SetActive (true);
		lightsHolder.transform.GetChild (1).gameObject.SetActive (false);

	}

	void Update(){
		
		if (!GameOver) {
			if (GameManager.instance.Meters % 50 == 2 && !powerUp) {
				copsSpeed += Time.deltaTime * 5;
				eBirdsSpeed += Time.deltaTime * 5;
				carSpeed += Time.deltaTime * 5;
				humansSpeed += Time.deltaTime * 5;
				cornSpeed += Time.deltaTime * 5;
				if (minTime > 0.5f) {
					minTime -= Time.deltaTime * 5;

				}
				if (maxTime > 0.7f) {
					maxTime -= Time.deltaTime * 10;
				}
				if (environmentSpeed <= 12f) {
					environmentSpeed += Time.deltaTime * 5;
				}
				player.GetComponent<Rigidbody2D> ().mass += Time.deltaTime * 5;
				//Debug.Log ("I added speed");
				if (environmentSpeed >= 10) {
					player.GetComponent<Rigidbody2D> ().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
				}

			} else if (powerUp) {
				if (environmentSpeed > 6) {
					environmentSpeed -= Time.deltaTime * 10;
					eBirdsSpeed -= Time.deltaTime * 10;
					carSpeed -= Time.deltaTime * 10;
					humansSpeed -= Time.deltaTime *10;
					copsSpeed -= Time.deltaTime * 10;
					cornSpeed -= Time.deltaTime * 10;
					minTime += Time.deltaTime * 5;
					maxTime += Time.deltaTime * 10;

				} 
				if (player.GetComponent<Rigidbody2D> ().mass > 6) {
					player.GetComponent<Rigidbody2D> ().mass = 6;
				}
				if (environmentSpeed <=6) {
					powerUp = false;
				}
				if (environmentSpeed < 10) {
					player.GetComponent<Rigidbody2D> ().collisionDetectionMode = CollisionDetectionMode2D.Discrete; 
				}

			}
		
		}


        CalculateBounty();


    }

	/// <summary>
	/// We inform the gameManager that the player started the game.
	/// We disactivate the main menu and the logo. But we enable the score.
	/// </summary>
	public void PlayerStartedGame(){
		playerActive = true;
		mainMenuButton.SetActive (false);
		logo.SetActive (false);
		score.SetActive (true);
	}

	/// <summary>
	/// We inform the game manager that the player is in the game
	/// </summary>
	public void EnterGame(){
		gameStarted = true;
	}

	/// <summary>
	/// if we use tutorial this is the end function :P
	/// </summary>
	public void EndOfTutorial(){
		instractionsBorder.GetComponent<Animator> ().SetBool ("OnClick", true);
		tutorial = true;
		StartCoroutine (EndAnimationForInstructions ());
	}

	public void CloseThePanle(){
		instractionsBorder.GetComponent<Animator> ().SetBool ("OnClick", false);
		infoPanel.SetActive (false);
		if (tutorialBool) {
			CallTheCops.CallMeOver.CallMoreCops ();
			tutorialBool = false;
		}
	}
	/// <summary>
	/// We are starting the tutorial. 
	/// </summary>
	public void StartTutorial(){
		if (!tutorial) {
			infoPanel.SetActive (true);
		}
	}


	/// <summary>
	/// We handle the printing of the meters on the screen.
	/// </summary>
	/// <param name="metersToAdd">Meters to add.</param>
	public void AddToMeters(float metersToAdd){
		//Debug.Log (metersToAdd);
		meters = (int)metersToAdd;
		scoreToScreen.text = meters.ToString () + " m";

	}

	public void ResetMeters(){
		meters = 0;
	}

	/// <summary>
	/// we handle the printing of the score.
	/// </summary>
	public void AddToScorePoints(){
		currentScoreToScreen.text = currentScore.ToString ();
	}

	public void AddToCorns(){
		corns++;
		cornAmount.text = corns.ToString ();
	}
	/// <summary>
	/// We can indetify the id in order to award the points.
	/// </summary>
	/// <param name="id">Identifier.</param>
	public void AddToScore(int id){
		if (id == 1) {
			humanHited++;
			currentScore += 250;
		} else if (id == 2) {
			grannyHited++;
			currentScore += 500;
		} else if (id == 3) {
			copsHited++;
			currentScore += 1000;
		} else if (id == 4) {
			statueHited++;
			currentScore = (currentScore - meters) * 2;
		} else if (id == 5) {
			boosts++;
			currentScore += 750;
		}
		//Debug.Log (currentScore);
	}
	/// <summary>
	/// The formula that calculates the score.
	/// </summary>
	public void CalculateBounty(){
		bounty = (meters + (grannyHited * 500) + (humanHited * 250) + (copsHited * 1000));
	}

	public int CalculateHightScore(){
		if (bounty > hightScore) {
			hightScore = bounty;
		}
		return hightScore;
	}


	// Here is the death function. We set the gameOver true and enabling the deathPanel.
	// Also we start the coroutine in order to give the player a chance to revive Pjee.
	public void Death(){
		Debug.Log (revived);
		gameOver = true;
		eventManager.GetComponent<UIBehavior>().musicSlider.value -= (eventManager.GetComponent<UIBehavior>().musicSlider.value*0.5f);
		//deathPanel.SetActive (true);
		jailImg.SetActive (true);
		eventManager.GetComponent<UIBehavior> ().PrintTotalCorns ();
		score.SetActive (false);
		if (rewardedLife) {
			rewardedLife = !rewardedLife;
		}
		if (!revived) {
			deathPanel.SetActive (true);
		} else {
			OnForwardClick ();
		}
    }

    /// <summary>
    /// Here we handle the forward button in the death scene.
    /// </summary>
    public void OnForwardClick(){
		loseScreen.SetActive (false);
		statsSceen.SetActive (true);
		jailImg.SetActive (true);
		eventManager.GetComponent<UIBehavior> ().PrintCurrentStats ();

		//Submit leaderboard Scores , if authenticated
		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{	
			// Note: make sure to add using GooglePlayGames
			PlayGamesPlatform.Instance.ReportScore (CalculateHightScore (), PjeeGPG.leaderboard_citypjee_bounty,
				(bool success) => {
					Debug.Log ("LeaderBoard Update success : " + success);
				});
		}

		// End leaderBoard Update Code.

	}

	/// <summary>
	/// Rewarding player for watching the ad.
	/// We give him on more chance to go further in the map.
	/// </summary>
	public void RewardPlayer(){
		gameOver = false;
		rewardedLife = true;
		deathPanel.SetActive (false);
		score.SetActive (true);
		jailImg.SetActive (false);
		MafiaPigeonAfterDeath.SetActive(true);
		canContinueAfterDeath = true;
		// An other way to reward player!
		player.GetComponent<CircleCollider2D>().enabled = true;
		eventManager.GetComponent<UIBehavior>().musicSlider.value += (eventManager.GetComponent<UIBehavior>().musicSlider.value*0.5f);
		mainCamera.GetComponent<AudioSource> ().pitch = 1;
		copsHodler.transform.position = new Vector3 (-12,copsHodler.transform.position.y,copsHodler.transform.position.z);
		player.transform.position = new Vector2 (0, -0.8f);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		Time.timeScale = 0;

	}

	public void RewardPlayerwithCorns(){
		if (totalCorns >= 500) {
			totalCorns -= 500;
			RewardPlayer ();
		}
	}


	/// <summary>
	/// Save this instance.
	/// </summary>
	public void Save (){
		totalHumanHited += humanHited;
		totalGrannyHited += grannyHited;
		totalCopsHited += copsHited;
		totalStatueHited += statueHited;
		totalMeters += meters;
		totalBount += bounty;
		totalBoosts += boosts;
		totalCorns += corns;
	
		PlayerPrefs.SetInt ("humansHited", totalHumanHited);
		PlayerPrefs.SetInt ("grannyHited", totalGrannyHited);
		PlayerPrefs.SetInt ("copsHited", totalCopsHited);
		PlayerPrefs.SetInt ("statueHited", totalStatueHited);
		PlayerPrefs.SetInt ("meters", totalMeters);
		PlayerPrefs.SetInt ("bounty", totalBount);
		PlayerPrefs.SetInt ("boosts", totalBoosts);
		PlayerPrefs.SetInt ("hightScore", hightScore);
		PlayerPrefs.SetInt ("corns", totalCorns);
		SaveVibrateValue ();

	}
	public void LoadVibrateValue(){
		vibrateValue = PlayerPrefs.GetInt ("vibrateValue");
		vibrate = vibrateValue == 1 ? true : false;
		Debug.Log (vibrate);
	}

	public void SaveVibrateValue(){
		vibrateValue = vibrate == true?1:0 ;
		PlayerPrefs.SetInt ("vibrateValue", vibrateValue);
		Debug.Log (vibrateValue);

	}
	/// <summary>
	/// Load this instance.
	/// </summary>
	public void Load(){
		totalHumanHited = PlayerPrefs.GetInt ("humansHited");
		totalGrannyHited = PlayerPrefs.GetInt ("grannyHited");
		totalCopsHited = PlayerPrefs.GetInt ("copsHited");
		totalStatueHited = PlayerPrefs.GetInt ("statueHited");
		totalMeters = PlayerPrefs.GetInt ("meters");
		totalBoosts = PlayerPrefs.GetInt ("boosts");
		totalBount = PlayerPrefs.GetInt ("bounty");
		hightScore = PlayerPrefs.GetInt ("hightScore");
		totalCorns = PlayerPrefs.GetInt ("corns");
		vibrateValue = PlayerPrefs.GetInt ("vibrateValue");
		LoadVibrateValue ();

	}

	IEnumerator CopsSiren(){
		lights [0].SetActive (!lights[0].activeInHierarchy);
		lights[1].SetActive(!lights[1].activeInHierarchy);
		yield return null;
	}

	IEnumerator EndAnimationForInstructions(){
		yield return new WaitForSeconds (1);
		CloseThePanle ();
	}
}

			
