using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {
	public static ObjectPooling SharedInstance;
	public List<GameObject> pooledObjects = new List<GameObject> ();
	public List<GameObject> birdsPooled = new List<GameObject> ();
	public List<GameObject> shitPooled = new List<GameObject> ();
	public List<GameObject> humanPooled = new List<GameObject> ();
	public List<GameObject> explosionPooled = new List<GameObject>();
	public List<GameObject> cornPooled = new List<GameObject> ();
	public List<GameObject> copsPooled = new List<GameObject> ();
    //mariza
    public List<GameObject> enemyBirdsPooled = new List<GameObject>();
    public GameObject[] enemyBird;
    public Transform enemyBirdParent;
    public int enemyAmount;
    //end mariza
	public GameObject explosion;
	public Transform explosionParent;
	public int explAmount;
	public GameObject[] humansToPool;
	public Transform humanHolder;
	public int humansToSpawn;
	public GameObject shitToPool;
	public Transform shitHolder;
	public int shitAmmo;
	public GameObject[] birdsToPool;
	public Transform birdsParent;
	public int amoutForBirdsToPool;
	public GameObject[] objectsToPool;
	public int amountToPool;
	public Transform carHolderTransform;
	public GameObject[] cornToPool;
	public int amountOfCorns;

	public GameObject copsFollower;
	public int copsFollowerToPool;
	public Transform CopsPooledHolder;



	// Use this for initialization

	void Awake(){
		SharedInstance = this;
	}

	void Start () {
		CarPooling ();
		BirdsPooling ();
		ShitPooling ();
		HumanPooling ();
		ExplosionPooling ();
        EnemyBirdsPooling();
		CornPooling ();
		CopsFollowersPooling ();

	}



	public void CopsFollowersPooling(){
		for (int i = 0; i < copsFollowerToPool; i++) {
			GameObject obj = (GameObject)Instantiate (copsFollower);
			obj.SetActive (false);
			obj.transform.SetParent (CopsPooledHolder);
			obj.transform.localPosition = Vector2.zero;
			copsPooled.Add (obj);
		}
	}

	public GameObject SearchCops(){
		for (int i = 0; i < copsPooled.Count; i++) {
			if (!copsPooled [i].activeInHierarchy) {
				return copsPooled [i];
			}
		}
		return null;
	}


    public void EnemyBirdsPooling()
    {
        for (int i = 0; i < enemyAmount; i++) {
			float number = Random.Range (0f, 12f);
			GameObject obj = (GameObject)Instantiate(enemyBird[(int)number]);
            obj.SetActive(false);
            obj.transform.SetParent(enemyBirdParent);
            obj.transform.localPosition = Vector2.zero;
            enemyBirdsPooled.Add(obj);
        }

    }

    public GameObject SearchPooledEnemBirds() {
        for (int i = 0; i < enemyBirdsPooled.Count; i++) {
            if (!enemyBirdsPooled[i].activeInHierarchy) {
                return enemyBirdsPooled[i];
            }
        }
        return null;

    }

	public void CornPooling(){
		for (int i = 0; i < amountOfCorns-10; i++) {
			GameObject obj = (GameObject)Instantiate (cornToPool[0]);
			obj.SetActive (false);
			obj.transform.SetParent (enemyBirdParent);
			obj.transform.localPosition = Vector2.zero;
			cornPooled.Add (obj);
		}
		for (int i = 10; i < amountOfCorns; i++) {
			GameObject obj = (GameObject)Instantiate (cornToPool[1]);
			obj.SetActive (false);
			obj.transform.SetParent (enemyBirdParent);
			obj.transform.localPosition = Vector2.zero;
			cornPooled.Add (obj);
		}
	}

	public GameObject SerachPooledCorns(){
		for (int i = 0; i < cornPooled.Count-10; i++) {
			if (!cornPooled [i].activeInHierarchy) {
				return cornPooled [i];
			}
		}
		return null;
	}
	public GameObject SearchForNoCops(){
		for (int i = 10; i < cornPooled.Count; i++) {
			if (!cornPooled [i].activeInHierarchy) {
				return cornPooled [i];
			}
		}
		return null;
	}

	/// <summary>
	/// Dimiourgoume tin lista pou periexei olous tous anthrwpous pou tha paizoun 
	/// katholi tin diarkeia tou game.
	/// </summary>
    /// 
	public void HumanPooling(){
		for (int i = 0; i < humansToSpawn; i++) {
			float number = Random.Range (0f, 7f);
			GameObject obj = (GameObject)Instantiate (humansToPool [(int)number]);
			obj.SetActive (false);
			obj.transform.SetParent (humanHolder);
			obj.transform.localPosition = Vector2.zero;
			humanPooled.Add (obj);
		}
		ShufledList (humanPooled);
	}
	/// <summary>
	/// Dimiourgoume tin klasi me tin opoia tha psaxnoume an yparxei 
	/// anenergo adikeimeno stin lista mas.
	/// </summary>
	/// <returns>The pooled human.</returns>
	public GameObject GetPooledHuman(){
		for (int i = 0; i < humanPooled.Count; i++) {
			if (!humanPooled [i].activeInHierarchy) {
				return humanPooled [i];
			}
		}
		return null;
	}

	/// <summary>
	/// Dimiourgoume tis sfaires pou tha exei o paixtis wste 
	/// na mporei na petyxei perastikous kai alla empodia.
	/// </summary>
	public void ShitPooling(){
		for(int i=0; i < shitAmmo; i++){
			GameObject obj = (GameObject)Instantiate (shitToPool);
			obj.SetActive (false);
			obj.transform.SetParent (shitHolder);
			obj.transform.localPosition = Vector2.zero;
			shitPooled.Add (obj);
		}
	}

	public GameObject GetPooledShit(){
		for (int i = 0; i < shitPooled.Count; i++) {
			if (!shitPooled [i].activeInHierarchy) {
				//shitPooled [i].transform.GetChild (0).gameObject.SetActive (false);
				return shitPooled [i];
			}
		}
		return null;
	}

	/// <summary>
	/// Dimiourgoume tin lista me ta poulia pou tha yparxoun sto background
	/// </summary>
	public void BirdsPooling(){
		for (int i = 0; i < amoutForBirdsToPool; i++) {
			float number = Random.Range (0f, 5f);
			GameObject obj = (GameObject)Instantiate (birdsToPool [(int)number]);
			obj.SetActive (false);
			obj.transform.SetParent (birdsParent);
			obj.transform.localPosition = Vector2.zero;
			birdsPooled.Add (obj);
		}
		ShufledList (birdsPooled);
	}
	/// <summary>
	/// Kanoume Search tin lista gia na doume pio adikeimeno
	/// tis listas einai anenergo gia na boresoume na to xrisimopoiisoume
	/// </summary>
	/// <returns>The pooled bird.</returns>
	public GameObject GetPooledBird(){
		for (int i = 0; i < birdsPooled.Count; i++) {
			if (!birdsPooled [i].activeInHierarchy) {
				return birdsPooled [i];
			}
		}
		return null;
	}



	/// <summary>
	/// Dimourgoume ta autokinita kai ta vazoume mesa se mia lista
	/// gia na mporoume na ta travame.
	/// </summary>
	public void CarPooling ()
	{
		for (int i = 0; i < amountToPool; i++) {
			float randomNumber = Random.Range (0f, 4f);
			GameObject obj = (GameObject)Instantiate (objectsToPool [(int)randomNumber]);
			obj.SetActive (false);
			obj.transform.SetParent (carHolderTransform);
			obj.transform.localPosition = Vector2.zero;
			obj.GetComponent<CarMovement> ().resetPos = carHolderTransform;
			pooledObjects.Add (obj);
		}

		ShufledList (pooledObjects);
		
	}
	/// <summary>
	/// Gets the pooled object.
	/// </summary>
	public GameObject GetPooledObject(){
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy) {
				return pooledObjects [i];
			}
		}
		return null;
	}

	/// <summary>
	/// Dimiourgoume Mia methodo gia na kanei pseutoanakatema stin lista mas 
	/// </summary>
	/// <param name="list">List.</param>
	public void ShufledList(IList list){
		int n = list.Count;
		while (n > 1) {
			int k= Random.Range(0,n)%n;
			n--;
			var value = list [k];
			list [k] = list [n];
			list [n] = value;
		}
	}

	public void ExplosionPooling(){
		for (int i = 0; i < explAmount; i++) {
			GameObject obj = (GameObject)Instantiate (explosion);
			obj.transform.SetParent (explosionParent);
			obj.transform.localPosition = Vector2.zero;
			obj.SetActive (false);
			explosionPooled.Add (obj);
		}
	}

	public GameObject GetPooledExpl(){
		for (int i = 0; i < explosionPooled.Count; i++) {
			if (!explosionPooled [i].activeInHierarchy) {
				return explosionPooled [i];
			}
		}
		return null;
	}

}

		

