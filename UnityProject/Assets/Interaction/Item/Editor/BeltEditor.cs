using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Belt))]
public class BeltEditor : Editor
{
    private Belt belt;

    void Awake()
    {
        belt = target as Belt;

        if (belt == null)
        {
            Debug.LogError("target could not be cast to Belt");
            return;
        }

        if (!belt.Initialized)
            belt.Initialize();
    }

    public override void OnInspectorGUI()
    {
        ItemEditor.DrawInspecturGUI(belt);
        EquipmentEditor.DrawInspectorGUI(belt, true);

        GUILayout.Space(10f);
        DrawInspectorGUI(belt);
    }

    public static void DrawInspectorGUI(Belt belt)
    {
        var changesMade = false;
        GUILayout.Label("Belt Options", EditorStyles.boldLabel);

        var slotNumber = EditorGUILayout.IntField("Slot Number", belt.BeltSlots);

        if (slotNumber != belt.BeltSlots && slotNumber > 0 && slotNumber <= Belt.MaximumSlotNumber)
        {
            changesMade = true;
            belt.BeltSlots = slotNumber;
        }

        if (changesMade)
            EditorUtility.SetDirty(belt);
    }
}
