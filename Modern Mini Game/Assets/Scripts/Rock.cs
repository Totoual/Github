using UnityEngine;
using System.Collections;

public class Rock : Objects {

	[SerializeField] Vector3 topPosition;
	[SerializeField] Vector3 bottomPosition;
	[SerializeField] float speed;

	// Use this for initialization
	void Start () {
		StartCoroutine (Move (bottomPosition));
	}

	protected override void Update(){
		if (GameManager.instance.PlayerActive) {
			base.Update ();
		}
	}

	IEnumerator Move(Vector3 target){
		while (Mathf.Abs((target - transform.localPosition).y) > 0.20f) {					// Oso i apoliti diafora tou target me to local position ston aksona y einai megalitero to 0.20f
			Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;		// Dimiourgoume ena Vector 3 kai tou orizoume na pernei tin timi vector3.Up an einai iso me to top position alliws na parei to down
			transform.localPosition += direction * Time.deltaTime * speed;

			yield return null;
		}
		yield return new WaitForSeconds (0.5f);												//Perimenoume miso deuterolepto

		Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;		// Elegxoume an to y mas einai iso me to top position an einai idio tote prepei na pame pros ta katw

		StartCoroutine (Move (newTarget));													// Kai kaloume tin routina pali me to kainourgio position;
	}
}
