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
        var changesMade = false;

        GUILayout.Space(10f);
        GUILayout.Label("Equipment Options", EditorStyles.boldLabel);
        equipment.TargetSlot = (EquipmentSlots)EditorGUILayout.EnumPopup("Target Body Slot", equipment.TargetSlot);

        GUILayout.Label("Covered Body Parts:");
        EditorGUI.indentLevel++;
        for (int i = 0; i < Equipment.NumberOfBodyAreas; i++)
        {
            var area = EditorGUILayout.Toggle(((BodyCoverArea)i).ToString(), equipment.CoveredBodyAreas[i]);

            if (area != equipment.CoveredBodyAreas[i])
            {
                changesMade = true;
                equipment.CoveredBodyAreas[i] = area;
            }
        }
        EditorGUI.indentLevel--;

        GUILayout.Space(10f);
        GUILayout.Label("Status Effects", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < equipment.StatEffects.Count; i++)
        {
            var effect = (PermanentStatEffect) EditorGUILayout.ObjectField((i + 1).ToString(), equipment.StatEffects[i], typeof(PermanentStatEffect), true);

            if (effect != equipment.StatEffects[i])
            {
                changesMade = true;
                equipment.StatEffects[i] = effect;
            }
        }

        EditorGUILayout.EndVertical();

        GUILayout.Space(10f);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("New"))
            equipment.StatEffects.Add(null);

        if (GUILayout.Button("Remove"))
            equipment.StatEffects.RemoveAt(equipment.StatEffects.Count - 1);

        EditorGUILayout.EndHorizontal();

        if (changesMade)
            EditorUtility.SetDirty(equipment);
    }
}
