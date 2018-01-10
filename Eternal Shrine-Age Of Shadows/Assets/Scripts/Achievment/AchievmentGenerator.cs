using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class AchievmentGenerator : MonoBehaviour {

	//Public Variables
	public GameObject achievmentPrefab; // To prefab tou achiev pou tha kanoume instantiate;
	public ScrollRect scrollRect;				// Prepei na allazoume ton slider
	public GameObject earnedAchievment;			// To achiev pou tha dixnoume stin othoni mas
	public Sprite unlockedSprite;
	public GameObject achievmentPanel;

	public AudioClip earnTheAchievClip;

	//Private Variables
	[SerializeField] private List<GenericAchiev> achievmentDatabase = new List<GenericAchiev> ();		// Gia testing thelw na vlepw tin lista
	private JsonData itemData;					// Dimiourgo to adikeimeno apo to json
	private AchievmentButton activeButton;		// Theloume na kseroume poio koubi einai active
	private float fadeTime = 2f;					// Xronos gia fade out

	//Singleton
	private static AchievmentGenerator achivInstance;
	private static int skeletonCounter=0;
	private static int firstloot=0;
	private static bool completeFirstQuest;

	#region Getter of Singleton
	public static AchievmentGenerator AchivInstance{
		get{
			if(achivInstance == null){														// Elegxoume an yparxei 
				achivInstance = GameObject.FindObjectOfType<AchievmentGenerator> (); 		// An den yparxei vriskoume to adikeimeno autou tou typou
			}
			return AchievmentGenerator.achivInstance;}
	}
	#endregion


	// Use this for initialization
	void Start () {
		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath+"/StreamingAssets/Achievments.json"));
		ConstructAchievmentDatabase ();
		activeButton = GameObject.Find ("Summary_Button").GetComponent<AchievmentButton> ();
		activeButton.Click ();

		foreach (GameObject achievmentList in GameObject.FindGameObjectsWithTag("AchievmentList")) {			// Apenergopoioume oles ti katigories pou den theloume afou exoume dimiourgisei ta achievment
			achievmentList.SetActive (false);
		}

		//Debug.Log (achievmentDatabase [1].Title);
	}

	void ConstructAchievmentDatabase(){
		for (int i = 0; i < itemData.Count; i++) {
			achievmentDatabase.Add (new GenericAchiev ((int)itemData[i]["id"],itemData[i]["title"].ToString(),itemData[i]["description"].ToString(),itemData[i]["category"].ToString(),itemData[i]["slug"].ToString(),(int)itemData[i]["points"],(int)itemData[i]["unlocked"]));
			CreateAchievmentList (i);
			if (i == itemData.Count) {
				achievmentPanel.SetActive (false);
			}
		}


	}

	void CreateAchievmentList(int id){
		GameObject achievment = (GameObject)Instantiate (achievmentPrefab);
		SetAchievInfo (id, achievmentDatabase [id].Category, achievment);

		
	}
	public void EarnAchievment(int id){
		if (achievmentDatabase [id].EarnAchievment()) {
			achievmentDatabase [id].Unlocked = true;
			GetComponent<AudioSource> ().clip = earnTheAchievClip;
			GetComponent<AudioSource> ().Play ();
			GameObject achievment = (GameObject)Instantiate (earnedAchievment);		// Emfanizoume stin othoni to achievment pou kerdise o paixtis;
			//Debug.Log(GameObject.Find(achievmentDatabase[id].Category).transform.FindChild(achievmentDatabase[id].Title));
			//GameObject.Find(achievmentDatabase[id].Category).transform.FindChild(achievmentDatabase[id].Title).GetComponent<Image>().sprite = unlockedSprite;
			SetAchievInfo (id, "EarnCanvas", achievment);
			StartCoroutine(FadeAchievment(achievment));
		}
	}

	public void SetAchievInfo(int id, string parent, GameObject achiev){
		achiev.transform.SetParent (GameObject.Find(parent).transform);
		achiev.transform.localScale = new Vector3 (1, 1, 1);
		achiev.transform.GetChild(3).GetComponent<Text>().text =achievmentDatabase [id].Title;
		achiev.name = achievmentDatabase [id].Title;
		achiev.transform.GetChild(4).GetComponent<Image>().sprite =achievmentDatabase [id].Icon;
		achiev.transform.GetChild(5).GetComponent<Text>().text =achievmentDatabase [id].Description;
		achiev.transform.GetChild(6).GetComponent<Text>().text = achievmentDatabase [id].Points.ToString();
	}

	public void ChangeCategory(GameObject button){
		AchievmentButton achievmentButton = button.GetComponent<AchievmentButton> ();
		Debug.Log (button.name);
		scrollRect.content = activeButton.achievmentList.GetComponent<RectTransform>();	// Pernoume to rectTransform tis kathe katigorias pou exoume orisei sto kathe koubi
		achievmentButton.Click();														// An to koubi itan energo tha to apenergopoiisoume
		activeButton.Click ();															// kai orizoume nai active button wste na allaksoume to sprite;
		activeButton = achievmentButton;												// Orizoume ws active button to teleuaio clicked button
		ChangeSprite(activeButton);
	}

	public void ChangeSprite(AchievmentButton activebutton){
		for (int i = 0; i < achievmentDatabase.Count; i++) {
			if (achievmentDatabase [i].Category == activeButton.achievmentList.name && achievmentDatabase[i].Unlocked==true) {
				GameObject.Find(achievmentDatabase[i].Category).transform.FindChild(achievmentDatabase[i].Title).GetComponent<Image>().sprite = unlockedSprite;
			}
		}
		
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

	public void EarnSlayingAchievment(){
		skeletonCounter++;
		Debug.Log (skeletonCounter);
		if (skeletonCounter == 10) {
			EarnAchievment (6);
		} else if (skeletonCounter == 20) {
			EarnAchievment (7);
		} else if (skeletonCounter == 50) {
			EarnAchievment (8);
		}
	}

	public void EarnLootingAchievment(){
		firstloot++;
		if (firstloot ==1) {
			EarnAchievment (1);
		} 
	}

	public void EarnQuestCompleteAchievment(){
		completeFirstQuest = true;
		if (completeFirstQuest) {
			EarnAchievment (0);
		}
	}
	// Update is called once per frame
	void Update () {

	}
}
