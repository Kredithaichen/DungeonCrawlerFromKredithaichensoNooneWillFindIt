using System.Collections;
using System.Collections.Generic;
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
        GUILayout.Label("General", EditorStyles.boldLabel);
        item.ItemName = EditorGUILayout.TextField("Name", item.ItemName);
        EditorGUILayout.LabelField("Description");
        item.Description = EditorGUILayout.TextArea(item.Description);

        GUILayout.Space(5f);
        GUILayout.Label("Appearance", EditorStyles.boldLabel);
        item.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", item.Icon, typeof(Sprite), true);
        item.MeshRenderer = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Mesh", item.MeshRenderer, typeof(SkinnedMeshRenderer), true);
    }
}
