using UnityEngine;
using System.Collections;

public class TrailWeaponFollower : MonoBehaviour {
	public GameObject weapon;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate( weapon.transform.position);
	}
}
