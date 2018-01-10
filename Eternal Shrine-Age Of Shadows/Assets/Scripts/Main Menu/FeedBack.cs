using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FeedBack : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler {
	AudioSource audioSource;
	Animator anim;
	public AudioClip clip;
	public SceneManager sceneManager;
	public AudioClip clickClip;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerExit(PointerEventData data){
		if(anim !=null)
		anim.SetBool ("OnHover", false);
	}

	public void OnPointerEnter(PointerEventData data){
		if (anim != null) {
			anim.SetBool ("OnHover", true);
			audioSource.clip = clip;
			audioSource.Play ();
		}
	}

	public void OnPointerDown(PointerEventData data){
		audioSource.clip = clickClip;
		audioSource.Play();
		//Debug.Log ("The clip length is " + audioSource.clip.length);
		//Debug.Log ("The time we are currently is ");
		//Debug.Log (audioSource.clip.length - audioSource.time);
		//Debug.Log ("SoundFinished");
		StartCoroutine(WaitforSomeTime(audioSource.clip.length));
		}

	IEnumerator WaitforSomeTime(float time){
		yield return new WaitForSeconds (1.2f);
		Debug.Log ("It is true");
		sceneManager.soundFinished = true;
		sceneManager.LoadStartLevel ();
	}

}
