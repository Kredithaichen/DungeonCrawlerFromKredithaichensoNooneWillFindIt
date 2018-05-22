using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingCam : MonoBehaviour {

    public Camera myCam;
    public float camSpeed;
    public float rot;
    public Vector2 upDownRotationLimit;

    // public float viewRotationX;
    // public float viewRotationY;

    // Use this for initialization
    void Start () {
        camSpeed = 225.0f;
	}
	
	// Update is called once per frame
	void Update () {


        /* if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right, -250.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right, 250.0f * Time.deltaTime);
        }
        */


        // transform.Translate(Vector3.forward * (Input.GetAxis("RightHorizontal") * camSpeed * Time.deltaTime));
        // viewRotationX = Input.GetAxis("RightVertical");
        // viewRotationY = Input.GetAxis("RightHorizontal");

        transform.Rotate(Vector3.right * Input.GetAxis("RightVertical") * camSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * Input.GetAxis("RightHorizontal") * camSpeed * Time.deltaTime);

        
        /*rot = transform.Rotate(Vector3.right * Input.GetAxis("RightVertical") * camSpeed);

        if (rot < leftRightRotationLimit) //Check for lower limit
            rot = leftRightRotationLimit;
        if (rot > upDownRotationLimit.x) //Check for upper limit
            rot = upDownRotationLimit;*/
    }
}
