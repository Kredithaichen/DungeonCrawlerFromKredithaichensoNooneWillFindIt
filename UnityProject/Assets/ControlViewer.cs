using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlViewer : MonoBehaviour
{
    private Camera cam;

	// Use this for initialization
	void Start ()
    {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
        var p = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        transform.LookAt(new Vector3(p.x, 0, p.z));
	}
}
