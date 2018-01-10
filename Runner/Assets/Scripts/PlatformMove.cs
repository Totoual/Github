using UnityEngine;
using System.Collections;

public class PlatformMove : MonoBehaviour {

	public float speed = -2f;
	private Vector2 temp;
	private GameManager gm;
	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//temp = Vector2.Lerp(this.transform.position,Vector2.right)
		transform.Translate (Vector2.right * (speed * Time.deltaTime));
		//gm.platformMove(this.transform);
	}
}
