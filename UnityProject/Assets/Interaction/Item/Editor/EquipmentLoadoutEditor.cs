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
        var changesMade = false;

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
            {
                changesMade = true;
                equipmentLoadout.EquipItem(newEquipment);
            }
        }

        EditorGUIUtility.labelWidth = saveSpace;

        GUILayout.Space(10f);
        GUILayout.Label("Weapons", EditorStyles.boldLabel);

        var slot = (WeaponSlot)EditorGUILayout.EnumPopup("Active Weapon", equipmentLoadout.ActiveWeaponSlot);
        if (slot != equipmentLoadout.ActiveWeaponSlot)
        {
            changesMade = true;
            equipmentLoadout.ActiveWeaponSlot = slot;
        }

        EditorGUILayout.BeginHorizontal();

        var primaryWeapon = (Equipment)EditorGUILayout.ObjectField("Primary Weapon", equipmentLoadout.EquippedItems[equipmentLoadout.PrimaryWeaponSlot], typeof(Equipment), false);
        if (primaryWeapon != equipmentLoadout.EquippedItems[equipmentLoadout.PrimaryWeaponSlot])
        {
            changesMade = true;
            equipmentLoadout.EquipItem(primaryWeapon);
        }

        if (GUILayout.Button("Remove", EditorStyles.miniButton, GUILayout.Width(70f)))
            equipmentLoadout.UnequipItem(equipmentLoadout.EquippedItems[equipmentLoadout.PrimaryWeaponSlot]);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        var secondaryWeapon = (Equipment)EditorGUILayout.ObjectField("Secondary Weapon", equipmentLoadout.EquippedItems[equipmentLoadout.SecondaryWeaponSlot], typeof(Equipment), false);
        if (secondaryWeapon != equipmentLoadout.EquippedItems[equipmentLoadout.SecondaryWeaponSlot])
        {
            changesMade = true;
            equipmentLoadout.EquipItem(secondaryWeapon);
        }

        if (GUILayout.Button("Remove", EditorStyles.miniButton, GUILayout.Width(70f)))
            equipmentLoadout.UnequipItem(equipmentLoadout.EquippedItems[equipmentLoadout.SecondaryWeaponSlot]);

        EditorGUILayout.EndHorizontal();

        var belt = equipmentLoadout.EquippedItems[(int) EquipmentSlots.Waist] as Belt;
        if (belt != null)
        {
            GUILayout.Space(10f);
            GUILayout.Label("Belt", EditorStyles.boldLabel);

            saveSpace = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 30f;

            for (int i = 0; i < belt.BeltSlots; i++)
            {
                var equippedItem = equipmentLoadout.BeltSlots[i];

                EditorGUILayout.BeginHorizontal();
                var newConsumable = (Consumable)EditorGUILayout.ObjectField((i + 1).ToString(), equippedItem, typeof(Consumable), true);
                if (GUILayout.Button("Remove", EditorStyles.miniButton, GUILayout.Width(70f)))
                { }
                EditorGUILayout.EndHorizontal();

                if (newConsumable != equippedItem)
                {
                    changesMade = true;
                    equipmentLoadout.BeltSlots[i] = newConsumable;
                }
            }

            EditorGUIUtility.labelWidth = saveSpace;
        }
        
        if (changesMade)
            EditorUtility.SetDirty(equipmentLoadout);
    }
}
