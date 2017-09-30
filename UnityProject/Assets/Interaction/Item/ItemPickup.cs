using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private Item item;

    public Item Item
    {
        get { return item; }
        set { item = value; }
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
