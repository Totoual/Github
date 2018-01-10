using UnityEngine;
using System.Collections;

public class EnvironmentMovement : MonoBehaviour {
	public int speed = -2;
	[SerializeField] private float resetPosition;
	[SerializeField] private float startPosition;
	public GameManager gmManager;
	// Use this for initialization
	void Awake () {
		gmManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	/// <summary>
	/// Einai i kinisi tou perivalodos
	/// se periptwsi pou to paixnidi exei ksekinisi kai o paixtis den einai nekros
	/// </summary>
	void Update () {
		if (gmManager.startGame && gmManager.notDead) {
			transform.Translate (Vector3.forward * (speed * Time.deltaTime));
			if (transform.localPosition.z <= resetPosition) {
				Vector3 newPos = new Vector3 (transform.position.x, transform.position.y, startPosition);
				transform.position = newPos;
			}
		}
	}
}
