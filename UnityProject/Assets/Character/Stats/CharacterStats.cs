using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : EditableMonoBehaviour
{
    [SerializeField]
    private float baseHealth = 100f;
    [SerializeField]
    private float currentHealth = 100f;
    [SerializeField]
    protected bool alive = true;

    [SerializeField]
    protected float baseMoveSpeed = 2f;
    [SerializeField]
    protected float currentMoveSpeed = 2f;
    [SerializeField]
    protected float fullDashSpeed = 80f;
    [SerializeField]
    protected float currentDashSpeed;
    [SerializeField]
    protected float dashReduction = 320f;

    [SerializeField]
    protected float currentDashCooldown;
    [SerializeField]
    protected float dashCooldown = 0.5f;

    [SerializeField]
    protected float baseDamage;
    [SerializeField]
    protected float weaponDamage;

    [SerializeField]
    protected float defense;

    [SerializeField]
    protected List<StatEffect> statEffects;

    public bool Alive
    {
        get { return alive; }
        set { alive = value; }
    }

    public float BaseHealth
    {
        get { return baseHealth; }
        set { baseHealth = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public float BaseMoveSpeed
    {
        get { return baseMoveSpeed; }
        set { baseMoveSpeed = value; }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set { currentMoveSpeed = value; }
    }

    public float FullDashSpeed
    {
        get { return fullDashSpeed; }
        set { fullDashSpeed = value; }
    }

    public float CurrentDashSpeed
    {
        get { return currentDashSpeed; }
        set { currentDashSpeed = value; }
    }

    public float DashReduction
    {
        get { return dashReduction; }
        set { dashReduction = value; }
    }

    public float BaseDamage
    {
        get { return baseDamage; }
        set { baseDamage = value; }
    }

    public float WeaponDamage
    {
        get { return weaponDamage; }
        set { weaponDamage = value; }
    }

    public float Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public List<StatEffect> StatEffects
    {
        get { return statEffects; }
        set { statEffects = value; }
    }

    public float CurrentDashCooldown
    {
        get { return currentDashCooldown; }
        set { currentDashCooldown = value; }
    }

    public float DashCooldown
    {
        get { return dashCooldown; }
        set { dashCooldown = value; }
    }

    public override void Initialize()
    {
        statEffects = new List<StatEffect>();

        base.Initialize();
    }

    public void UpdateStats(Character character)
    {
        if (!alive)
        {
            statEffects.Clear();
            return;
        }

        var removeStats = new List<StatEffect>();

        currentMoveSpeed = baseMoveSpeed;

        foreach (var stat in statEffects)
        {
            if (stat == null)
                continue;

            switch (stat.Type)
            {
                case StatEffectType.Movement:
                    currentMoveSpeed += stat.Value;
                    break;
                case StatEffectType.ArmorPiercing:
                    break;
                case StatEffectType.Damage:
                    break;
                case StatEffectType.Defense:
                    break;
                case StatEffectType.Health:
                    currentHealth += stat.Value;
                    if (currentHealth > baseHealth)
                        currentHealth = baseHealth;

                    if (currentHealth <= 0f)
                        character.Die();

                    break;
                case StatEffectType.Reach:
                    break;
            }

            if (stat.Remove)
                removeStats.Add(stat);

            stat.Update();
        }

        foreach (var removeStat in removeStats)
            statEffects.Remove(removeStat);
    }

    public void AddStatEffect(StatEffect effect)
    {
        statEffects.Add(effect);
    }
}
