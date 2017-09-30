using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(ItemCollection))]
public class ItemContainer : EditableMonoBehaviour
{
    [SerializeField]
    private Interactable interactable;
    [SerializeField]
    private ItemCollection itemCollection;

    [SerializeField]
    private string containerName;

    public string ContainerName
    {
        get { return containerName; }
        set { containerName = value; }
    }

    public Interactable Interactable
    {
        get { return interactable; }
        set { interactable = value; }
    }

    public ItemCollection Collection
    {
        get { return itemCollection; }
        set { itemCollection = value; }
    }

    public override void Initialize()
    {
        interactable = GetComponent<Interactable>();
        itemCollection = GetComponent<ItemCollection>();

        base.Initialize();
    }
}
