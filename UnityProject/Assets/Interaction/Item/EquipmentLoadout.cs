using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    [SerializeField]
    private Consumable[] beltSlots;

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

            if (value == WeaponSlot.Primary)
            {
                DestroyWeapon(secondaryWeaponSlot);
                activeWeaponSlot = value;
                SpawnWeapon(primaryWeaponSlot);
            }
            else
            {
                DestroyWeapon(primaryWeaponSlot);
                activeWeaponSlot = value;
                SpawnWeapon(secondaryWeaponSlot);
            }

            activeWeaponSlot = value;
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

    public Consumable[] BeltSlots
    {
        get { return beltSlots; }
        set { beltSlots = value; }
    }

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
        else if (equipment.TargetSlot == EquipmentSlots.Waist)
            EquipBelt(equipment);
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
    }

    private void EquipBelt(Equipment equipment)
    {
        var belt = equipment as Belt;

        if (belt == null)
            return;

        if (beltSlots == null)
            beltSlots = new Consumable[belt.BeltSlots];
        else
        {
            var newBelt = new Consumable[belt.BeltSlots];

            for (int i = 0; i < Mathf.Min(belt.BeltSlots, beltSlots.Length); i++)
                newBelt[i] = beltSlots[i];

            beltSlots = newBelt;
        }

        var oldBelt = equippedItems[(int)equipment.TargetSlot];
        if (oldBelt != null)
            UnequipRegularItem(oldBelt);

        equippedItems[(int)equipment.TargetSlot] = equipment;
        equipment.Equip(character);
        character.Inventory.RemoveItemFromInventory(equipment);

        // spawn mesh on character
        equippedMeshRenderers[(int)equipment.TargetSlot] = SpawnMesh(equipment);
    }

    private void EquipWeapon(Equipment weapon)
    {
        if (equippedItems[primaryWeaponSlot] != null && equippedItems[secondaryWeaponSlot] != null)
        {
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
            // put weapon in first
            equippedItems[primaryWeaponSlot] = weapon;
        }
        else if (equippedItems[primaryWeaponSlot] != null && equippedItems[secondaryWeaponSlot] == null)
        {
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

        if (activeWeaponSlot == WeaponSlot.Primary)
            ActivatePrimaryWeapon();
        else
            ActivateSecondaryWeapon();
    }

    private SkinnedMeshRenderer SpawnMesh(Equipment equipment)
    {
        var mesh = Instantiate(equipment.MeshRenderer);
        mesh.transform.SetParent(targetMesh.transform);

        mesh.bones = targetMesh.bones;
        mesh.rootBone = targetMesh.rootBone;

        ActivateBlendShapes(equipment);

        return mesh;
    }

    private void DestroyMesh(Equipment equipment)
    {
        SkinnedMeshRenderer oldEquipment;

        if (equipment.TargetSlot != EquipmentSlots.Weapon)
            oldEquipment = equippedMeshRenderers[(int) equipment.TargetSlot];
        else
        {
            if (activeWeaponSlot == WeaponSlot.Primary)
                oldEquipment = equippedMeshRenderers[primaryWeaponSlot];
            else
                oldEquipment = equippedMeshRenderers[secondaryWeaponSlot];
        }

        if (oldEquipment != null)
        {
            if (Application.isPlaying)
                Destroy(oldEquipment.gameObject);
            else
                DestroyImmediate(oldEquipment.gameObject);
        }

        DeactivateBlendShapes(equipment);
    }

    private void ActivateBlendShapes(Equipment equipment)
    {
        for (int i = 0; i < equipment.CoveredBodyAreas.Length; i++)
            targetMesh.SetBlendShapeWeight(i, equipment.CoveredBodyAreas[i] ? 100f : targetMesh.GetBlendShapeWeight(i));
    }

    private void DeactivateBlendShapes(Equipment equipment)
    {
        for (int i = 0; i < equipment.CoveredBodyAreas.Length; i++)
            targetMesh.SetBlendShapeWeight(i, equipment.CoveredBodyAreas[i] ? 0f : targetMesh.GetBlendShapeWeight(i));
    }

    public void UnequipItem(Equipment equipment)
    {
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

        DestroyMesh(equipment);
    }

    private void UnequipWeapon(Equipment weapon)
    {
        var index = weapon == equippedItems[primaryWeaponSlot] ? primaryWeaponSlot : secondaryWeaponSlot;
        RemoveWeapon(index);
    }

    private void SpawnWeapon(int slotIndex)
    {
        if ((slotIndex == primaryWeaponSlot && activeWeaponSlot == WeaponSlot.Primary) ||
            (slotIndex == secondaryWeaponSlot && activeWeaponSlot == WeaponSlot.Secondary))
        {
            var weapon = equippedItems[slotIndex];

            if (weapon != null)
            {
                weapon.Equip(character);
                DestroyMesh(weapon);

                equippedMeshRenderers[slotIndex] = SpawnMesh(weapon);
            }
        }
    }

    private void DestroyWeapon(int slotIndex)
    {
        if ((slotIndex == primaryWeaponSlot && activeWeaponSlot == WeaponSlot.Primary) ||
            (slotIndex == secondaryWeaponSlot && activeWeaponSlot == WeaponSlot.Secondary))
        {
            var weapon = equippedItems[slotIndex];

            if (weapon != null)
            {
                weapon.Unequip(character);
                DestroyMesh(weapon);
            }
        }
    }

    private void RemoveWeapon(int slotIndex)
    {
        DestroyWeapon(slotIndex);

        character.Inventory.AddItemToInventory(equippedItems[slotIndex]);
        equippedItems[slotIndex] = null;
    }

    private void ActivatePrimaryWeapon()
    {
        DestroyWeapon(secondaryWeaponSlot);
        SpawnWeapon(primaryWeaponSlot);
    }

    private void ActivateSecondaryWeapon()
    {
        DestroyWeapon(primaryWeaponSlot);
        SpawnWeapon(secondaryWeaponSlot);
    }
}
