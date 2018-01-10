using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

    public GameObject TouchedBlock;
    public Rigidbody2D rigid;
    private float velMag;
    private Vector3 vel;
    public float speedLimit;

    private Vector3 borderBlockPos;
    public GameObject gridGO;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        speedLimit = 10;
    }

    // Update is called once per frame
    void FixedUpdate () {

        velMag = rigid.velocity.magnitude;

        if (velMag >= 10.0f) {

            rigid.velocity = rigid.velocity.normalized * speedLimit; }
       
        if (velMag == 0) {
            Debug.Log(velMag);
            if (borderBlockPos.x<=gridGO.GetComponent<GridCreation>().GridOffset.x) {
                rigid.AddForce(-borderBlockPos, ForceMode2D.Impulse);
                Debug.Log("Im ADDING LEFT");
                this.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y);

            }
            else if (borderBlockPos.x >= Mathf.Abs(gridGO.GetComponent<GridCreation>().GridOffset.x))
            {
                rigid.AddForce(-borderBlockPos, ForceMode2D.Impulse);
                Debug.Log("Im ADDING RIGHT");
                this.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y);
            } else if (borderBlockPos.y <= gridGO.GetComponent<GridCreation>().GridOffset.y)
            {
                rigid.AddForce(-borderBlockPos, ForceMode2D.Impulse);
                Debug.Log("Im ADDING UP");
                this.transform.position = new Vector2(transform.position.x, transform.position.y+0.3f);

            }
            else if (borderBlockPos.y >= Mathf.Abs(gridGO.GetComponent<GridCreation>().GridOffset.y))
            {
                rigid.AddForce(-borderBlockPos, ForceMode2D.Impulse);
                Debug.Log("Im ADDING DOWN");
                this.transform.position = new Vector2(transform.position.x, transform.position.y-0.3f);

            }

        }

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "ActivatedBlock") && (velMag>3.0f)) 
        {

            Destroy(col.gameObject);
            //TouchedBlock = col.gameObject;
            
                }

        if (col.gameObject.tag == "EdgeBlock") {
            borderBlockPos = col.gameObject.transform.position;
            //Debug.Log(borderBlockPos);
        }
    }
}
