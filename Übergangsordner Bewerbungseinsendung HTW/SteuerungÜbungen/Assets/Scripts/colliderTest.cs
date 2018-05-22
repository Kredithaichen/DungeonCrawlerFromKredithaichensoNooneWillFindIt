using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTest : MonoBehaviour {

    public ParticleSystem cryMeARiver;

    private void Awake()
    {
        cryMeARiver = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider col)
    {
        cryMeARiver.Play();
    }
}
