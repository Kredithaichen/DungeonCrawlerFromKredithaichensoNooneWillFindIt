using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemPickup))]
public class ItemPickupEditor : Editor
{
    [SerializeField]
    private ItemPickup itemPickup;

    void Awake()
    {
        itemPickup = target as ItemPickup;

        if (itemPickup == null)
            Debug.LogError("target could not be casted to ItemPickup");
    }

    public override void OnInspectorGUI()
    {
        var changesMade = false;

        GUILayout.Label("Content", EditorStyles.boldLabel);
        var item = (Item)EditorGUILayout.ObjectField("Item to pick up", itemPickup.Item, typeof(Item), true);
        if (item != itemPickup.Item)
        {
            changesMade = true;
            itemPickup.Item = item;
        }

        if (changesMade)
            EditorUtility.SetDirty(itemPickup);
    }
}
