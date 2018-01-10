using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	public AudioClip clip;

	// Use this for initialization
	void Start () {
		StartCoroutine(SoundPlayer (5));

	}
	
	// Update is called once per frame
	IEnumerator SoundPlayer(float seconds){
		yield return new WaitForSeconds (seconds);
		GetComponent<AudioSource> ().Play ();
		StartCoroutine (SoundPlayer (5));
	}
}
