using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour {

    public float speed = 50.0f;
    public Rigidbody2D rigid;
    public bool isFlat = true;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.Translate (Input.acceleration.x * speed * Time.deltaTime, Input.acceleration.y * speed * Time.deltaTime, 0);

        Vector3 tilt = Input.acceleration;

        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0)*tilt;
        rigid.AddForce(tilt * speed);
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.white);
    }
}
