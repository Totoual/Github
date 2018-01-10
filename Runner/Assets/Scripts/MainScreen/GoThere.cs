using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoThere : MonoBehaviour {
	public float speed;
	public Text text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Mathf.Abs (this.transform.position.x-(-3.36f)));
		if (Mathf.Abs (this.transform.position.x-(-3.36f)) >= 0.2f) {
			Debug.Log ("I am fucking walking");
			transform.Translate (Vector2.right * (speed * Time.deltaTime));

		}

		if (Mathf.Abs (this.transform.position.x - (-3.36f)) <= 0.2f) {
			text.text = "ok now you can go";
			GetComponent<Animator> ().SetBool ("GoIdle", true);
		}
	}
}
