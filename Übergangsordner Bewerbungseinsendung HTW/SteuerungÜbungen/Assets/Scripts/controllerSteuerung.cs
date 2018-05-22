using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerSteuerung : MonoBehaviour {

    public float speed;
    public float camSpeed;
    public float turnSpeed;

    public Camera cam;


	// Use this for initialization
	void Start () {
        speed = 20.0f;
        camSpeed = 30.0f;
        turnSpeed = 3.0f;

        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * (Input.GetAxis("Vertical") * speed * Time.deltaTime));
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed);

       // transform.Translate(Vector3.forward * (Input.GetAxis("RightVertical") * camSpeed * Time.deltaTime));
       // transform.Rotate(Vector3.up * Input.GetAxis("RightHorizontal"));

        if (Input.GetButton("Fire2"))
        {
            speed = 40.0f;
            turnSpeed = 1.5f;
        }
        else
        {
            speed = 20.0f;
            turnSpeed = 3.0f;
        }

    }
}
