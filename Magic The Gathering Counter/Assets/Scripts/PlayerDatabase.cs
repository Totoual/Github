using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerDatabase : MonoBehaviour {
	public string path;

	void Awake(){
		path = Application.persistentDataPath + "/playerDb.json";
		if (!System.IO.File.Exists (path)) {
			Debug.Log ("The file does not exist");
			System.IO.File.Create (path);
			Debug.Log ("I created the file");
		} else {
			Debug.Log (path);
			Debug.Log ("the File exists");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
