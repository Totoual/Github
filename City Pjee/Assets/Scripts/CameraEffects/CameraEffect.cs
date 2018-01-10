using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraEffect : MonoBehaviour {
	public float speed;
	public float seconds;
	public GameObject player;
	public float offset;

	public GameObject fadeOutTexture; // The texture that will overlay the screen.
	public float fadeSpeed = 0.8f;	 // The speed that we are fading out.
	public GameObject canvas;
	public GameObject logo;
    public GameObject gameLogo;



    private float alpha = 1.0f; 	// the texture's alpha value between 0 and 1
	private int fadeDir = -1;		// the direction to fade: in in = -1 or out =1
	private float timePassed;
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetMouseButtonDown (0))
        {
            if  (!player.GetComponent<Player>().IsPointerOverUIObject())       //////////mariza
            {
                speed = 8f;
            }
		}
		if (transform.position.y >= 0) {
			Fade ();

			transform.Translate (Vector2.down * (speed * Time.deltaTime));
		} else {
			GameManager.instance.EnterGame ();
			canvas.SetActive (true);
			logo.SetActive (false);
            //gameLogo.SetActive(false);
		}

		if (!GameManager.instance.GameOver && GameManager.instance.GameStarted) {
			//Debug.Log (GameManager.instance.MustFollow);
			if (GameManager.instance.MustFollow) {
				//transform.position = new Vector3 (transform.position.x, player.transform.position.y + offset, transform.position.z);
				float lineraY = Mathf.Lerp(transform.position.y,player.transform.position.y ,50f);
				transform.position = new Vector3 (transform.position.x,lineraY+offset,transform.position.z);
			} else if (!GameManager.instance.MustFollow) {
				transform.position = new Vector3 (transform.position.x,0, transform.position.z);
			}
		}
		if (GameManager.instance.GameOver) {
			transform.position = new Vector3 (transform.position.x,0, transform.position.z);
		}
			

	}

	public void Fade(){
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		//force (clamp) the number between 0 and 1.because color uses alpha values between 0 and 1.
		alpha = Mathf.Clamp01 (alpha);
		fadeOutTexture.GetComponent<SpriteRenderer> ().color = new Color (fadeOutTexture.GetComponent<SpriteRenderer> ().color.r, fadeOutTexture.GetComponent<SpriteRenderer> ().color.g, fadeOutTexture.GetComponent<SpriteRenderer> ().color.b, alpha); 
		if (alpha <= 0) {
			fadeOutTexture.gameObject.SetActive (false);
		}
//		if (GetComponent<Vignette> ().VignettePower >= 30) {
//			GetComponent<Vignette> ().VignettePower -= 200 * Time.deltaTime;
//		} else if (GetComponent<Vignette> ().VignettePower <= 30 && GetComponent<Vignette> ().VignettePower > 0) {
//			GetComponent<Vignette> ().VignettePower -= 100 * Time.deltaTime;
//		} else if (GetComponent<Vignette> ().VignettePower <= 10 && GetComponent<Vignette> ().VignettePower > 0) {
//			GetComponent<Vignette> ().VignettePower -= 5 * Time.deltaTime;
//		}else if (GetComponent<Vignette> ().VignettePower <= 0) {
//			GetComponent<Vignette>().enabled = false;
//		}
		timePassed += Time.deltaTime;
//		GetComponent<Vignette>().VignettePower = Mathf.Lerp(300,0, timePassed/1.5f);
//		if (GetComponent<Vignette> ().VignettePower <= 0) {
//			GetComponent<Vignette> ().VignettePower = 0;
//			GetComponent<Vignette>().enabled = false;
//		}
	}


}
