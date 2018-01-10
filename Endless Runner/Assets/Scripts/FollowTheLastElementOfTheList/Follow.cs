using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	/// <summary>
	/// To adikeimeno to opoio exei auto to script panw tou 
	/// akolouthei pada to z position tou teleutaiou adikeimenou stin lista
	/// wste na borw na kserw to z sto opoio tha kanw instantiate xwris na xriazetai
	/// kathe fora na kanw calculate to megethos tis gefyras. kai dedomenou oti to kathe kommati tis gefyras 
	/// exei diaforetiko megethos o ypologismos tou mikous tis gefyras ginotan diskolos. 
	/// </summary>

	public GeneratorManager genManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (0, 0, genManager.InGameObjects [genManager.InGameObjects.Count - 1].transform.GetChild (1).transform.position.z);
	}
}
