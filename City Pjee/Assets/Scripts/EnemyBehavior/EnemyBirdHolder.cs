using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdHolder : MonoBehaviour {

    // Use this for initialization
	void Start () {
        StartCoroutine(enemyBirdsSpawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator enemyBirdsSpawn() {
        if (!GameManager.instance.GameOver) {
            GameObject bird = ObjectPooling.SharedInstance.SearchPooledEnemBirds();
            bird.transform.position = new Vector3(bird.transform.position.x, Random.Range(3.32f, -3f), bird.transform.position.z);
            bird.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(enemyBirdsSpawn());
    }
}

