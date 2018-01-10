using UnityEngine;
using System.Collections;

public class QuestProgress : MonoBehaviour {
	private QuestDatabase questDb;
	public GameObject[] NPC;

	private int randomNumber;
	// Use this for initialization
	void Start () {

		questDb = GetComponent<QuestDatabase> ();
	
	}

	void Update(){
		
	}
	public void AddToProgress(int id){
		questDb.activeQuest [id].Progress++;
		Debug.Log ("I added 1 in the progress");
		if(questDb.activeQuest[id].Progress >= questDb.activeQuest[id].TrueObjective){
			questDb.activeQuest [id].Progress = questDb.activeQuest [id].TrueObjective;
			SearchTheNpcThatCompleteTheQuest (id);
		}
	}

	public void SearchTheNpcThatCompleteTheQuest(int id){
		foreach (GameObject temp in NPC) {
			if (questDb.activeQuest [id].NpcId == temp.GetComponent<NPC> ().npcId) {
				if (questDb.activeQuest [id].Progress == questDb.activeQuest [id].TrueObjective) {
					temp.transform.GetChild (1).gameObject.SetActive (true);
					break;
				}
			}
		}
	}

	public bool CalculateDropChance(){
		if (questDb.activeQuest.Count != 0) {
			if (!questDb.activeQuest [0].Completed) {
				randomNumber = Random.Range (0, 101);
				if (randomNumber <= 50) {
					
					return true;
				}
			}

		}
		return false;
   }
}
