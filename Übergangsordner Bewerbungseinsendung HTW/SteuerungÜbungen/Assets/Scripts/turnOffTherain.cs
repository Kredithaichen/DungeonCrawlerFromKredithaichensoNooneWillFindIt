using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffTherain : MonoBehaviour {

    public GameObject rain;
    public GameObject characterSlotThingy;
    public Collider rainOffZone;

	// Use this for initialization
	void Start () {
       // rain = GetComponent<ParticleEmitter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (rain.enabled)
        {
            rain.enabled = false;
        }
        if (rain.enabled == false)
        {
            rain.enabled = true;
        }
    }*/
}
