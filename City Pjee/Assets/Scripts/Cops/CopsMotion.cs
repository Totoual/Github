using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopsMotion : MonoBehaviour {

    // Update is called once per frame
	void Update () {
		if (tag == "killerBird") {
			transform.Translate (Vector2.left * (GameManager.instance.eBirdsSpeed * Time.deltaTime));
		} else {
			transform.Translate (Vector2.left * (GameManager.instance.copsSpeed* Time.deltaTime));
		}

	}
}
