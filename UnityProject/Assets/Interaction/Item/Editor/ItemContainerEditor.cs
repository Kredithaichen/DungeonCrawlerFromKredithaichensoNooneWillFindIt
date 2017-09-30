﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemContainer))]
public class ItemContainerEditor : Editor
{
    private ItemContainer itemContainer;

    void Awake()
    {
        itemContainer = target as ItemContainer;

        if (itemContainer == null)
        {
            Debug.LogError("target could not be cast to ItemContainer");
            return;
        }

        if (!itemContainer.Initialized)
            itemContainer.Initialize();
    }

    public override void OnInspectorGUI()
    {
        var changesMade = false;
        GUILayout.Label("General", EditorStyles.boldLabel);
        var name = EditorGUILayout.TextField("Container Name", itemContainer.ContainerName);

        if (name != itemContainer.ContainerName)
        {
            changesMade = true;
            itemContainer.ContainerName = name;
        }

        if (changesMade)
            EditorUtility.SetDirty(itemContainer);
    }
}
