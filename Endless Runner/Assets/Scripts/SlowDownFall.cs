using UnityEngine;
using System.Collections;

public class SlowDownFall : MonoBehaviour {

	public Vector3 down;	

	// Use this for initialization
	void Start () {
		down = new Vector3 (0, -1f, 0);
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = Vector3.Lerp (this.transform.position, this.transform.position + down, 0.2f);



	}
}
