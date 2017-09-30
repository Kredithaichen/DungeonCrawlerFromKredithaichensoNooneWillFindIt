using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponSlot
{
    Primary,
    Secondary
}

public class EquipmentLoadout : EditableMonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Equipment[] equippedItems;
    [SerializeField]
    private SkinnedMeshRenderer[] equippedMeshRenderers;
    [SerializeField]
    private SkinnedMeshRenderer targetMesh;

    [SerializeField]
    private int equipmentSlotLength;
    [SerializeField]
    private int primaryWeaponSlot;
    [SerializeField]
    private int secondaryWeaponSlot;
    [SerializeField]
    private WeaponSlot activeWeaponSlot;
    [SerializeField]
    private int nonWeaponSlots;

    public Equipment[] EquippedItems
    {
        get { return equippedItems; }
        set { equippedItems = value; }
    }

    public SkinnedMeshRenderer[] EquippedMeshRenderers
    {
        get { return equippedMeshRenderers; }
        set { equippedMeshRenderers = value; }
    }

    public SkinnedMeshRenderer TargetMesh
    {
        get { return targetMesh; }
        set { targetMesh = value; }
    }

    public WeaponSlot ActiveWeaponSlot
    {
        get { return activeWeaponSlot; }
        set
        {
            if (value == activeWeaponSlot)
                return;

            activeWeaponSlot = value;

            if (value == WeaponSlot.Primary)
                ActivatePrimaryWeapon();
            else
                ActivateSecondaryWeapon();
        }
    }

    public int NonWeaponSlots
    {
        get { return nonWeaponSlots; }
        set { nonWeaponSlots = value; }
    }

    public int PrimaryWeaponSlot
    {
        get { return primaryWeaponSlot; }
        set { primaryWeaponSlot = value; }
    }

    public int SecondaryWeaponSlot
    {
        get { return secondaryWeaponSlot; }
        set { secondaryWeaponSlot = value; }
    }

    [ContextMenu("Initialize")]
    public override void Initialize()
    {
        character = GetComponent<Character>();

        equipmentSlotLength = Enum.GetNames(typeof(EquipmentSlots)).Length + 1;
        primaryWeaponSlot = equipmentSlotLength - 2;
        secondaryWeaponSlot = equipmentSlotLength - 1;
        nonWeaponSlots = equipmentSlotLength - 2;

        equippedItems = new Equipment[equipmentSlotLength];
        equippedMeshRenderers = new SkinnedMeshRenderer[equipmentSlotLength];

        base.Initialize();
    }

    public void EquipItem(Equipment equipment)
    {
        if (equipment == null)
            return;

        if (equipment.TargetSlot == EquipmentSlots.Weapon)
            EquipWeapon(equipment);
        else
            EquipRegularItem(equipment);
    }

    private void EquipRegularItem(Equipment equipment)
    {
        var oldItem = equippedItems[(int)equipment.TargetSlot];
        if (oldItem != null)
            UnequipRegularItem(oldItem);

        equippedItems[(int)equipment.TargetSlot] = equipment;
        equipment.Equip(character);
        character.Inventory.RemoveItemFromInventory(equipment);

        // spawn mesh on character
        equippedMeshRenderers[(int)equipment.TargetSlot] = SpawnMesh(equipment);

        // adjust blend shapes
        ActivateBlendShapes(equipment);
    }

    private void EquipWeapon(Equipment weapon)
    {
        if (equippedItems[primaryWeaponSlot] != null && equippedItems[secondaryWeaponSlot] != null)
        {
            Debug.Log("two weapons");
            // move second into inventory
            var secondaryWeapon = equippedItems[secondaryWeaponSlot];
            character.Inventory.AddItemToInventory(secondaryWeapon);

            // move first into second
            var primaryWeapon = equippedItems[primaryWeaponSlot];
            equippedItems[secondaryWeaponSlot] = primaryWeapon;

            // put weapon in first
            equippedItems[primaryWeaponSlot] = weapon;
        }
        else if (equippedItems[primaryWeaponSlot] == null && equippedItems[secondaryWeaponSlot] != null)
        {
            Debug.Log("weapon in slot 2");

            // put weapon in first
            equippedItems[primaryWeaponSlot] = weapon;
        }
        else if (equippedItems[primaryWeaponSlot] != null && equippedItems[secondaryWeaponSlot] == null)
        {
            Debug.Log("weapon in slot 1");

            // move first into second
            var primaryWeapon = equippedItems[primaryWeaponSlot];
            equippedItems[secondaryWeaponSlot] = primaryWeapon;

            // put weapon in first
            equippedItems[primaryWeaponSlot] = weapon;
        }
        else
            // put weapon in first
            equippedItems[primaryWeaponSlot] = weapon;

        character.Inventory.RemoveItemFromInventory(weapon);

        //if (activeWeaponSlot == WeaponSlot.Primary)
        //    ActivatePrimaryWeapon();
        //else
        //    ActivateSecondaryWeapon();
    }

    private SkinnedMeshRenderer SpawnMesh(Equipment equipment)
    {
        var mesh = Instantiate(equipment.MeshRenderer);
        mesh.transform.SetParent(targetMesh.transform);

        mesh.bones = targetMesh.bones;
        mesh.rootBone = targetMesh.rootBone;

        return mesh;
    }

    private void ActivateBlendShapes(Equipment equipment)
    {
        for (int i = 0; i < equipment.CoveredBodyAreas.Length; i++)
            targetMesh.SetBlendShapeWeight(i, equipment.CoveredBodyAreas[i] ? 100f : 0f);
    }

    private void DeactivateBlendShapes(Equipment equipment)
    {
        for (int i = 0; i < equipment.CoveredBodyAreas.Length; i++)
            targetMesh.SetBlendShapeWeight(i, 0f);
    }

    public void UnequipItem(Equipment equipment)
    {
        Debug.Log("unequip item " + equipment);
        if (equipment == null)
            return;

        if (equipment.TargetSlot == EquipmentSlots.Weapon)
            UnequipWeapon(equipment);
        else
            UnequipRegularItem(equipment);
    }

    private void UnequipRegularItem(Equipment equipment)
    {
        equippedItems[(int)equipment.TargetSlot] = null;
        equipment.Unequip(character);
        character.Inventory.AddItemToInventory(equipment);

        if (Application.isPlaying)
            Destroy(equippedMeshRenderers[(int)equipment.TargetSlot].gameObject);
        else
            DestroyImmediate(equippedMeshRenderers[(int)equipment.TargetSlot].gameObject);

        // adjust blend shapes
        DeactivateBlendShapes(equipment);
    }

    private void UnequipWeapon(Equipment weapon)
    {
        var index = weapon == equippedItems[primaryWeaponSlot] ? primaryWeaponSlot : secondaryWeaponSlot;

        RemoveWeapon(index);
    }

    private void SpawnWeapon(int slotIndex)
    {
        var weapon = equippedItems[slotIndex];

        if (weapon != null)
        {
            weapon.Equip(character);

            if (equippedMeshRenderers[slotIndex] != null)
            {
                if (Application.isPlaying)
                    Destroy(equippedMeshRenderers[slotIndex].gameObject);
                else
                    DestroyImmediate(equippedMeshRenderers[slotIndex].gameObject);
            }

            equippedMeshRenderers[slotIndex] = SpawnMesh(weapon);
            ActivateBlendShapes(weapon);
        }
    }

    private void RemoveWeapon(int slotIndex)
    {
        if ((slotIndex == primaryWeaponSlot && activeWeaponSlot == WeaponSlot.Primary) ||
            (slotIndex == secondaryWeaponSlot && activeWeaponSlot == WeaponSlot.Secondary))
        {
            var weapon = equippedItems[slotIndex];

            if (weapon != null)
            {
                weapon.Unequip(character);
                DeactivateBlendShapes(weapon);

                if (!Application.isPlaying)
                    DestroyImmediate(equippedMeshRenderers[slotIndex].gameObject);
                else
                    Destroy(equippedMeshRenderers[slotIndex].gameObject);
            }
        }

        character.Inventory.AddItemToInventory(equippedItems[slotIndex]);
        equippedItems[slotIndex] = null;
    }

    private void ActivatePrimaryWeapon()
    {
        RemoveWeapon(secondaryWeaponSlot);
        SpawnWeapon(primaryWeaponSlot);
    }

    private void ActivateSecondaryWeapon()
    {
        RemoveWeapon(primaryWeaponSlot);
        SpawnWeapon(secondaryWeaponSlot);
    }

    public void UpdateWeaponMeshes()
    {
        if (activeWeaponSlot == WeaponSlot.Primary)
        {
            if (equippedMeshRenderers[primaryWeaponSlot] != null)
            {
                if (!Application.isPlaying)
                    DestroyImmediate(equippedMeshRenderers[primaryWeaponSlot].gameObject);
                else
                    Destroy(equippedMeshRenderers[primaryWeaponSlot].gameObject);
            }

            if (equippedItems[primaryWeaponSlot] != null)
            {
                
            }
        }
    }
}
