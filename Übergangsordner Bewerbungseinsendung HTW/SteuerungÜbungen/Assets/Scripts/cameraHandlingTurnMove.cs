using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandlingTurnMove : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 8.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * -8.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += transform.right * -5.0f * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * 5.0f * Time.deltaTime;
            }
        }
        else{
            if (Input.GetKey(KeyCode.A)){ 
            transform.Rotate(Vector3.up, -250.0f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D)){
            transform.Rotate(Vector3.up, 250.0f * Time.deltaTime);
            }
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * 40.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position += transform.up * -10.0f * Time.deltaTime;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
