using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableScriptableObject : ScriptableObject
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

    void Awake()
    {
        if (!initialized)
            Initialize();
    }
}
