using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private int touched;


	public int Touched(){
		return touched;
	}


	public void IsTouched(){
		touched++;
	}

	public void Restart(){
		SceneManager.LoadScene (0);
	}
	public void Quit(){
		Application.Quit ();
	}
}
