using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float force = 100f;
    [SerializeField] private float speed = 4;
	//public AudioClip flapSound;

    private Rigidbody2D rigidbd;
    private bool jump = false;
    private bool isInPosition = false;
    private bool canMoveInPosition = false;
    private bool runnedOnce = false;
   // private bool tutorial = false;

	public float timer;
    //public BackgroundMovement bgMov;

	public int counter;

    public GameObject startPanel;
	public GameObject ShitCounter;
   

    // Use this for initialization
    void Start()
    {
        rigidbd = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Elegxoyme an o paixtis exei ksekinisei to paixnidi kai an den einai nekros
    /// stin synexeia elegxoume an kanei tap stin othoni kai den pataei kapoio apo ta koubia
    /// pou yparxoun stin kedriki skini. An patisei stin othoni
    /// tote enimerwnoume ton game manager oti o paixtis ksekinise to game tou dilwnoume 
    /// oti borei na ksekinisei na proxwraei to peristeri stin thesi pou tou exoume orisei. Elegxoume an exei ksekinisei na pigainei stin thesi 
    /// an ftasei kai afou exei treksei o prwtos elegxos tote tou epitrepoume na kanei jump kai energopoioume tin fysiki gia to gameobject 
    /// sygekrimena tin varitita.
    /// </summary>
    void Update()
	{
//        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
//        {
//			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
//                || !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
//            {
//
//                this.GetComponent<Animator>().SetBool("Flap", true);
//                GameManager.instance.PlayerStartedGame();
//                canMoveInPosition = true;
//                if (runnedOnce)
//                {
//                    jump = true;
//                }
//            }
//        }
			
// THIS IS THE RIGHT CODE IN ORDER TO WORK PROPERLY IN ANDROID SYSTEM
		if (!GameManager.instance.GameOver && GameManager.instance.GameStarted) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began || Input.GetMouseButtonDown (0)) {
				//if(!EventSystem.current.IsPointerOverObject(Input.GetTouch(0).fingerId) ||!EventSystem.current.IsPointerOverGameObject() ){
				if (!IsPointerOverUIObject ()) {       //////////mariza
					this.GetComponent<Animator> ().SetBool ("Flap", true);
					GameManager.instance.PlayerStartedGame ();
					if (GameManager.instance.canContinueAfterDeath) {
						GameManager.instance.canContinueAfterDeath = false;
						GameManager.instance.MafiaPigeonAfterDeath.SetActive (false);
						Time.timeScale = 1;
					}
					canMoveInPosition = true;
					if (runnedOnce) {
						jump = true;
					}
				}
			}
		}
// END OF THE CORRECT CODE IN ORDER TO WORK IN ANDROID SYSTEM

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetMouseButtonUp (0)) {
			if (!IsPointerOverUIObject ()) {        /////////mariza
				this.GetComponent<Animator> ().SetBool ("Flap", false);
			}
		}

		if (!isInPosition && canMoveInPosition && transform.position.x < -2.88f) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		} else if (transform.position.x >= -2.88f && !runnedOnce) {
			if (!GameManager.instance.Tutorial) {
				GameManager.instance.StartTutorial ();
				Time.timeScale = 0.1f;
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetMouseButtonDown (0)) {
					if (!IsPointerOverUIObject ()) {        //////////mariza
						Time.timeScale = 1f;
						GameManager.instance.EndOfTutorial ();                
					}
				}
			} else {
				runnedOnce = true;
				isInPosition = true;
				rigidbd.simulated = true;
			}
		}
		//This is the relocation part. We check if the position is less that the original position
		//and if it is runned the update at least once.
		//then we count seconds if timer is more that 10 then we start moving right until we reach the original pos.
		if (!GameManager.instance.GameOver && GameManager.instance.GameStarted) {
			if (transform.position.x < -2.8f && runnedOnce) {
				timer += Time.deltaTime;
				if (timer >= 10) {
					//Debug.Log ("I moving to the original Pos");
					transform.Translate (Vector2.right * Time.deltaTime);
				}
			}
			if (transform.position.x >= -2.8f) {
				timer = 0;
			}

			//Here is the off screen part that we kill the player
			if (transform.position.x <= -12.5) {
				GameManager.instance.Death ();
			}
		}

		if (counter == 0) {
			for (int i = 0; i < ShitCounter.GetComponent<FollowPlayerBar> ().shiticons.Count; i++) {
				ShitCounter.GetComponent<FollowPlayerBar> ().shiticons [i].SetActive (false);
			}
		}
		if (counter == 1) {
			ShitCounter.GetComponent<FollowPlayerBar> ().shiticons [counter-1].SetActive (true);
		} else if (counter == 2) {
			ShitCounter.GetComponent<FollowPlayerBar> ().shiticons [counter-1].SetActive (true);
		} 


    }

	public bool IsPointerOverUIObject(){
		PointerEventData eventDataCurrentPosition = new PointerEventData (EventSystem.current);
		#if UNITY_EDITOR
		eventDataCurrentPosition.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);   //**Uncomment This For Editor**//
      	#else
		 eventDataCurrentPosition.position = Input.GetTouch(0).position;       				          //**Uncomment This For Mobile**//
		#endif
		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (eventDataCurrentPosition, results);
		return results.Count > 0;
	}

    // Update is called once per frame
    /// <summary>
    /// Edw vazoume tin dynami me tin opoia 
    /// o paixtis kanei bounce ston aera kathe fora pou kanei tap stin othoni
    /// </summary>
    void FixedUpdate()
    {        
        if (jump && !GameManager.instance.stunned)
        {
            jump = false;
            counter++;
            rigidbd.velocity = new Vector2(0, 0);
			if (counter == 0) {
				for (int i = 0; i < ShitCounter.GetComponent<FollowPlayerBar> ().shiticons.Count; i++) {
					ShitCounter.GetComponent<FollowPlayerBar> ().shiticons [i].SetActive (false);
				}
			}
			if (counter == 1) {
				ShitCounter.GetComponent<FollowPlayerBar> ().shiticons [counter - 1].SetActive (true);
			} else if (counter == 2) {
				ShitCounter.GetComponent<FollowPlayerBar> ().shiticons [counter - 1].SetActive (true);
			}
			else if (counter == 3)
            {	
				ShitCounter.GetComponent<FollowPlayerBar> ().shiticons [counter - 1].SetActive (true);
				if (GameManager.instance.vibrate) {
					Handheld.Vibrate ();
				}
                counter = 0;
                GameObject shit = ObjectPooling.SharedInstance.GetPooledShit();
                shit.SetActive(true);
                shit.transform.SetParent(shit.transform.parent.parent);
                force += 10;
				StartCoroutine (ReturnTheForceToNormal ());
            }
			rigidbd.AddForce(new Vector2(0, force), ForceMode2D.Impulse);


        }
        if (rigidbd.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            float angle = Mathf.Lerp(0, -20, (-rigidbd.velocity.y / 8f));
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }


		if (!GameManager.instance.GameOver && GameManager.instance.PlayerActive && isInPosition) {
			//Debug.Log (Mathf.Round (2 * Time.time));
			GameManager.instance.AddToMeters (Mathf.Round (GameManager.instance.environmentSpeed * Time.timeSinceLevelLoad));
			//Debug.Log(Mathf.Round(Time.timeSinceLevelLoad));
		}



    }

	public  IEnumerator ReturnTheForceToNormal(){
		yield return new WaitForSeconds (0.3f);
		force = 25;
	}

}
