using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemCollection))]
[RequireComponent(typeof(EquipmentLoadout))]
public class Inventory : EditableMonoBehaviour
{
    [SerializeField]
    private ItemCollection itemCollection;
    [SerializeField]
    private EquipmentLoadout equipmentLoadout;
    [SerializeField]
    private Character character;

    public List<Item> Items
    {
        get { return itemCollection.Items; }
    }

    public int ItemCount
    {
        get { return itemCollection.ItemCount; }
    }

    public GameObject ItemPickupPrefab
    {
        get { return itemCollection.ItemPickupPrefab; }
        set { itemCollection.ItemPickupPrefab = value; }
    }

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    public ItemCollection Collection
    {
        get { return itemCollection; }
        set { itemCollection = value; }
    }

    public EquipmentLoadout EquipmentLoadout
    {
        get { return equipmentLoadout; }
        set { equipmentLoadout = value; }
    }

    public override void Initialize()
    {
        character = GetComponent<Character>();
        itemCollection = GetComponent<ItemCollection>();
        equipmentLoadout = GetComponent<EquipmentLoadout>();

        base.Initialize();
    }

    public void AddItemToInventory(Item item)
    {
        itemCollection.AddItemToCollection(item);
    }

    public void RemoveItemFromInventory(Item item)
    {
        itemCollection.RemoveItemFromCollection(item);
    }

    public void DropItem(Item item)
    {
        itemCollection.DropItem(item);
    }

    public void EquipItem(Item item)
    {
        equipmentLoadout.EquipItem(item as Equipment);
    }

    public void UnequipItem(Item item)
    {
        equipmentLoadout.UnequipItem(item as Equipment);
    }

    public void ConsumeItem(Item item)
    {
        if (!(item is Consumable))
            return;

        RemoveItemFromInventory(item);
        (item as Consumable).Consume(character);
    }
}
