using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerBar : MonoBehaviour {
	public GameObject player;
	public List<GameObject> shiticons = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = player.transform.position + new Vector3 (0, -0.6f, 0);
		
	}
}
