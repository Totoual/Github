using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

	public void OnSceneChange(string sceneName){
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
	}


}
