using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BackgroundMovement : MonoBehaviour//, IPointerClickHandler
{

	public float objectSpeed = 2;
	public float resetPosition;
	public float startPosition;

    //public GameObject player;

	void Update () {
		if (GameManager.instance.GameOver != true && GameManager.instance.PlayerActive) {
			transform.Translate(Vector2.left * (GameManager.instance.environmentSpeed * Time.deltaTime));
			if (transform.localPosition.x <= resetPosition) {
				Vector2 newPos = new Vector2 (startPosition, transform.position.y);
				transform.position = newPos;
			}
		}	
	}





    /*public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
        {
            player.GetComponent<Player>().gameOn = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }*/



}
