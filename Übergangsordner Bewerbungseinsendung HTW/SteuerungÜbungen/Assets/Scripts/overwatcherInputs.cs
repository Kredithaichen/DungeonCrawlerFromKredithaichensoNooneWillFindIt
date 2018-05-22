using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overwatcherInputs : MonoBehaviour {

    public float inputHorizontal;
    public float inputVertical;
    public bool inputFire1;
    public bool inputFire2;
    public bool inputFire3;
    public bool inputJump;
    public float inputMouseX;
    public float inputMouseY;
    public float inputMouseScrollWheel;
    public float inputRightStickHorizontal;
    public float inputRightStickVertical;


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical"); 
        inputFire1 = Input.GetButton("Fire1");
        inputFire2 = Input.GetButton("Fire2");
        inputFire3 = Input.GetButton("Fire3");
        inputJump = Input.GetButton("Jump");
        inputRightStickHorizontal = Input.GetAxis("RightHorizontal");
        inputRightStickVertical = Input.GetAxis("RightVertical");
}
}
