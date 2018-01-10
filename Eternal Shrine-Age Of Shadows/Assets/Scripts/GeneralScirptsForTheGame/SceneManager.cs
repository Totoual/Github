using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
	public float autoLoadNextLevelAfter;
	public bool soundFinished = false;


	// Use this for initialization
	void Start () {
		if (autoLoadNextLevelAfter == 0) {									// Checkaroume an to float gia to posi wra tha kanei gia na fortwsei mono tou to epomeno level einai 0
			Debug.Log ("Level Auto Load Disabled");
		} else {
			
			StartCoroutine(WaitingTimeToLoad(autoLoadNextLevelAfter));				// An den einai fortwnoume to epomeno level simfwna me ta seconds
		}
	
	}

	IEnumerator WaitingTimeToLoad(float sec){
		yield return new WaitForSeconds (sec);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}

	public void RestartLevel(string levelName){ 
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(levelName,LoadSceneMode.Single);
	}

	public void LoadLevel(){										// Methodos gia na fortwsoume to epomeno level me vasi to onoma tou
		UnityEngine.SceneManagement.SceneManager.LoadScene("GUI",LoadSceneMode.Single);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Level01",LoadSceneMode.Additive);


	}
	public void QuitRequest(){												// Methdoso gia to quit
		Application.Quit ();
	}
	public void LoadNextLevel(){											// Methodos gia to load next level
		int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		UnityEngine.SceneManagement.SceneManager.LoadScene (currentScene + 1);
	}

	public void LoadStartLevel(){											// Methodos gia to load start level
		if (soundFinished) {
			int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;
			UnityEngine.SceneManagement.SceneManager.LoadScene (currentScene + 1);
		}
	}
}

