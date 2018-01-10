using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip [] levelMusicChangeArray;

	private AudioSource audioSource;

	void Awake(){
		DontDestroyOnLoad(gameObject);
		Debug.Log("Don't destroy on load:" + name);
	}

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	void OnLevelWasLoaded(int level){

		AudioClip thislevelMusic = levelMusicChangeArray[level];

		Debug.Log("Playign Clip:" + levelMusicChangeArray[level]);

		if( thislevelMusic){  //if there is some music attached

			audioSource.clip = thislevelMusic;
			GetComponent<AudioSource>().loop = true;
			GetComponent<AudioSource>().Play();
		}
	}

	public void ChangeVolume(float volume){

		audioSource.volume = volume;

	}
}
