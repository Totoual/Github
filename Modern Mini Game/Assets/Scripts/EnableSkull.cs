using UnityEngine;
using System.Collections;

public class EnableSkull : MonoBehaviour {
	MeshRenderer mesh;
	Rigidbody rgbody;
	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshRenderer> ();						// Pernoume ton MeshRender tou gameObject
		rgbody = GetComponent<Rigidbody> ();						// Pernoume to Rigidbody you gameObject
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.SkullReseted) {						// Elegxoume an exei ginei reset to skull mesw tou game manager
			this.mesh.enabled = true;									// Energaopoioume to meshRenderer
			this.transform.GetChild (0).gameObject.SetActive (true);	// Energopoioume to particle;


		}

	}
}
