using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemCollection))]
public class ItemCollectionEditor : Editor
{
    private ItemCollection itemCollection;
    private EquipmentLoadout equipmentLoadout;

    void Awake()
    {
        itemCollection = target as ItemCollection;

        if (itemCollection == null)
        {
            Debug.LogError("target could not be cast to ItemCollection");
            return;
        }

        if (!itemCollection.Initialized)
            itemCollection.Initialize();

        equipmentLoadout = itemCollection.GetComponent<EquipmentLoadout>();
    }

    public override void OnInspectorGUI()
    {
        var changesMade = false;

        GUILayout.Label("Items", EditorStyles.boldLabel);

        if (itemCollection.ItemCount == 0)
            GUILayout.Label("No Items in Inventoray", EditorStyles.centeredGreyMiniLabel);
        else
        {
            var saveSpace = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 30f;

            EditorGUILayout.BeginVertical();

            for (int i = 0; i < itemCollection.ItemCount; i++)
            {
                EditorGUILayout.BeginHorizontal();

                var item = (Item)EditorGUILayout.ObjectField((i + 1).ToString(), itemCollection.Items[i], typeof(Item), true);
                if (item != itemCollection.Items[i])
                {
                    changesMade = true;
                    itemCollection.Items[i] = item;
                }

                if (equipmentLoadout != null &&
                    GUILayout.Button("Equip", EditorStyles.miniButton, GUILayout.Width(50f)))
                    equipmentLoadout.EquipItem(itemCollection.Items[i] as Equipment);

                if (GUILayout.Button("Remove", EditorStyles.miniButton, GUILayout.Width(70f)))
                    itemCollection.RemoveItemFromCollection(itemCollection.Items[i]);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            EditorGUIUtility.labelWidth = saveSpace;
        }

        GUILayout.Space(10f);
        if (GUILayout.Button("New"))
            itemCollection.AddItemToCollection(null);

        if (changesMade)
            EditorUtility.SetDirty(itemCollection);
    }
}
