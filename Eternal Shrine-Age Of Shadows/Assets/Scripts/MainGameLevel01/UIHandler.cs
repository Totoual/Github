using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public class UIHandler : MonoBehaviour {
	public Image HealthPoints;
	public Image RagePoints;
	public Image XpBar;
	public Text Leveltext;
	public GameObject skill;
	public GameObject skillbar;
	public GameObject inventoryPanel;
	public GameObject characterShitPanel;
	public GameObject achievmentPannel;
	public GameObject pausePanel;
	public GameObject optionsPanel;
	public GameObject buttonMusicPlayer;
	public GameObject miniMap;
	public GameObject expBar;
	public GameObject actionBar;
	public GameObject healthBar;
	public GameObject buttons;
	public GameObject enemyHealth;
	public List<GameObject> enemiesAffected = new List<GameObject>();
	public GameObject generalCanvas;
	public GameObject deathScreen;
	public GameObject cameraObj;

	//public GameObject lootbox;




	public Text strenght_txt;
	public Text stamina_txt;
	public Text intellect_txt;
	public Text agillity_txt;
	public Text spirit_txt;
	public Text armor_txt;
	public Text iron_Currency_txt;
	public Text leather_Currency_txt;
	public Text cloth_Currency_txt;

	public PlayerCalculations playerCalc;
	public PlayerManager playerMan;
	public bool canUseHotkeys = true;
	public bool canUseCharge = true;

	private bool isActiveInv=true;
	private bool isActiveChar = true;
	private bool isActiveAchiev = true;
	private bool isActivePause = true;
	private bool isActiveEnemy;
	private bool isActiveOptions = true;
	private GameObject player;
	private string temp;
	private bool canDrinkAgain = true;
	private bool canUseCleave = true;
	private bool canHowl = true;
	private bool canDragonSwipe = true;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		//CreateSkills ();
		inventoryPanel.SetActive (false);
		characterShitPanel.SetActive (false);
		achievmentPannel.SetActive (false);
		pausePanel.SetActive (false);
		PrintAttributes ();
	}

	// Update is called once per frame
	void Update () {
		ChangeUI ();
	}

	public void ChangeUI(){
		HealthPoints.rectTransform.localScale = new Vector3 ((float)playerCalc.currentHealth /(float) playerCalc.CalculateHealth(playerMan.Stamina), HealthPoints.rectTransform.localScale.y, HealthPoints.rectTransform.localScale.z);
		RagePoints.rectTransform.localScale = new Vector3 (((float)playerCalc.CalcualteRage() / playerCalc.maxRage), RagePoints.rectTransform.localScale.y, RagePoints.rectTransform.localScale.z);
		XpBar.rectTransform.localScale = new Vector3 ((float)playerMan.XpToLevelUp/ (float)playerMan.MaxXp, XpBar.rectTransform.localScale.y, XpBar.rectTransform.localScale.z);
		Leveltext.text = playerMan.Level.ToString();
		PrintAttributes ();
		iron_Currency_txt.text = playerMan.IronCurrency.ToString();
		leather_Currency_txt.text = playerMan.WoodCurrency.ToString();
		cloth_Currency_txt.text = playerMan.ClothCurrency.ToString();

	}

//	public void CreateSkills(){
//		for (int i = 0; i < playerMan.skills.Count; i++) {
//			GameObject skillbutton = Instantiate (skill);
//			skillbutton.transform.SetParent (skillbar.transform);
//			skillbutton.transform.localPosition = Vector2.zero;
//			skillbutton.transform.localScale = new Vector3 (1, 1, 1); // Orizoume to arxiko megethos tou eikonidiou
//			skillbutton.transform.GetChild(1).GetComponent<Image>().sprite =  Resources.Load<Sprite> ("Sprites/Class/" + playerMan.skills[i].Skill_Slug);
//		}
//	}

	public void OnInventoryClick(){
		inventoryPanel.SetActive (isActiveInv);
		CallTheMusic (0,isActiveInv);
		isActiveInv = !isActiveInv;

			
	}

	public void OnCharacterShitClick(){
		characterShitPanel.SetActive (isActiveChar);
		CallTheMusic (1,isActiveChar);
		isActiveChar = !isActiveChar;


	}

	public void OnOptionsClick(){
		optionsPanel.SetActive (isActiveOptions);
		CallTheMusic (1,isActiveOptions);
		isActiveOptions = !isActiveOptions;
		
	}

	public void OnAchievmentClick(){
			achievmentPannel.SetActive (isActiveAchiev);
			CallTheMusic (1, isActiveAchiev);
			isActiveAchiev = !isActiveAchiev;
	}
#region Skills
	public void OnDragonSwipClick(){
		if (playerCalc.rage >= 15 && canDragonSwipe) {
			canDragonSwipe = false;
			player.GetComponent<ButtonMusicPlayer> ().PlayPlayerDragonSwipeClip ();
			player.GetComponent<Animator> ().SetTrigger ("CanSwipe");
			Collider[] objs;
			objs = Physics.OverlapSphere (player.transform.position + Vector3.up, 5.0f, 1 << LayerMask.NameToLayer ("Interactable"));
			enemiesAffected = new List<GameObject> ();
			foreach (Collider col in objs) {
				if (col.gameObject.tag == "Enemy") {
					enemiesAffected.Add (col.gameObject);
				}
			}
			for (int i = 0; i < enemiesAffected.Count; i++) {
				enemiesAffected [i].GetComponent<EnemyStats> ().currentHealth -= playerCalc.DragonSwipeDmg ();
				enemiesAffected [i].GetComponent<EnemyStats> ().Death ();

			}
			player.GetComponent<WorldInteractions> ().DrawEnemyHealth (player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ());
			StartCoroutine (DragonSwipeCoolDown ());
			Debug.Log (playerCalc.DragonSwipeDmg ());
		}else {
			Debug.Log ("Cannot Do it Right now");
		}

	}
	IEnumerator DragonSwipeCoolDown(){
		yield return new WaitForSeconds (5f);
		Debug.Log ("You can swipe again");
		canDragonSwipe = true;
	}
	public void OnHowlClick(){
		if (playerCalc.rage >= 5 && canHowl) {
			canHowl = false;
			player.GetComponent<ButtonMusicPlayer> ().PlayPlayerHowlClip ();
			player.GetComponent<Animator> ().SetTrigger ("CanHowl");
			playerCalc.rage -= 5;
			Collider[] objs;
			objs = Physics.OverlapSphere (player.transform.position + Vector3.up, 5.0f, 1 << LayerMask.NameToLayer ("Interactable"));
			enemiesAffected = new List<GameObject> ();
			foreach (Collider col in objs) {
				if (col.gameObject.tag == "Enemy") {
					enemiesAffected.Add (col.gameObject);
				}
			}
			for (int i = 0; i < enemiesAffected.Count; i++) {
				enemiesAffected [i].GetComponent<EnemyStats> ().immobilised = true;
			}

			StartCoroutine (HowlCoolDown ());

		} else {
			Debug.Log ("Cannot Do it Right now");
		}
	}

	IEnumerator HowlCoolDown(){
		yield return new WaitForSeconds (4f);
		for (int i = 0; i < enemiesAffected.Count; i++) {
			enemiesAffected [i].GetComponent<EnemyStats> ().immobilised = false;
			enemiesAffected [i].GetComponent<EnemyStats> ().StartTheAttackAgain(player);
		}
		yield return new WaitForSeconds (2f);
		canHowl = true;
	}

	public void OnCleaveClick(){
		if (playerCalc.rage >= 20 && canUseCleave) {
			if (player.GetComponent<WorldInteractions> ().selectedTarget != null) {
				player.GetComponent<ButtonMusicPlayer> ().PlayPlayerCleaveClip ();
				player.transform.GetChild (0).transform.GetChild (0).transform.gameObject.SetActive (true);
				player.GetComponent<Animator> ().SetTrigger ("CanCleave");

				playerCalc.rage -= 20;
				player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ().currentHealth -= playerCalc.CleaveDmg ();
				player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ().Death ();
				player.GetComponent<WorldInteractions> ().DrawEnemyHealth (player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ());
				canUseCleave = false;
				Debug.Log (player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ().currentHealth);
				StartCoroutine (CleaveCoolDown ());
			}
		} else {
			Debug.Log ("Can not use it now");
		}
	}

	IEnumerator CleaveCoolDown(){
		yield return new WaitForSeconds (2f);
		player.transform.GetChild (0).transform.GetChild (0).transform.gameObject.SetActive (false);
		yield return new WaitForSeconds (3f);
		canUseCleave = true;
	}

	// Auto kanonika prepei na paei sto player calculations
	public void OnChargeClick(){
		if (player.GetComponent<WorldInteractions> ().selectedTarget != null && canUseCharge) {
			if (Vector3.Distance (player.transform.position, player.GetComponent<WorldInteractions> ().selectedTarget.parent.transform.position) <= 50f && 
				Vector3.Distance (player.transform.position, player.GetComponent<WorldInteractions> ().selectedTarget.parent.transform.position) >=5f) 
			{	
				player.GetComponent<ButtonMusicPlayer> ().PlayPlayerChargeClip ();
				player.GetComponent<Animator> ().SetTrigger ("CanCharge");	
				player.transform.GetChild (10).gameObject.SetActive (true);
				player.GetComponent<NavMeshAgent> ().destination = player.GetComponent<WorldInteractions> ().selectedTarget.position;
				player.GetComponent<NavMeshAgent> ().speed = 50;
				player.GetComponent<NavMeshAgent> ().acceleration = 200;
				player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ().immobilised = true;
				player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<NavMeshAgent> ().speed = 0f;
				StartCoroutine (ChargeCoolDown ());
			}
			canUseCharge = false;

			//Debug.Log (Vector3.Distance (player.transform.position, player.GetComponent<WorldInteractions> ().selectedTarget.parent.transform.position));
		}else {
			Debug.Log ("Can not use it now");
		}
	}
	// Auto prepei na paei sto player calculations;
	IEnumerator ChargeCoolDown(){
		yield return new WaitForSeconds (1f);
		player.GetComponent<NavMeshAgent> ().speed = 5;
		player.GetComponent<NavMeshAgent> ().acceleration = 8;
		player.transform.GetChild (10).gameObject.SetActive (false);
		yield return new WaitForSeconds (1f);

		if (player.GetComponent<WorldInteractions> ().selectedTarget != null) {
			player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ().immobilised = false;
			player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<NavMeshAgent> ().speed = 3.5f;
			player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyBehavior> ().FollowPlayer (player);
			player.GetComponent<WorldInteractions> ().selectedTarget.GetComponent<EnemyStats> ().StartTheAttackAgain (player);
		}
		yield return new WaitForSeconds (2f);
		Debug.Log ("I can use charge Again");
		canUseCharge = true;
	}

	public void OnPotionClick(){
		if (playerCalc.currentHealth < playerCalc.CalculateHealth (playerMan.Stamina) && canDrinkAgain) {
			canDrinkAgain = false;
			buttonMusicPlayer.GetComponent<ButtonMusicPlayer> ().PlayPotionClip();
			playerCalc.currentHealth += playerCalc.CalculateHealth (playerMan.Stamina) * 25 / 100;
			if(playerCalc.currentHealth >playerCalc.CalculateHealth (playerMan.Stamina)){
				playerCalc.currentHealth = playerCalc.CalculateHealth(playerMan.Stamina);
			}
			StartCoroutine(PotionCoolDown());
		}
	}

	IEnumerator PotionCoolDown(){
		yield return new WaitForSeconds (5f);
		canDrinkAgain = true;
	}
#endregion
	public void OnPauseClick(){
		pausePanel.SetActive (isActivePause);
		CallTheMusic (1,isActivePause);
		canUseHotkeys = false;
		if (isActivePause) {
			//player.GetComponent<Animation> ().Play (animation: "free");
			Time.timeScale = 0;
			player.transform.GetChild (9).gameObject.SetActive (true);
			characterShitPanel.SetActive (false);
			inventoryPanel.SetActive (false);
			achievmentPannel.SetActive (false);
			miniMap.SetActive (false);
			expBar.SetActive (false);
			actionBar.SetActive (false);
			healthBar.SetActive (false);
			buttons.SetActive (false);
			if (enemyHealth.activeSelf) {
				isActiveEnemy = true;
				enemyHealth.SetActive (false);
			} else {
				isActiveEnemy = false;
			}
		} else {
			//player.GetComponent<Animation> ().Play (animation: temp);
			Time.timeScale = 1;
			canUseHotkeys = true;
			player.transform.GetChild (9).gameObject.SetActive (false);
			miniMap.SetActive (true);
			expBar.SetActive (true);
			actionBar.SetActive (true);
			healthBar.SetActive (true);
			buttons.SetActive (true);
			if (isActiveEnemy) {
				enemyHealth.SetActive (true);
			}
			if (optionsPanel.activeSelf) {
				optionsPanel.SetActive (false);
				isActiveOptions = true;
			}
			if (achievmentPannel.activeSelf) {
				achievmentPannel.SetActive (false);
				isActiveAchiev = true;
			}

		}
		isActivePause = !isActivePause;
		
	}

	public void PrintAttributes(){
		stamina_txt.text = playerMan.Stamina.ToString ();
		strenght_txt.text = playerMan.Strength.ToString ();
		agillity_txt.text = playerMan.Agility.ToString ();
		intellect_txt.text = playerMan.Intellect.ToString ();
		spirit_txt.text = playerMan.Spirit.ToString ();
		armor_txt.text = playerCalc.CalculateArmor (playerMan.Agility).ToString();
	}

	public void CallTheMusic(int id , bool isActive){
		if (isActive) {
			//Debug.Log ("I m trying to play the music");
			buttonMusicPlayer.GetComponent<ButtonMusicPlayer> ().PlayOpenClip (id);
		} else {
			buttonMusicPlayer.GetComponent<ButtonMusicPlayer> ().PlayCloseClip (id);
		}
	}

	public void OnDeath(){
		Time.timeScale = 0.2f;
		generalCanvas.SetActive (false);
		deathScreen.SetActive (true);
	}

}
