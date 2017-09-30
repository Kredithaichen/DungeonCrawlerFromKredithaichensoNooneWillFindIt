using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemCollection))]
[RequireComponent(typeof(Destructible))]
public class DestructibleContainer : MonoBehaviour
{
    private ItemCollection itemCollection;
    private Destructible destructible;

    void Start()
    {
        itemCollection = GetComponent<ItemCollection>();
        destructible = GetComponent<Destructible>();

        destructible.OnDestroy += Destroy;
    }

    private void Destroy()
    {
        while (itemCollection.ItemCount > 0)
            itemCollection.DropItem(itemCollection.Items[0]);
    }

    public GameObject DestroyObject()
    {
        return destructible.DestroyObject();
    }
}
