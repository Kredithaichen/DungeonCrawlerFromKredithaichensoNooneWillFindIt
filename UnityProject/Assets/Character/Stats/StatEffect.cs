using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatEffectType
{
    Health,
    Movement,
    Damage,
    ArmorPiercing,
    Defense,
    Reach
}

[System.Serializable]
public class StatEffect : ScriptableObject
{
    [SerializeField]
    protected string statName;
    [SerializeField]
    protected StatEffectType type;
    [SerializeField]
    protected float value;
    [SerializeField]
    protected bool remove;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected IStatSource source;

    public string StatName
    {
        get { return statName; }
        set { statName = value; }
    }

    public StatEffectType Type
    {
        get { return type; }
        set { type = value; }
    }

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public bool Remove
    {
        get { return remove; }
        set { remove = value; }
    }

    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public IStatSource StatSource
    {
        get { return source; }
        set { source = value; }
    }

    public virtual void Update()
    { }
}
