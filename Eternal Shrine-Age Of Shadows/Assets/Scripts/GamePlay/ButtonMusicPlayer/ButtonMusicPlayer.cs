using UnityEngine;
using System.Collections;

public class ButtonMusicPlayer : MonoBehaviour {

	public AudioClip[] 	openClip;
	public AudioClip[] 	closeClip;
	public AudioClip 	potionClip;
	public AudioClip[]	footSteps;
	public AudioClip[]	player_Attack;
	public AudioClip 	chargeClip;
	public AudioClip 	cleaveClip;
	public AudioClip 	howlClip;
	public AudioClip 	dragonSwipeClip;
	public AudioClip[]	enemy_Attack;
	public AudioClip 	levelUpClip;
	public AudioClip[] 	vendorFarewell;
	public AudioClip[] 	vendorGreeting;
	public AudioClip[] 	generalFarewell;
	public AudioClip[] 	generalGreeting;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayOpenClip(int id){
		GetComponent<AudioSource> ().clip = openClip [id]; 
		GetComponent<AudioSource> ().Play ();
	}
	public void PlayCloseClip(int id){
		GetComponent<AudioSource> ().clip = closeClip [id]; 
		GetComponent<AudioSource> ().Play ();
	}
	public void PlayPotionClip(){
		GetComponent<AudioSource> ().clip = potionClip;
		GetComponent<AudioSource> ().Play ();
	}

	public void PlayQuestAcceptClip(){
		GetComponent<AudioSource> ().Play ();
	}

	public void PlayQuestCompleteClip(){
		GetComponent<AudioSource> ().Play ();
	}
	#region PlayerClips
	public void PlayFootstepsClip(){
		int temp = Random.Range (0, footSteps.Length);
		GetComponent<AudioSource> ().clip = footSteps [temp];
		GetComponent<AudioSource> ().Play ();
	}
	public void PlayPlayerAttackClip(){
		int temp = Random.Range (0, player_Attack.Length);
		GetComponent<AudioSource> ().clip = player_Attack[temp];
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
	public void PlayPlayerChargeClip(){
		GetComponent<AudioSource> ().clip = chargeClip;
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
	public void PlayPlayerCleaveClip(){
		GetComponent<AudioSource> ().clip = cleaveClip;
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
	public void PlayPlayerHowlClip(){
		GetComponent<AudioSource> ().clip = howlClip;
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
	public void PlayPlayerDragonSwipeClip(){
		GetComponent<AudioSource> ().clip = dragonSwipeClip;
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
	#endregion
	public void PlayEnemyAttackClip(){
		int temp = Random.Range (0, enemy_Attack.Length);
		GetComponent<AudioSource> ().clip = enemy_Attack[temp];
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}

	public void PlayLevelUpClip(){
		GetComponent<AudioSource> ().clip = levelUpClip;
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}

	public void PlayVendorGreetingClip(){
		int temp = Random.Range (0,vendorGreeting.Length);
		GetComponent<AudioSource> ().clip = vendorGreeting[temp];
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
	public void PlayVendorFarewellClip(){
		int temp = Random.Range (0,vendorFarewell.Length);
		GetComponent<AudioSource> ().clip = vendorFarewell[temp];
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}

	public void PlayGeneraGreetingClip(){
		int temp = Random.Range (0,generalGreeting.Length);
		GetComponent<AudioSource> ().clip = generalGreeting[temp];
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
	public void PlayGeneralFarewellClip(){
		int temp = Random.Range (0,generalFarewell.Length);
		GetComponent<AudioSource> ().clip = generalFarewell[temp];
		GetComponent<AudioSource> ().PlayOneShot(GetComponent<AudioSource> ().clip);
	}
}
