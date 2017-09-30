using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable :EditableMonoBehaviour
{
    [SerializeField]
    private float interactionRadius = 3f;
    [SerializeField]
    private float noticeRadius = 10f;

    public const string InteractionTriggerArea = "InteractionTriggerArea";
    public const string NoticeTriggerArea = "NoticeTriggerArea";

    public float InteractionRadius
    {
        get { return interactionRadius; }
        set { interactionRadius = value; }
    }

    public float NoticeRadius
    {
        get { return noticeRadius; }
        set { noticeRadius = value; }
    }

    void Start()
    {
        // create trigger for interaction
        var obj = new GameObject(InteractionTriggerArea);
        obj.transform.SetParent(transform, false);
        var sphereCollider = obj.AddComponent<SphereCollider>();
        sphereCollider.radius = interactionRadius;
        sphereCollider.isTrigger = true;

        // create trigger for notice
        obj = new GameObject(NoticeTriggerArea);
        obj.transform.SetParent(transform, false);
        sphereCollider = obj.AddComponent<SphereCollider>();
        sphereCollider.radius = noticeRadius;
        sphereCollider.isTrigger = true;
    }

    void OnDrawGizmosSelected()
    {
        // draw interaction radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);

        // draw notice radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, noticeRadius);
    }

    public bool IsInteractableOfType<T>()
    {
        return GetComponent<T>() != null;
    }

    public T AsInteractableOfType<T>()
    {
        return GetComponent<T>();
    }
}
