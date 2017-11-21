﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool initialized;

    public bool Initialized
    {
        get { return initialized; }
    }

    public virtual void Initialize()
    {
        initialized = true;
    }

    public virtual void OnStartInitialize()
    {
    }

	void Start ()
    {
	    if (!initialized)
            Initialize(); 	

        OnStartInitialize();
	}
}
