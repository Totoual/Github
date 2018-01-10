using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievmentManager : MonoBehaviour {

	//Public Variables
	public GameObject achievmentPrefab; // To prefab tou achiev pou tha kanoume instantiate;
	public ScrollRect scrollRect;				// Prepei na allazoume ton slider
	public GameObject earnedAchievment;			// To achiev pou tha dixnoume stin othoni mas
	public Dictionary<string , Achievment> achievments = new Dictionary<string, Achievment>();		// DImiourgoume ena dictioneri me ta achievment pou exoume
	public int coutner;
	public Sprite unlockedSprite;
	public Text textPoints;

	// Private Variables
	private AchievmentButton activeButton;		// Theloume na kseroume poio koubi einai active
	private static AchievmentManager instance;		// Dimiourgoume to singleton
	private float fadeTime = 2f;					// Xronos gia fade out

	#region Getter
	public static AchievmentManager Instance{			// Orizoume ton getter wste na boroume na exoume access apo kapoio allo script

		get{
			if(instance == null){						// Elegxoume an yparxei 
				instance = GameObject.FindObjectOfType<AchievmentManager> (); 		// An den yparxei vriskoume to adikeimeno autou tou typou
			}
			return AchievmentManager.instance;}
	}
	#endregion

	// Use this for initialization
	void Start () {
		activeButton = GameObject.Find ("Summary_Button").GetComponent<AchievmentButton> ();
		activeButton.Click ();
//		CreateAchievment ("General","Kill all","Kill everything on sight",50);
//		CreateAchievment ("General","Kill all2","Kill everything on sight",60,new string[]{"Kill All"} );



		foreach (GameObject achievmentList in GameObject.FindGameObjectsWithTag("AchievmentList")) {			// Apenergopoioume oles ti katigories pou den theloume afou exoume dimiourgisei ta achievment
			achievmentList.SetActive (false);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (coutner == 10) {
			EarnAchievment ("Kill all");
		}
		if (coutner == 20) {
			EarnAchievment ("Kill all2");
		}

	
	}

	public void EarnAchievment(string title){
		if (achievments [title].EarnAchievment ()) {
			// do something
			GameObject achievment = (GameObject) Instantiate(earnedAchievment);		// Emfanizoume stin othoni to achievment pou kerdise o paixtis;
			SetAchievmentInfo("EarnCanvas",achievment,title);
			StartCoroutine(FadeAchievment(achievment));
		}
	}

	public IEnumerator HideAchievment(GameObject achievment){
		yield return new WaitForSeconds (3);
		Destroy (achievment);
	}

	public void CreateAchievment(string parent , string title,string description, int points, string[] dependencies = null){
		GameObject achievment = (GameObject) Instantiate (achievmentPrefab);	// Kanoume instantiate to achievment
		Achievment newAchievment = new Achievment(title,description,points,achievment,title);
		achievments.Add(title,newAchievment);																// Vaozume to achievment sto dictionery;
		SetAchievmentInfo(parent,achievment,title);   														// Kaloume tin methodo gia na thesoume ta infos tou achievment

		if (dependencies != null) {
			foreach (string achievmentTitle in dependencies) {												// Psaxnoume ston pinaka me ta string
				Achievment dependency = achievments [achievmentTitle];										// Dimiourgoume ena achievment me vasi auto poy yparxei ston pinaka.
				dependency.Child = title;																	// Orizoume to child ston title tou achiev;
				newAchievment.AddDependency(dependency);

				//Dependency = Press Space <-- Child = Press W			Dimiourgoume mia sxesi parent kai child
				// NewAchievemnt = Press W --> Press Space
			}
		}
	}

	public void SetAchievmentInfo(string parent, GameObject achievment, string title){

		achievment.transform.SetParent (GameObject.Find (parent).transform);			// Psaxnoume na vroume to gameObject me to onoma tis katigorias pou exoume dwsei
		achievment.transform.localScale = new Vector3(1,1,1); 							// Allazoume to scale tou prefab gia na fenetai swsta
		achievment.transform.GetChild(3).GetComponent<Text>().text = title;
		achievment.transform.GetChild(5).GetComponent<Text>().text = achievments[title].Description;
		achievment.transform.GetChild(6).GetComponent<Text>().text = achievments[title].Points.ToString();
		// Prepei na valw na travaei to sprite apo to resources folder simfwna me to slug name tou achieve
		//achievment.transform.GetChild(4).GetComponent<Image>().sprite = 
	}

	public void ChangeCategory(GameObject button){
		AchievmentButton achievmentButton = button.GetComponent<AchievmentButton> ();
		Debug.Log (button.name);
		scrollRect.content = activeButton.achievmentList.GetComponent<RectTransform>();	// Pernoume to rectTransform tis kathe katigorias pou exoume orisei sto kathe koubi
		achievmentButton.Click();														// An to koubi itan energo tha to apenergopoiisoume
		activeButton.Click ();															// kai orizoume nai active button wste na allaksoume to sprite;
		activeButton = achievmentButton;												// Orizoume ws active button to teleuaio clicked button
	}

	private IEnumerator FadeAchievment(GameObject achievment){
		CanvasGroup canvasGroup = achievment.GetComponent<CanvasGroup> ();				// Pernoume to canvasGroup component to prefab

		float rate = 1.0f / fadeTime;
		int startAlpha = 0;
		int endAlpha = 1;


		for (int i = 0; i < 2; i++) {
			float progress = 0.0f;
			while (progress < 1.0) {
				canvasGroup.alpha = Mathf.Lerp (startAlpha, endAlpha, progress);
				progress += rate * Time.deltaTime;
				yield return null;
			}
			yield return new WaitForSeconds (2);
			startAlpha = 1;
			endAlpha = 0;
		}
		Destroy (achievment);

	}
}
