using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Belt", menuName = "Interactable/Equipment/Belt")]
public class Belt : Equipment
{
    [SerializeField]
    private int beltSlots = 1;

    public const int MaximumSlotNumber = 10;

    public int BeltSlots
    {
        get { return beltSlots; }
        set { beltSlots = value; }
    }

    public override void Initialize()
    {
        base.Initialize();

        TargetSlot = EquipmentSlots.Waist;
    }
}
