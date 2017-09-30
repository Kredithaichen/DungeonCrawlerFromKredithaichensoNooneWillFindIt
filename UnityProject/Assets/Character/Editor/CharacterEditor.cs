using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    private Character character;

    void Awake()
    {
        character = target as Character;

        if (character == null)
        {
            Debug.LogError("target could not be cast to Character");
            return;
        }

        if (!character.Initialized)
            character.Initialize();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Equipment Options", EditorStyles.boldLabel);
        character.TargetMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Target Mesh", character.TargetMesh, typeof(SkinnedMeshRenderer), true);
    }
}
