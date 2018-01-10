using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	void Start(){
		if (autoLoadNextLevelAfter == 0 ){
			Debug.Log("Level Auto load Disabled");
		}else{
		Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}


	public void LoadLevel(string name){
		//Debug.Log ("Level load requested for : " + name);
		Application.LoadLevel(name);
	
	}
	public void QuitRequest(){
		//Debug.Log ("The Game is quited: " + name);
		Application.Quit();
	}
	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel+1);
	}

}
