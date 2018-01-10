using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVId : MonoBehaviour {
	public MovieTexture movie;

	void Awake(){
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		movie.Play ();
		movie.loop = true;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
