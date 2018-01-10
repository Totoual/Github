using UnityEngine;
using System.Collections;

public class PlayerCalculations : MonoBehaviour {
	public GameObject musicPlayer;
	public GameObject weaponSlot;
	public GameObject[] armorSlots;
	private GameObject player;
	private int calculateDif = 0;
	private float calculateMorDif = 0f;
	private int mobXp = 45;
	private bool diedOnce = false;
	public int currentHealth;
	public int rage;
	public int maxRage;
	public int damage;
	public bool canAttack= true;
	float temp = 0;

	public static bool dead = false;


	public PlayerManager playerMan;
	public UIHandler uiHandle;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		rage = 0;
		currentHealth = CalculateHealth (playerMan.Stamina);
		maxRage = 100;
		//PlayerAttack ();
	}
	/// <summary>
	/// Gia na boresoume na doulepsoume tin formoula pou exoume paradwsei prepei na ypologizoume kathe fora to xp pou
	/// xriazete o paixtis gia na kanei lvl up. To sygekrimeno einai to max xp pou xriazete gia lvl up. Se auto to 
	/// function kaloume kai alla ta opoia voithane sto ypologismo tis formoulas
	/// </summary>
	/// <returns>The X.</returns>
	/// <param name="level">Level.</param>
	public int CalculateXP(int level){
		int calcdif = CalculateDifficulty (level);
		float calcmorDif = CalculateMoreDifficulty(level);
		int calcMob = CalculateMobXp (level);
		int calculatexp = (int) Mathf.Round( ((8 * level) + calcdif) * calcMob * calcmorDif);
		return calculatexp;

	}
	/// <summary>
	/// Ypologizei tin diskolia pou exei to kathe level etsi wste na boresoume na 
	/// anevasoume ta level gramika.
	/// </summary>
	/// <returns>The difficulty.</returns>
	/// <param name="level">Level.</param>
	public int CalculateDifficulty(int level){
		if(level <= 28){
			return calculateDif= 0;
		} else if (level == 29) {
			return calculateDif = 1;
		} else if (level == 30) {
			return calculateDif = 3;
		} else if (level == 31) {
			return calculateDif = 6;
		} else if (level == 32) {
			return calculateDif= 10;
		} else if (level > 32) {
			calculateDif += 5;
			return calculateDif;
		} else {
			return calculateDif = 0;
		}

	}
	/// <summary>
	/// Vazoume mia parapanw diskolia. Xrisimopoiite apo tin Calculate xp
	/// </summary>
	/// <returns>The more difficulty.</returns>
	/// <param name="level">Level.</param>
	public float CalculateMoreDifficulty(int level){
		if (level > 0 && level <= 10) {
			return calculateMorDif = 1.0f;
		} else if (level >= 11 && level <= 28) {
			calculateMorDif -= 0.01f;
			return calculateMorDif;
		} else {
			return calculateMorDif = 0.82f;
		}
	}
	/// <summary>
	/// Ypologizoume kata meso oro poso xp dinei to kathe mob
	/// </summary>
	/// <returns>The mob xp.</returns>
	/// <param name="level">Level.</param>
	public int CalculateMobXp(int level){
		mobXp += 5;
		return mobXp;
	}
	/// <summary>
	/// Ypologismos tou attackPower
	/// Den poly xriazete gia to character selection isws to kalw meta 
	/// sto game gia na ypologizw stats
	/// </summary>
	/// <returns>The attack power.</returns>
	/// <param name="strength">Strength.</param>
	public int CalculateAttackPower(int strength){
		return strength * 2;
	}
	/// <summary>
	/// Ypologismos tou armor
	/// Den poly xriazete gia to character selection isws to kalw meta 
	/// sto game gia na ypologizw stats
	/// </summary>
	/// <returns>The armor.</returns>
	/// <param name="agility">Agility.</param>
	public int CalculateArmor(int agility){
		int armor = agility * 2;
		for (int i = 0; i < armorSlots.Length; i++) {
			if (armorSlots [i].transform.childCount > 2) {
				armor += armorSlots [i].transform.GetChild (2).transform.gameObject.GetComponent<ItemData> ().item.armor;
			}
		}
		return armor;
	}
	public int CalculateHealth(int stamina,int headStamin =0, int shouldersStamina =0, int chestStamina =0, int pantsStamina = 0){
		int temp = stamina + shouldersStamina + chestStamina + pantsStamina ;
		return temp * 10;
	}
	/// <summary>
	/// Ypologizoume to rage.
	/// An o paixtis einai offcombat tote xanei rage 
	/// me vasi ton xrono pou pernaei. Prepei na bei functionality gia na prostithete 
	/// </summary>
	/// <returns>The rage.</returns>
	public int CalcualteRage(){
		
		temp += Time.deltaTime*0.8f;
		if (1-temp <= 0.05f) {
			temp = 0;
			rage -= 2;
		}

		if (rage <= 0) {
			rage = 0;
		}
		if (rage >= maxRage) {
			rage = maxRage;
		}
		//Debug.Log (temp);
		return rage;
	}

	public int CalculateXpToEarn(int charLevel,int mobLevel){
		if (charLevel < mobLevel) {
			if (mobLevel - charLevel > 4) {
				mobLevel = charLevel + 4;
			}
			float xpTogive = (charLevel * 5 + 45) * (1 + 0.05f*(mobLevel - charLevel));
			return (int)xpTogive;
		} else if (charLevel == mobLevel) {
			return (charLevel * 5 + 45);
		} else if (charLevel > mobLevel) {
			if (charLevel - mobLevel > 4) {
				return 0;
			}
			return (charLevel * 5 + 15);
		} else {
			return 0;
		}
	}

	public void CalculateLevelUp(int currentXp,int maxXp){
		if (currentXp == maxXp) {
			playerMan.XpToLevelUp = 0;
			playerMan.Level += 1;
			playerMan.MaxXp = CalculateXP (playerMan.Level);
			AddStats ();
			uiHandle.PrintAttributes ();
			currentHealth = CalculateHealth (playerMan.Stamina);
			musicPlayer.GetComponent<ButtonMusicPlayer> ().PlayLevelUpClip ();
		} else if (currentXp > maxXp) {
			int temp = currentXp - maxXp;
			playerMan.XpToLevelUp = 0;
//			Debug.Log (currentXp);
			//Debug.Log (temp);
			playerMan.XpToLevelUp = temp;
			playerMan.Level += 1;
			playerMan.MaxXp =CalculateXP (playerMan.Level);
			AddStats ();
			uiHandle.PrintAttributes ();
			currentHealth = CalculateHealth (playerMan.Stamina);
			musicPlayer.GetComponent<ButtonMusicPlayer> ().PlayLevelUpClip ();
		}
	}

	public void CalculateDeath(){
		if (currentHealth <= 0) {
			Debug.Log ("You are dead");
			if (!diedOnce) {
				uiHandle.OnDeath ();
				diedOnce = true;
				player.GetComponent<Animator> ().SetTrigger ("Death");
			}
			currentHealth = 0;
			dead = true;
		}

	}

	public void AddStats(){
		playerMan.Strength += 3;
		playerMan.Stamina += 2;
		playerMan.Agility += 1;
		playerMan.Spirit += 1;
		playerMan.Intellect += 1;

	}
	public void GenerateRage(){
		if (rage >= maxRage) {
			rage = maxRage;
		} else {
			rage += (int)( 2 * PlayerAttack() * (4 * playerMan.Level) + 3.2f / 2);
			//Debug.Log (rage);
		}
	}
#region Skill Methods
	public int PlayerAttack(){
		
		if (canAttack) {
			player.GetComponent<Animator> ().SetTrigger("CanAttack");

			if (weaponSlot.transform.childCount > 2) {
				damage = Mathf.RoundToInt (((weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.min_Damage + weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.max_Damage / 2)
				+ CalculateAttackPower (playerMan.Strength) / 5) / weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.speed);
			} else {
				damage = 2;				// To default dmg pou kanei an den foraei kapoio oplo
			}
			canAttack = false;

			//Debug.Log (" Damage" + damage + " AttackPower " + CalculateAttackPower (playerMan.Strength) + " AttackPower /5 " + CalculateAttackPower (playerMan.Strength) / 5);
			StartCoroutine (AutoAttackCD (weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.speed));
			return damage;
		} else {
			//Debug.Log ("I cannot attack");
			return 0;
		}

	}

	public IEnumerator AutoAttackCD(float speed){
		yield return new WaitForSeconds (0.3f);
		player.GetComponent<ButtonMusicPlayer> ().PlayPlayerAttackClip ();
		yield return new WaitForSeconds (speed -0.3f);
		canAttack = true;
	}

	public int CleaveDmg(){
		damage = Mathf.RoundToInt (((weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.min_Damage + weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.max_Damage / 2)
			+ CalculateAttackPower (playerMan.Strength) / 5) / weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.speed) + 15;
		return damage;
	}

	public int DragonSwipeDmg(){
		damage = Mathf.RoundToInt ((((weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.min_Damage + weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.max_Damage / 2)
			+ CalculateAttackPower (playerMan.Strength) / 5) / weaponSlot.transform.GetChild (2).GetComponent<ItemData> ().item.speed) * 1.2f) + 10;
		return damage;
	}

#endregion

}
