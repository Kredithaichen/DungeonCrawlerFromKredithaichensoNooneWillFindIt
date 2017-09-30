using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EquipmentLoadout))]

public class EquipmentLoadoutEditor : Editor
{
    private EquipmentLoadout equipmentLoadout;

    void Awake()
    {
        equipmentLoadout = target as EquipmentLoadout;

        if (equipmentLoadout == null)
        {
            Debug.LogError("target could not be cast to EquipmentLoadout");
            return;
        }

        if (!equipmentLoadout.Initialized)
            equipmentLoadout.Initialize();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Loadout", EditorStyles.boldLabel);

        var saveSpace = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 70f;

        for (int i = 0; i < equipmentLoadout.NonWeaponSlots; i++)
        {
            var equippedItem = equipmentLoadout.EquippedItems[i];

            EditorGUILayout.BeginHorizontal();
            var newEquipment = (Equipment)EditorGUILayout.ObjectField(((EquipmentSlots)i).ToString(), equippedItem, typeof(Equipment), true);
            if (GUILayout.Button("Unequip", EditorStyles.miniButton, GUILayout.Width(70f)))
                equipmentLoadout.UnequipItem(equippedItem);
            EditorGUILayout.EndHorizontal();

            if (newEquipment != equippedItem)
                equipmentLoadout.EquipItem(newEquipment);
        }

        EditorGUIUtility.labelWidth = saveSpace;

        GUILayout.Space(10f);
        GUILayout.Label("Weapons", EditorStyles.boldLabel);
        equipmentLoadout.ActiveWeaponSlot = (WeaponSlot)EditorGUILayout.EnumPopup("Active Weapon", equipmentLoadout.ActiveWeaponSlot);

        EditorGUILayout.BeginHorizontal();

        equipmentLoadout.EquippedItems[equipmentLoadout.PrimaryWeaponSlot] = (Equipment)EditorGUILayout.ObjectField("Primary Weapon", equipmentLoadout.EquippedItems[equipmentLoadout.PrimaryWeaponSlot], typeof(Equipment), false);

        if (GUILayout.Button("Remove", EditorStyles.miniButton, GUILayout.Width(70f)))
            equipmentLoadout.UnequipItem(equipmentLoadout.EquippedItems[equipmentLoadout.PrimaryWeaponSlot]);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        equipmentLoadout.EquippedItems[equipmentLoadout.SecondaryWeaponSlot] = (Equipment)EditorGUILayout.ObjectField("Secondary Weapon", equipmentLoadout.EquippedItems[equipmentLoadout.SecondaryWeaponSlot], typeof(Equipment), false);

        if (GUILayout.Button("Remove", EditorStyles.miniButton, GUILayout.Width(70f)))
            equipmentLoadout.UnequipItem(equipmentLoadout.EquippedItems[equipmentLoadout.SecondaryWeaponSlot]);

        EditorGUILayout.EndHorizontal(); 
    }
}
