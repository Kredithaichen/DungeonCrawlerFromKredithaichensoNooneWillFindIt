using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Destructible : EditableMonoBehaviour
{
    [SerializeField]
    private bool destroyOnCollision = true;
    [SerializeField]
    private float destructionThreshold = 5f;

    [SerializeField]
    private GameObject destroyedVersion;

    public delegate void OnDestroyHandler();

    public event OnDestroyHandler OnDestroy;

    public bool DestroyOnCollision
    {
        get { return destroyOnCollision; }
        set { destroyOnCollision = value; }
    }

    public float DestructionThreshold
    {
        get { return destructionThreshold; }
        set { destructionThreshold = value; }
    }

    public GameObject DestroyedVersion
    {
        get { return destroyedVersion; }
        set { destroyedVersion = value; }
    }

    public GameObject DestroyObject()
    {
        // spawn a shattered object
        var obj = Instantiate(destroyedVersion, transform.position, transform.rotation);
        obj.name = destroyedVersion.name + "_destroyed";

        // call any handlers
        if (OnDestroy != null)
            OnDestroy.Invoke();

        // remove the current object
        Destroy(gameObject);

        return obj;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (destroyOnCollision && collision.relativeVelocity.magnitude > destructionThreshold)
            this.DestroyObject();
    }
}
