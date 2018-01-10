using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class NPC : Interactable {

	public PlayerManager plManager;
	public PlayerCalculations plCalc;
	public GameObject parent;
	public GameObject questFrame;
	public GameObject completedFrame;
	public GameObject inprogressFrame;
	GameObject currentQuest;
	public QuestDatabase questDb;
	public ItemDatabase itemDb;
	public Inventory inv;
	public int npcId;
	public int questId;
	public GameObject SoundPlayer;
	public BoxCollider triggerBoxCollider;

	private bool showQuest = false;
	private bool questFlag = false;
	private bool playedOnce = false;


	private Quest temp;
	private AchievmentGenerator achievManager;

	void Start(){
		achievManager = GameObject.FindGameObjectWithTag ("AchievmentManager").GetComponent<AchievmentGenerator> ();
	}

	public override void Interact(){
		Debug.Log ("Interacting with NPC");
		if (!questDb.questList [questId].Chain) {
			if (!showQuest) {
				InstansiateTheQuest (SearchQuestDbById (questId));
			}
			if (GetComponent<AudioSource> () != null) {
				GetComponent<ButtonMusicPlayer> ().PlayGeneraGreetingClip ();
				playedOnce = false;
			}
			Debug.Log ("To quest pou theloume exei id " + SearchQuestDbById (questId).Description);
		} else {
			if (questDb.activeQuest [questDb.questList [questId].ChainId].NpcId == npcId) {			
				if (questDb.activeQuest [questDb.questList [questId].ChainId].Progress == questDb.activeQuest [questDb.questList [questId].ChainId].TrueObjective && !questDb.activeQuest [questDb.questList [questId].ChainId].Completed) {
					InstansiateTheCompleteDialog (questDb.activeQuest [questDb.questList [questId].ChainId]);
					//questDb.activeQuest [questId].Completed = true;			
				} else if (!questDb.activeQuest [questDb.questList [questId].ChainId].Completed && questDb.activeQuest [questDb.questList [questId].ChainId].Progress != questDb.activeQuest [questDb.questList [questId].ChainId].TrueObjective) {
					InstansiateInProgressDialog (questDb.activeQuest [questDb.questList [questId].ChainId]);
				} else if (questDb.activeQuest [questDb.questList [questId].ChainId].Completed) {
					if (questFlag && !showQuest) {
						InstansiateTheQuest (SearchQuestDbById (questId));
					}
				}

				if (GetComponent<AudioSource> () != null) {
					GetComponent<ButtonMusicPlayer> ().PlayGeneraGreetingClip ();
					triggerBoxCollider.enabled = true;
					playedOnce = false;
				}
			
			}
		}
	}

	public void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player" && !playedOnce) {
			GetComponent<ButtonMusicPlayer> ().PlayGeneralFarewellClip ();
			triggerBoxCollider.enabled = false;
			playedOnce = true;
		}
	}


	public Quest SearchQuestDbById(int id){
		for (int i = 0; i <= questDb.questList.Count; i++) {
			if (questDb.questList [i].Id == id) {
				temp = questDb.questList [i];
				break;
			} else {
				temp = null;
			}
		}
		return temp;
	}

	public void InstansiateTheQuest(Quest quest){
		currentQuest = (GameObject)Instantiate (questFrame);
		currentQuest.transform.SetParent (parent.transform);
		currentQuest.transform.localPosition = Vector3.zero;
		currentQuest.transform.localScale = new Vector3 (1, 1, 1);
		currentQuest.transform.GetChild (1).GetComponent<Text> ().text = quest.Title;
		currentQuest.transform.GetChild (2).GetComponent<Text> ().text = quest.SubText;
		currentQuest.transform.GetChild (4).GetComponent<Text> ().text = quest.Description;
		currentQuest.transform.GetChild (9).GetComponent<Text> ().text = quest.Xp.ToString ();
		if (quest.ItemSlug != null) {
			currentQuest.transform.GetChild (7).transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Items/" + quest.ItemSlug);
		} else {
			Debug.Log ("I have to close the image");
			currentQuest.transform.GetChild (7).gameObject.SetActive (false);
		}
		currentQuest.transform.GetChild (10).GetComponent<Button> ().onClick.AddListener (()=>{
			AddQuestToActive(quest.Id , currentQuest);
			SoundPlayer.GetComponent<ButtonMusicPlayer> ().PlayQuestAcceptClip ();
		});
		currentQuest.transform.GetChild (11).GetComponent<Button> ().onClick.AddListener (()=>{CloseWindow(currentQuest);});

		showQuest = true;
	}

	public void InstansiateTheCompleteDialog(Quest quest){
		currentQuest = (GameObject)Instantiate (completedFrame);
		currentQuest.transform.SetParent (parent.transform);
		currentQuest.transform.localPosition = Vector3.zero;
		currentQuest.transform.localScale = new Vector3 (1, 1, 1);
		currentQuest.transform.GetChild (1).GetComponent<Text> ().text = quest.Title;
		currentQuest.transform.GetChild (2).GetComponent<Text> ().text = quest.CompleteDialog;
		currentQuest.transform.GetChild (7).GetComponent<Text> ().text = quest.Xp.ToString ();
		if (quest.ItemSlug != null) {
			currentQuest.transform.GetChild (5).transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Items/" + quest.ItemSlug);
		} else {
			currentQuest.transform.GetChild (5).gameObject.SetActive (false);
		}
		currentQuest.transform.GetChild (8).GetComponent<Button> ().onClick.AddListener (()=>{
			FinishQuest(currentQuest,quest);
			achievManager.EarnQuestCompleteAchievment();
			//SoundPlayer.GetComponent<ButtonMusicPlayer>()
		});
		currentQuest.transform.GetChild (9).GetComponent<Button> ().onClick.AddListener (()=>{CloseWindow(currentQuest);});
	}

	public void InstansiateInProgressDialog(Quest quest){
		currentQuest = (GameObject)Instantiate (inprogressFrame);
		currentQuest.transform.SetParent (parent.transform);
		currentQuest.transform.localPosition = Vector3.zero;
		currentQuest.transform.localScale = new Vector3 (1, 1, 1);
		currentQuest.transform.GetChild (1).GetComponent<Text> ().text = quest.Title;
		currentQuest.transform.GetChild (2).GetComponent<Text> ().text = quest.InProgressDialog;
		currentQuest.transform.GetChild (3).GetComponent<Button> ().onClick.AddListener (()=>{CloseWindow(currentQuest);});
	}

	public void AddQuestToActive(int id , GameObject currentItem){
		questDb.activeQuest.Add (SearchQuestDbById (id));
		this.transform.GetChild (0).gameObject.SetActive (false);
		//Debug.Log ("I added a querst in the list");
		Destroy (currentItem);
		//Debug.Log ("I am calling this shit ");
	}

	public void CloseWindow(GameObject frame){
		frame.SetActive (!frame.activeSelf);
		if (npcId == 0) {
			showQuest = false;
		}
	}

	public void FinishQuest(GameObject frame, Quest quest){
		Destroy (frame);
		this.transform.GetChild (1).gameObject.SetActive (false);
		quest.Completed = true;
		plManager.XpToLevelUp += quest.Xp;
		plCalc.CalculateLevelUp (plManager.XpToLevelUp, plManager.MaxXp);
		Debug.Log (itemDb.FetchItemBySlug (quest.ItemSlug));
		if(quest.ItemSlug != null)
		inv.AddItem (itemDb.FetchItemBySlug (quest.ItemSlug));
		StartCoroutine (WaitBeforeNewQuest ());
		
	}

	public void AcceptQuest(GameObject frame){
		frame.transform.GetChild (10).transform.GetChild (0).GetComponent<ButtonMusicPlayer> ().PlayQuestAcceptClip ();
	}

	IEnumerator WaitBeforeNewQuest(){
		yield return new WaitForSeconds (1f);
		transform.GetChild (0).gameObject.SetActive (true);
		showQuest = false;
		questFlag = true;

	}


}
