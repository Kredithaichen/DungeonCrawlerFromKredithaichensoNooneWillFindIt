using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : EditableMonoBehaviour
{
    [SerializeField]
    private List<Item> items;
    [SerializeField]
    private GameObject itemPickupPrefab;

    public List<Item> Items
    {
        get { return items; }
        set { items = value; }
    }

    public int ItemCount
    {
        get { return items.Count; }
    }

    public GameObject ItemPickupPrefab
    {
        get { return itemPickupPrefab; }
        set { itemPickupPrefab = value; }
    }

    public override void Initialize()
    {
        items = new List<Item>();

        var obj = GameObject.FindGameObjectWithTag("GameSession");
        itemPickupPrefab = obj.GetComponent<GameSession>().DroppedItemPrefab;

        base.Initialize();
    }

    public void AddItemToCollection(Item item)
    {
        items.Add(item);
    }

    public void RemoveItemFromCollection(Item item)
    {
        items.Remove(item);
    }

    public void DropItem(Item item)
    {
        RemoveItemFromCollection(item);

        var obj = Instantiate(itemPickupPrefab, transform.position + Vector3.up, new Quaternion());
        obj.name = "ItemPickup_" + item.ItemName;

        var pickup = obj.AddComponent<ItemPickup>();
        pickup.Item = item;
    }
}
