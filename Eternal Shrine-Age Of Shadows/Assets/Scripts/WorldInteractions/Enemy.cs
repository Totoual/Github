using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Enemy : Interactable {

	public Image enemyHealth;
	//public GameObject enemyPortrait;
	public GameObject enemyPos;
	public PlayerCalculations playerCalc;
	public PlayerManager plManager;

	public GameObject player;


	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerCalc = GameObject.FindGameObjectWithTag ("PlayerChar").GetComponent<PlayerCalculations> ();
		plManager = playerCalc.GetComponent<PlayerManager> ();

	}


	public override void Interact(){
		Debug.Log ("Interacting with Enemy");
		if (GetComponent<EnemyStats> ().isNotDead) {
			PlayerAttack ();
			player.GetComponent<Animator> ().SetBool ("InCombat", true); 	//Orizoume oti o xaraktiras einai se combat;
			GetComponent<EnemyStats> ().Death ();
		}
	}

	public void PlayerAttack(){
		player.GetComponent<WorldInteractions> ().DrawEnemyHealth (this.GetComponent<EnemyStats> ());
		GetComponent<EnemyStats> ().currentHealth -= playerCalc.PlayerAttack();
		playerCalc.GenerateRage ();
	}

}
