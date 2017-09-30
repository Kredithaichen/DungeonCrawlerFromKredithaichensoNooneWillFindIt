using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Equipment))]
public class EquipmentEditor : Editor
{
    private Equipment equipment;

    void Awake()
    {
        equipment = target as Equipment;

        if (equipment == null)
        {
            Debug.LogError("target could not be cast to Equipment");
            return;
        }
    }

    public override void OnInspectorGUI()
    {
        ItemEditor.DrawInspecturGUI(equipment);
        EquipmentEditor.DrawInspectorGUI(equipment);
    }

    public static void DrawInspectorGUI(Equipment equipment)
    {
        GUILayout.Space(10f);
        GUILayout.Label("Equipment Options", EditorStyles.boldLabel);
        equipment.TargetSlot = (EquipmentSlots)EditorGUILayout.EnumPopup("Target Body Slot", equipment.TargetSlot);

        GUILayout.Label("Covered Body Parts:");
        EditorGUI.indentLevel++;
        for (int i = 0; i < Equipment.NumberOfBodyAreas; i++)
            equipment.CoveredBodyAreas[i] = EditorGUILayout.Toggle(((BodyCoverArea)i).ToString(), equipment.CoveredBodyAreas[i]);
        EditorGUI.indentLevel--;

        GUILayout.Space(10f);
        GUILayout.Label("Status Effects", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < equipment.StatEffects.Count; i++)
            equipment.StatEffects[i] =
                (PermanentStatEffect)EditorGUILayout.ObjectField((i + 1).ToString(), equipment.StatEffects[i], typeof(PermanentStatEffect), true);

        EditorGUILayout.EndVertical();

        GUILayout.Space(10f);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("New"))
            equipment.StatEffects.Add(null);

        if (GUILayout.Button("Remove"))
            equipment.StatEffects.RemoveAt(equipment.StatEffects.Count - 1);

        EditorGUILayout.EndHorizontal();
    }
}
