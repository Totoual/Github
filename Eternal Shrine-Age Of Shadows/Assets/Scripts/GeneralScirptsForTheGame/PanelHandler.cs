using UnityEngine;
using System.Collections;

public class PanelHandler : MonoBehaviour {
	public GameObject mainMenuPanel;
	public GameObject optionsPanel;
	public GameObject Title;

	// Use this for initialization
	void Start () {
		optionsPanel.SetActive (false);
		mainMenuPanel.SetActive (true);
		Title.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OpenOptions(){
		mainMenuPanel.SetActive (false);
		Title.SetActive (false);
		optionsPanel.SetActive (true);

	}
	public void CloseOptions(){
		optionsPanel.SetActive (false);
		mainMenuPanel.SetActive (true);
		Title.SetActive (true);
	}
}
