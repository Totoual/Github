using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour {
	Transform temp;
	public float speed = 2f;				// I taxytita me tin opoia tha kinite.
	public GameManager gmManager;
	// Use this for initialization
	void Awake () {
		gmManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gmManager.startGame && gmManager.notDead) {
			transform.Translate (Vector3.forward * (speed * Time.deltaTime));			// Orizoume tin taxytita me tin opoia tha kinite i platforma sto paixnidi
		}
	}
}
