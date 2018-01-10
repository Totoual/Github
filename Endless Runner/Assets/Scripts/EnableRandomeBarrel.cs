using UnityEngine;
using System.Collections;

public class EnableRandomeBarrel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EnableRandomBarrels ();
		EnableRandomSouls ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/// <summary>
	/// Kathe fora pou energopoiite i sygekrimeni platforma paragei enan arithmo apo to 2 ews to 5 gia to posa 
	/// empodia tha yparoxun panw stin platforma.
	/// stin synexia kanei mia loopa kai kathe fora paragei enan arithmo apo to 3 ews to 9 (einai ta child tis sygekrimenis platformas)
	/// kai ta energopoiei
	/// </summary>
	public void EnableRandomBarrels(){
		int rand = Random.Range (2, 6);

		for (int i = 0; i <= rand; i++) {
			int temp = Random.Range (3, 10);
			this.transform.GetChild (temp).gameObject.SetActive (true);
		}

	}

	/// <summary>
	/// Kathe fora pou energopoiite i sygekrimeni platforma paragei enan arithmo apo to 5 ews to 13 gia to posa 
	/// souls tha yparoxun panw stin platforma.
	/// stin synexia kanei mia loopa kai kathe fora paragei enan arithmo apo to 10 ews to 27 (einai ta child tis sygekrimenis platformas)
	/// kai ta energopoiei
	/// </summary>
	public void EnableRandomSouls(){
		int rand = Random.Range (5,13);

		for (int i = 0; i <= rand; i++) {
			int temp = Random.Range (10, 28);
			this.transform.GetChild (temp).gameObject.SetActive (true);
		}

	}
}
