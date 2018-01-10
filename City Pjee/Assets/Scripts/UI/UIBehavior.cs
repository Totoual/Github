using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;


public class UIBehavior : MonoBehaviour
{	
	
	#region Variables
    public GameObject mainMenuButton;
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject googlePlay;
    public GameObject totalStats;
    public GameObject credits;
    public GameObject pause;
	public GameObject pausePanel;
    public GameObject end;
	public GameObject OnOffVibButton;

    public Slider musicSlider;
    public Slider soundsSlider;
    public AudioSource audioSrc;
    public AudioSource buttonAudioSrc;
    public GameObject player;
    public Transform enemyBirdsTrans;
	public Transform copsHolder;
    public AudioSource copsAudioSrc;
	public AudioSource anarcBoss;
	public AudioSource miniExplosion;
    public Transform explosionsTrans;

    public GameObject gameLogo;
    public GameObject loseScreen;


    //Aristotelis Code here. Variables in order to print the stats
    public Text humanAmountHited;
    public Text grannyAmountHited;
    public Text copsAmountHited;
    public Text totalBount;
    public Text totalMeters;
    public Text totalBoosts;
	public Text totalCorns;
    // End of aristotelis Code.

	public Text pauseHumanAmount;
	public Text pauseGrannyAmount;
	public Text pauseCopsAmount;
	public Text pauseBounty;
	public Text pauseMeters;
	public Text pauseCorn;

	//Aristotelis Code here. Variables for the end screne.
	public Text humansHitted;
	public Text grannyHitted;
	public Text copsHitted;
	public Text meterFlyed;
	public Text highScore;
	public Text bounty;
	public Text runCorns;

	public bool firstTimeRunnedForUI;

	#endregion
	void Awake(){
		int temp = PlayerPrefs.GetInt ("firstRun");
		firstTimeRunnedForUI = temp == 1 ? true : false;
		if (!firstTimeRunnedForUI) {
			musicSlider.value = 1;
			soundsSlider.value = 1;
		} else {
			LoadSettings ();
		}
		if (!firstTimeRunnedForUI) {
			firstTimeRunnedForUI = true;
			PlayerPrefs.SetInt ("firstRun", firstTimeRunnedForUI == true ? 1 : 0);
		}
	}

	void Start(){
		
	}

    // Update is called once per frame
    void Update()
    {
		HandleSounds ();
    }
	public void HandleSounds(){
		audioSrc.volume = musicSlider.value;
		buttonAudioSrc.volume = soundsSlider.value;
		player.GetComponent<AudioSource>().volume = soundsSlider.value;

		foreach(Transform child in enemyBirdsTrans)
		{
			if (child.gameObject.tag == "killerCop")
			{
				child.gameObject.GetComponent<AudioSource>().volume = soundsSlider.value;
			}
		}

		foreach (Transform child in copsHolder) {
			child.gameObject.GetComponent<AudioSource> ().volume = soundsSlider.value;
		}

		foreach (Transform child in explosionsTrans)
		{
			child.gameObject.GetComponent<AudioSource>().volume = soundsSlider.value;
		}
		anarcBoss.volume = soundsSlider.value;
		miniExplosion.volume = soundsSlider.value;
		copsAudioSrc.volume = soundsSlider.value;     
	}


    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        mainMenuButton.GetComponent<Button>().interactable = false;
        gameLogo.SetActive(false);
    }

    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
        mainMenuButton.GetComponent<Button>().interactable = true;
        gameLogo.SetActive(true);
    }

    public void ShowSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
        mainMenuButton.GetComponent<Button>().interactable = false;
		GameManager.instance.LoadVibrateValue ();
		//LoadSettings ();
    }

    public void HideSettings()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
        mainMenuButton.GetComponent<Button>().interactable = true;
		GameManager.instance.Save ();
		SaveSettings ();

    }

    public void ShowGooglePlay()
    {
        googlePlay.SetActive(true);
        mainMenu.SetActive(false);
        mainMenuButton.GetComponent<Button>().interactable = false;

    }

    public void SignInToGP()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) => { });

        }

        //if (Social.localUser.authenticated)
        //{
        //    Social.ShowLeaderboardUI();
        //    Social.ReportScore(GameManager.instance.TotalBounty, PjeeGPG.leaderboard_wpc_bounty, (bool success)=> { });
        //}
        //else
        //{
        //PopupManager.Instance.ShowPopup("Unable to connect", "We were not able to reach Google Play, are you connected to the internet?")
        //}
    }

	//dimiourgisa mia metavliti leaderboard gia na apothikeuetai to call apo to web;
    public void ShowLB()
    {
        if (Social.localUser.authenticated)
        {
			((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GameManager.instance.leaderboard);
        }    
    }

    public void HideGooglePlay()
    {
        googlePlay.SetActive(false);
        mainMenu.SetActive(true);
        mainMenuButton.GetComponent<Button>().interactable = true;
        //Let Google Play Know!!
    }

    public void ShowTotalStats()
    {
        totalStats.SetActive(true);
        mainMenu.SetActive(false);
        PrintTotalStats();
        mainMenuButton.GetComponent<Button>().interactable = false;

    }

    public void HideTotalStats()
    {
        totalStats.SetActive(false);
        mainMenu.SetActive(true);
        mainMenuButton.GetComponent<Button>().interactable = true;
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
        mainMenuButton.GetComponent<Button>().interactable = false;
    }

    public void HideCredits()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
        mainMenuButton.GetComponent<Button>().interactable = true;
    }

    public void ShowFb()
    {
        Application.OpenURL("https://www.facebook.com/TeamRocketGames/");
    }

    public void ShowPause()
    {
        pause.SetActive(true);
		StartCoroutine (PauseTheGame ());

    }

    public void HidePause()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }


    public void ShowEnd()
	{	loseScreen.SetActive(false);
        end.SetActive(true);
		PrintCurrentStats ();
        
    }

    public void PlayAgain()
    {	
		GameManager.instance.Save ();
		GameManager.instance.ResetMeters ();
        end.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void QuitGame()
    {	
		GameManager.instance.Save ();
        Application.Quit();
    }


    //Aristotelis Code Here. Method in order to print the stats
	public void PrintTotalStats()
    {	
			humanAmountHited.text = GameManager.instance.TotalHumanHited.ToString ();
			grannyAmountHited.text = GameManager.instance.TotalGrannyHited.ToString ();
			copsAmountHited.text = GameManager.instance.TotalCopsHited.ToString ();
			totalMeters.text = GameManager.instance.TotalMeters.ToString ();
			totalBoosts.text = GameManager.instance.TotalBoosts.ToString ();
			totalBount.text = GameManager.instance.TotalBounty.ToString ();
			
    }

	public void PauseStats(){
		pauseHumanAmount.text = GameManager.instance.HumanHited.ToString();
		pauseGrannyAmount.text = GameManager.instance.GrannyHited.ToString();
		pauseCopsAmount.text  = GameManager.instance.CopsHited.ToString();
		pauseBounty.text = GameManager.instance.Bounty.ToString ();
		pauseMeters.text = GameManager.instance.Meters.ToString();
		pauseCorn.text = GameManager.instance.Corns.ToString ();
	}

	public void PrintCurrentStats(){
		humansHitted.text = GameManager.instance.HumanHited.ToString();
		grannyHitted.text = GameManager.instance.GrannyHited.ToString();
		copsHitted.text = GameManager.instance.CopsHited.ToString();
		meterFlyed.text = GameManager.instance.Meters.ToString();
		bounty.text = GameManager.instance.Bounty.ToString ();
		highScore.text = GameManager.instance.CalculateHightScore ().ToString ();
		runCorns.text = GameManager.instance.Corns.ToString ();

	}

	public void PrintTotalCorns(){
		totalCorns.text = GameManager.instance.TotalCorns.ToString ();
	}

	public void OnVibrationButtonClick(){
		if (GameManager.instance.vibrate) {
			OnOffVibButton.transform.localPosition = new Vector3 (-36.55f, OnOffVibButton.transform.localPosition.y, OnOffVibButton.transform.localPosition.z);
			GameManager.instance.vibrate = false;
		} else {
			OnOffVibButton.transform.localPosition = new Vector3 (36.55f, OnOffVibButton.transform.localPosition.y, OnOffVibButton.transform.localPosition.z);
			GameManager.instance.vibrate = true;
		}
	}



	// Collision Sound -> Player GameObject
	// Matatzides Sound -> for(int i=0; i<= EnemyBirds.transform.ChildCount; i++)
	// Checkare an exoun tag killerCops an nai valta se mia lista.
	// Cops Pou kinigane -> CopsHolder GameObject.
	// Explosions -> ExplParent GameObject.


	public IEnumerator PauseTheGame(){
		yield return new WaitForSeconds(0.3f);
		Time.timeScale = 0;
	}

	public void SaveSettings(){
		PlayerPrefs.SetFloat ("sliderMusic", musicSlider.value);
		PlayerPrefs.SetFloat ("sliderSounds", soundsSlider.value);
	}

	public void LoadSettings(){
		musicSlider.value = PlayerPrefs.GetFloat ("sliderMusic");
		soundsSlider.value = PlayerPrefs.GetFloat ("sliderSounds");
		HandleSounds ();
	}
}
