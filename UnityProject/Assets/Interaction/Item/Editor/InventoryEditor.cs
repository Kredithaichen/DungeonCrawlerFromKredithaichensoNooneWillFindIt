using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]

public class InventoryEditor : Editor
{
    private Vector2 itemsScrollPosition;
    private Inventory inventory;

    void Awake()
    {
        inventory = target as Inventory;

        if (inventory == null)
        {
            Debug.LogError("target could not be casted to Inventory");
            return;
        }

        if (!inventory.Initialized)
            inventory.Initialize();
    }

    public override void OnInspectorGUI()
    {
        var inventory = target as Inventory;

        GUILayout.Label("General", EditorStyles.boldLabel);
        inventory.Character = (Character)EditorGUILayout.ObjectField("Possessing Character", inventory.Character, typeof(Character), true);
        inventory.Collection = (ItemCollection)EditorGUILayout.ObjectField("Internal Item Collection", inventory.Collection, typeof(ItemCollection), true);

        //inventory.TargetMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Player Mesh", inventory.TargetMesh, typeof(SkinnedMeshRenderer), true);
        //inventory.ItemPickupPrefab = (GameObject)EditorGUILayout.ObjectField("Item Pickup Prefab", inventory.ItemPickupPrefab, typeof(GameObject), true);

        /*GUILayout.Space(5f);
        GUILayout.Label("Items", EditorStyles.boldLabel);
        if (inventory.ItemCount > 0)
        {
            EditorGUILayout.LabelField("Number of Items: " + inventory.ItemCount.ToString());

            itemsScrollPosition = EditorGUILayout.BeginScrollView(itemsScrollPosition, GUILayout.MinHeight(200f));

            int ct = 0;
            foreach (var item in inventory.Items)
            {
                inventory.Items[ct++] = (Item)EditorGUILayout.ObjectField("Object", item, typeof(Item), true);

                if (item == null)
                    continue;

                item.ItemName = EditorGUILayout.TextField("Item Name", item.ItemName);
                item.Icon = (Sprite) EditorGUILayout.ObjectField("Icon", item.Icon, typeof(Sprite), true);
                GUILayout.Label("Description");
                item.Description = EditorGUILayout.TextArea(item.Description);

                GUILayout.Space(5f);
            }

            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("No Items in Inventory", EditorStyles.centeredGreyMiniLabel);

        if (GUILayout.Button("Add Item to Inventory"))
            inventory.AddItemToInventory(null);*/

        //GUILayout.Space(5f);
        //GUILayout.Label("Equipment", EditorStyles.boldLabel);

        /*inventory.EquippedItems[(int)EquipmentSlots.Head] = (Equipment)EditorGUILayout.ObjectField("Head", inventory.EquippedItems[(int)EquipmentSlots.Head], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Neck] = (Equipment)EditorGUILayout.ObjectField("Neck", inventory.EquippedItems[(int)EquipmentSlots.Neck], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Torso] = (Equipment)EditorGUILayout.ObjectField("Torso", inventory.EquippedItems[(int)EquipmentSlots.Torso], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Arms] = (Equipment)EditorGUILayout.ObjectField("Arms", inventory.EquippedItems[(int)EquipmentSlots.Arms], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Hands] = (Equipment)EditorGUILayout.ObjectField("Hands", inventory.EquippedItems[(int)EquipmentSlots.Hands], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Legs] = (Equipment)EditorGUILayout.ObjectField("Legs", inventory.EquippedItems[(int)EquipmentSlots.Legs], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Feet] = (Equipment)EditorGUILayout.ObjectField("Feet", inventory.EquippedItems[(int)EquipmentSlots.Feet], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Waist] = (Equipment)EditorGUILayout.ObjectField("Waist", inventory.EquippedItems[(int)EquipmentSlots.Waist], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Shield] = (Equipment)EditorGUILayout.ObjectField("Shield", inventory.EquippedItems[(int)EquipmentSlots.Shield], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Lamp] = (Equipment)EditorGUILayout.ObjectField("Lamp", inventory.EquippedItems[(int)EquipmentSlots.Lamp], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Weapon] = (Equipment)EditorGUILayout.ObjectField("Primary Weapon", inventory.EquippedItems[(int)EquipmentSlots.Weapon], typeof(Equipment), true);
        inventory.EquippedItems[(int)EquipmentSlots.Weapon + 1] = (Equipment)EditorGUILayout.ObjectField("Secondary Weapon", inventory.EquippedItems[(int)EquipmentSlots.Weapon + 1], typeof(Equipment), true);*/
    }
}
