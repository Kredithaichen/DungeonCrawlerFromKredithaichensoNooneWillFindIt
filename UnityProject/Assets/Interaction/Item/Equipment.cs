using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlots
{
    Head,
    Neck,
    Torso,
    Arms,
    Hands,
    Legs,
    Feet,
    Waist,
    Shield,
    Lamp,
    Weapon
}

public enum BodyCoverArea
{
    Legs,
    Arms,
    Torso
}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Interactable/Equipment")]
public class Equipment : Item
{
    public const int NumberOfBodyAreas = 3;

    [SerializeField]
    private EquipmentSlots targetSlot;
    [SerializeField]
    private bool[] coveredBodyAreas = new bool[3];

    [SerializeField]
    private List<PermanentStatEffect> statEffects = new List<PermanentStatEffect>();

    public EquipmentSlots TargetSlot
    {
        get { return targetSlot; }
        set { targetSlot = value; }
    }

    public bool[] CoveredBodyAreas
    {
        get { return coveredBodyAreas; }
        set { coveredBodyAreas = value; }
    }

    public List<PermanentStatEffect> StatEffects
    {
        get { return statEffects; }
        set { statEffects = value; }
    }

    public void Equip(Character character)
    {
        foreach (var effect in statEffects)
            character.AddStatEffect(effect, this);
    }

    public void Unequip(Character character)
    {
        character.RemoveAllStatEffects(this);
    }
}
