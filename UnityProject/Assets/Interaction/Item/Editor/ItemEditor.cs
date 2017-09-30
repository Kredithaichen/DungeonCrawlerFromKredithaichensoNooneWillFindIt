using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    private Item item;

    void Awake()
    {
        item = target as Item;

        if (item == null)
        {
            Debug.LogError("target could not be cast to Item");
            return;
        }
    }

    public override void OnInspectorGUI()
    {
        ItemEditor.DrawInspecturGUI(item);
    }

    public static void DrawInspecturGUI(Item item)
    {
        var changesMade = false;

        GUILayout.Label("General", EditorStyles.boldLabel);
        var name = EditorGUILayout.TextField("Name", item.ItemName);
        if (name != item.ItemName)
        {
            changesMade = true;
            item.ItemName = name;
        }

        EditorGUILayout.LabelField("Description");
        var description = EditorGUILayout.TextArea(item.Description);
        if (description != item.Description)
        {
            changesMade = true;
            item.Description = description;
        }

        GUILayout.Space(5f);
        GUILayout.Label("Appearance", EditorStyles.boldLabel);
        var icon = (Sprite)EditorGUILayout.ObjectField("Icon", item.Icon, typeof(Sprite), true);
        if (icon != item.Icon)
        {
            changesMade = true;
            item.Icon = icon;
        }

        var mesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Mesh", item.MeshRenderer, typeof(SkinnedMeshRenderer), true);
        if (mesh != item.MeshRenderer)
        {
            changesMade = true;
            item.MeshRenderer = mesh;
        }

        if (changesMade)
            EditorUtility.SetDirty(item);
    }
}
