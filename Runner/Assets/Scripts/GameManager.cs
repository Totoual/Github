using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public List<GameObject> inGameObject = new List<GameObject> ();
	public GameObject[] parts;
	public GameObject tileHolder;
	public GameObject lastObject;
	private int generateNumber;
	private int previousGeneratedNumber;
	private int counter;
	private bool firstRun= true;
	public float width = 0;


	public static int SPEED = 2;

	// Use this for initialization
	void Start () {
		for (int i = 0; i <= 10; i++) {
			ConstuctTiles ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		lastObject.transform.position = new Vector2 ( inGameObject [inGameObject.Count - 1].transform.position.x + 
			inGameObject [inGameObject.Count - 1].GetComponent<Collider2D>().bounds.size.x/2 , inGameObject [inGameObject.Count - 1].transform.position.y);
	}


	public void ConstuctTiles(){
		if (firstRun) {
			firstRun = false;
			GameObject temp = (GameObject)Instantiate (parts [0]);
			temp.transform.SetParent (tileHolder.transform);
			temp.transform.localScale = new Vector2 (1, 1);
			temp.transform.localPosition = Vector2.zero;
			width += temp.GetComponent<Collider2D> ().bounds.size.x;
			inGameObject.Add (temp);
		} else {
			GameObject temp = (GameObject)Instantiate (parts [RandomGenerator()]);
			temp.transform.SetParent (tileHolder.transform);
			temp.transform.localScale = new Vector2 (1, 1);
			temp.transform.localPosition = new Vector2 (width, 0);
			width += temp.GetComponent<Collider2D> ().bounds.size.x;
			inGameObject.Add (temp);
		}
	}

	public void  InstantiateLastItem(){
		GameObject temp = (GameObject)Instantiate (parts [RandomGenerator()]);
		temp.transform.SetParent (tileHolder.transform);
		temp.transform.localScale = new Vector2 (1, 1);
		temp.transform.position = new Vector2 (lastObject.transform.position.x + temp.GetComponent<Collider2D>().bounds.size.x/2, lastObject.transform.position.y);
		inGameObject.Add (temp);
	}

	public int RandomGenerator(){
		generateNumber = Mathf.RoundToInt(Random.Range (0, parts.Length));
		if (generateNumber != previousGeneratedNumber && counter < 3) {
			previousGeneratedNumber = generateNumber;
			counter++;
			return generateNumber;
		} else {
			counter = 0;
			return RandomGenerator ();
		}
	}


	public void platformMove(Transform transf){
		transf.Translate (Vector2.right * (SPEED * Time.deltaTime));
	}
}
