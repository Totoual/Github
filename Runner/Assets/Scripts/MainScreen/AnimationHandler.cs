using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour{
	public GameObject holder;
	public GameObject mainMenu;
	public GameObject actualGame;
	public GameObject loseScreen;
	public Text scoreTxt;
	public Text deathScore;
	public int score;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scoreTxt.text = score.ToString ();
	}

	public void ChangeTheHeight(){
		holder.GetComponent<Animator> ().SetBool ("FireInTheHole", true);
		StartCoroutine (DisableTheMainMenu ());
	}

	public IEnumerator DisableTheMainMenu(){
		yield return new WaitForSeconds (1.5f);
		mainMenu.SetActive (false);
		actualGame.SetActive (true);

		Debug.Log ("Test");
	}

	public void Quit(){
		Application.Quit ();
	}

	public void TryAgain(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
	public void Death(){
		loseScreen.SetActive (true);
		actualGame.SetActive (false);
		deathScore.text = score.ToString ();

	}
}
