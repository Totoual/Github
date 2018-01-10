using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour {

	public Toggle fullScreenTogle;
	public Dropdown resolutionDropDown;
	public Dropdown textQualityDropDown;
	public Dropdown antialiasingDropDown;
	public Dropdown vSyncDropDown;

	public Resolution[] resolutions;
	public GameSettings gameSettings;

	void OnEnable(){

		gameSettings = new GameSettings ();

		fullScreenTogle.onValueChanged.AddListener (delegate {OnFullScreenToggle();});				// Vazoume listeners sta koubia gia na kaloun mia sygkekrimeni methodo
		resolutionDropDown.onValueChanged.AddListener (delegate {OnResolutionChange();});			// Vazoume listeners sta koubia gia na kaloun mia sygkekrimeni methodo			
		textQualityDropDown.onValueChanged.AddListener (delegate {OnTextQualityChange();});			// Vazoume listeners sta koubia gia na kaloun mia sygkekrimeni methodo
		antialiasingDropDown.onValueChanged.AddListener (delegate {OnAntialiasingChange();});		// Vazoume listeners sta koubia gia na kaloun mia sygkekrimeni methodo
		vSyncDropDown.onValueChanged.AddListener (delegate {OnVsyncChange();});						// Vazoume listeners sta koubia gia na kaloun mia sygkekrimeni methodo



		resolutions = Screen.resolutions;															// Orizoume ton pinika resolution ola ta resolutions pou boroume na exoume
		foreach (Resolution res in resolutions) {

			resolutionDropDown.options.Add(new Dropdown.OptionData(res.ToString()));				// Kai ta thetoume ws epiloges sto dropdown menu

		}

		LoadSettings ();
	
	}

	public void OnFullScreenToggle(){
		
		gameSettings.fullScreen = Screen.fullScreen = fullScreenTogle.isOn;							// Kanoume handle to koubi tou full screen
	
	}
	public void OnResolutionChange(){

		Screen.SetResolution (resolutions [resolutionDropDown.value].width, resolutions [resolutionDropDown.value].height, Screen.fullScreen);		// Kanoume Handle to resolution
		gameSettings.resolutionIndex = resolutionDropDown.value;
		
	}
	public void OnTextQualityChange(){
		QualitySettings.masterTextureLimit = gameSettings.textureQuality = textQualityDropDown.value;							// Kanoume Handle to Quality twn Texture

	}
	public void OnAntialiasingChange(){

		QualitySettings.antiAliasing = gameSettings.antialisingLvl = (int)Mathf.Pow (2f, antialiasingDropDown.value);			// Kanoume Handle to Antialiasing
	
	}
	public void OnVsyncChange(){
		QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropDown.value;													// Kanoume Handle to vSync
	}

	public void SaveSettings(){
	  
		string jsonData = JsonUtility.ToJson (gameSettings , true);
		File.WriteAllText (Application.persistentDataPath + "/gamesettings.json",jsonData);
		Debug.Log (Application.persistentDataPath);
	}

	public void LoadSettings(){

		gameSettings = JsonUtility.FromJson<GameSettings> (File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
		antialiasingDropDown.value = gameSettings.antialisingLvl;
		vSyncDropDown.value = gameSettings.vSync;
		resolutionDropDown.value = gameSettings.resolutionIndex;
		textQualityDropDown.value = gameSettings.textureQuality;
		fullScreenTogle.isOn = gameSettings.fullScreen;
		Screen.fullScreen = gameSettings.fullScreen;
		resolutionDropDown.RefreshShownValue ();
	
	}


	

}
