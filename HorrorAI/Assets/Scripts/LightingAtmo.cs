using UnityEngine;
using System.Collections;

public class LightingAtmo : MonoBehaviour {

	bool temp = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (EnableLight (temp));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator EnableLight(bool temp){
		yield return new WaitForSeconds (Random.Range (0.1f, 3f));
		this.transform.GetChild (1).gameObject.SetActive (temp);
		temp = !temp;
		StartCoroutine (EnableLight (temp));
	}
}
