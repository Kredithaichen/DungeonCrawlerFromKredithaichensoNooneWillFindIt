using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Interactable/Consumable")]
public class Consumable : Item
{
    [SerializeField]
    private List<TemporaryStatEffect> statEffects = new List<TemporaryStatEffect>();

    public List<TemporaryStatEffect> StatEffects
    {
        get { return statEffects; }
        set { statEffects = value; }
    }

    public void Consume(Character character)
    {
        foreach (var effect in statEffects)
            character.AddStatEffect(effect, this);
    }
}
