using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WorldInteractions : MonoBehaviour {

	public GameObject enemyPortrait;
	public float footStepSpeed;

	NavMeshAgent playerAgent;
	int layer_mask;
	int counter = 0;
	[SerializeField] public List<Transform> enemies = new List<Transform>();
	[SerializeField] public Transform selectedTarget;
	private float dist;
	private bool isMoving = true;
	private bool canPlayAnOtherFootStep =true;


	void Start(){
		playerAgent = GetComponent<NavMeshAgent> ();
		enemyPortrait = GameObject.FindGameObjectWithTag ("EnemyHealth");
		enemyPortrait.SetActive (false);
		layer_mask = LayerMask.GetMask("Interactable");
		playerAgent.enabled = false;
		playerAgent.enabled = true;
		selectedTarget = null;
		AddAllEnemies ();
	}

	public void AddAllEnemies(){
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in go) {
			enemies.Add (enemy.transform);
		}
		for (int i = 0; i < enemies.Count; i++) {
			enemies [i].gameObject.GetComponent<EnemyStats> ().id = i;
		}
	}

	private void TargetEnemy(int id){
		selectedTarget = enemies [id];
		this.transform.LookAt (selectedTarget.position);
	}
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)  && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
			this.GetComponent<Animator> ().SetBool ("CanRun", true);
			this.GetComponent<Animator> ().SetBool ("InCombat", false);
			GetInteraction (); 
		}
		if (isMoving && canPlayAnOtherFootStep) {
			this.GetComponent<ButtonMusicPlayer> ().PlayFootstepsClip ();
			canPlayAnOtherFootStep = false;
			StartCoroutine (CanGenerateAnOtherFootStep (footStepSpeed));
		}
		if (this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("run")) {
			isMoving = true;
		} else {
			isMoving = false;
		}
	



	}

	IEnumerator CanGenerateAnOtherFootStep(float speed){
		yield return new WaitForSeconds (speed);
		canPlayAnOtherFootStep = true;

	}

	void FixedUpdate(){
		
		if (dist != Mathf.Infinity && playerAgent.pathStatus == NavMeshPathStatus.PathComplete && playerAgent.remainingDistance <= 0.1f) {
			this.GetComponent<Animator> ().SetBool ("CanRun", false);
			isMoving = false;
		}
	}

	void GetInteraction(){
		
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo;
		if(Physics.Raycast(interactionRay , out interactionInfo , 150.0f,layer_mask))
		{	dist = playerAgent.remainingDistance;			
			GameObject interactedObject = null;
			interactedObject = interactionInfo.collider.gameObject;
			//Debug.Log (interactedObject.name);
			if (interactedObject.tag == "Intreactable") {
				interactedObject.GetComponent<Interactable> ().MoveToIntreraction (playerAgent);
			} else if (interactedObject.tag == "Lootable") {
				interactedObject.GetComponent<Interactable> ().MoveToIntreraction (playerAgent);
			} else if (interactedObject.tag == "Enemy") {
				TargetEnemy (interactedObject.GetComponent<EnemyStats> ().id);
				enemyPortrait.SetActive (true);
				DrawEnemyHealth (interactedObject.GetComponent<EnemyStats> ());
				interactedObject.GetComponent<Interactable> ().MoveToIntreraction (playerAgent);

			} else if (interactedObject.tag == "Vendor") {
				interactedObject.GetComponent<Interactable> ().MoveToIntreraction (playerAgent);
			} 
			else {
				enemyPortrait.SetActive (false);
				selectedTarget = null;
				playerAgent.stoppingDistance = 0;
				playerAgent.destination = interactionInfo.point;
				//Debug.Log (interactionInfo.point);
				//Debug.Log (Vector3.Distance (playerAgent.transform.position, interactionInfo.point));


			
			}
		}
	}

	public void DrawEnemyHealth(EnemyStats enemyStats){
		enemyPortrait.transform.GetChild (0).transform.GetChild(1).GetComponent<Image> ().rectTransform.localScale = new Vector3 ((float)enemyStats.currentHealth / (float)enemyStats.maxHealth, 
																								enemyPortrait.transform.GetChild (0).GetComponent<Image> ().rectTransform.localScale.y, 
																								enemyPortrait.transform.GetChild (0).GetComponent<Image> ().rectTransform.localScale.z);
		enemyPortrait.transform.GetChild (3).transform.GetChild (0).GetComponent<Text> ().text = enemyStats.level.ToString();
	}
}
