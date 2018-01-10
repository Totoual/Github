using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {

	public int id;
	public int level;						// To level tou enemy. THa allazei apo to inspector
	public int stamina;						// To stamina tou enemy
	public int strength;					// To strenght tou enemy
	public int maxHealth;					// To max life tou enemy
	public int currentHealth;				// To life pou exei ekeini tin xroniki stigmi stin arxi einai idio me to maxHealth
	public int attackDmg;					// To dmg pou kanei ston paixti
	public bool runnedOnce=true;			// Elegxoume an exei treksi mia fora to elegxoume sto enemyBehavior
	public bool immobilised;				// Tin theloume gia na elegxoume an o exthros einai akinitopoiimenos
	public bool isNotDead;
	private PlayerCalculations playerCalc;	// Pernoume ena instance tou playerCalculation
	private PlayerManager plManager;		// Pernoume ena isntance tou playerManager
	private WorldInteractions playerWorldInteractions;
	private bool playedOnce = false;		// Elegxoume an to animation tou death exei paiksei mia fora
	private QuestProgress questProg;
	private AchievmentGenerator achievManager;
	//private GameObject player;



	/// <summary>
	/// Arxikopoioume tis metavlites mas gia ton exthro 
	/// </summary>
	void Start () {
		achievManager = GameObject.FindGameObjectWithTag ("AchievmentManager").GetComponent<AchievmentGenerator> ();
		questProg = GameObject.FindGameObjectWithTag ("QuestManager").GetComponent<QuestProgress> ();
		playerCalc = GameObject.FindGameObjectWithTag ("PlayerChar").GetComponent<PlayerCalculations> ();
		playerWorldInteractions = GameObject.FindGameObjectWithTag ("Player").GetComponent<WorldInteractions> ();
		plManager = playerCalc.GetComponent<PlayerManager> ();
		stamina = CalculateStamina (level);
		strength = CalcualteStrength (level);
		maxHealth = CalculateMaxHealth (stamina);
		currentHealth = maxHealth;
		attackDmg = CalculateAttackDmg (strength);
		isNotDead = true;
		transform.GetChild (2).gameObject.SetActive (false);
	
	}

#region Ypologismos statistikwn gia ton exthro
	public int CalcualteStrength(int level){
		return level + 2;
	}
	public int CalculateStamina(int level){
		return level + 2;
	}

	public int CalculateMaxHealth(int stamina){
		return stamina * 10; 
	}
	public int CalculateAttackDmg(int strength){
		return strength * 2;
	}
#endregion
	/// <summary>
	/// To attack tou exthrou kathe 1.5 deutera
	/// </summary>
	IEnumerator Attack(GameObject player){

			runnedOnce = false;								// Auto kaleite apo to enemyBehavior wste na energopoioume to collision;
		if(Vector3.Distance(this.transform.position,player.transform.position)<=4f && isNotDead && !immobilised){
			Debug.Log (Mathf.RoundToInt(playerCalc.CalculateArmor(plManager.Agility)*20/100));
			playerCalc.currentHealth -= (attackDmg - Mathf.RoundToInt(playerCalc.CalculateArmor(plManager.Agility)*5/100));			//Orizoume to dmg pou dexetai o paixtis kathe fora pou epitythete to enemy
			playerCalc.rage +=2;
			playerCalc.CalculateDeath ();					// Elegxoume an exei pethanei o paixtis.
			this.GetComponent<Animation> ().Play (animation: "attack");
			//Debug.Log ("attacking Player");
			yield return new WaitForSeconds (0.3f);
			GetComponent<ButtonMusicPlayer> ().PlayEnemyAttackClip ();

		}
		yield return new WaitForSeconds (1.2f);
			StartCoroutine (Attack (player));	 // Ksanakaloume tin coroutine
		

	}


	public void StartTheAttackAgain(GameObject player){
		StartCoroutine (Attack (player));
	}

	public void StopTheAttack(GameObject player){
		StopCoroutine (Attack (player));
	}

	public void Death(){
		if (currentHealth <= 0) {
			achievManager.EarnSlayingAchievment ();
			this.tag = "Lootable";
			this.gameObject.transform.parent.tag = "Lootable";
			isNotDead = false;
			GetComponent<BoxCollider> ().enabled = false;
			this.transform.GetChild (2).gameObject.SetActive (true);
			currentHealth = 0;
			playerWorldInteractions.DrawEnemyHealth (this.GetComponent<EnemyStats>());
			StopCoroutine (Attack (GameObject.FindGameObjectWithTag ("Player")));
			//this.GetComponent<Animation> ().Stop ("attack");
			if (!playedOnce) {
				this.GetComponent<Animation> ().Play (animation: "die");
				GetComponent<NavMeshAgent> ().speed = 0;
				playedOnce = true;
				plManager.XpToLevelUp += playerCalc.CalculateXpToEarn (plManager.Level, level);
				playerCalc.CalculateLevelUp (plManager.XpToLevelUp, plManager.MaxXp);
				questProg.CalculateDropChance ();

			}

		}
	}


}
