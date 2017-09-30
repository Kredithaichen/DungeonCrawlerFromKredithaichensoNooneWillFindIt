using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterStats))]
public class CharacterStatsEditor : Editor
{
    private Vector2 statsScrollPosition;
    private CharacterStats characterStats;

    void Awake()
    {
        characterStats = target as CharacterStats;

        if (characterStats == null)
        {
            Debug.LogError("target could not be cast to CharacterStats");
            return;
        }

        if (!characterStats.Initialized)
            characterStats.Initialize();
    }

    public override void OnInspectorGUI()
    {
        var stats = target as CharacterStats;

        GUILayout.Label("Health", EditorStyles.boldLabel);
        stats.Alive = EditorGUILayout.Toggle("Is Alive?", stats.Alive);
        stats.BaseHealth = EditorGUILayout.FloatField("Base Health", stats.BaseHealth);
        stats.CurrentHealth = EditorGUILayout.FloatField("Current Health", stats.CurrentHealth);

        GUILayout.Space(5f);
        GUILayout.Label("Movement", EditorStyles.boldLabel);
        stats.BaseMoveSpeed = EditorGUILayout.FloatField("Base Movement Speed", stats.BaseMoveSpeed);
        stats.CurrentMoveSpeed = EditorGUILayout.FloatField("Current Movement Speed", stats.CurrentMoveSpeed);

        GUILayout.Space(5f);
        stats.FullDashSpeed = EditorGUILayout.FloatField("Full Dash Speed", stats.FullDashSpeed);
        stats.CurrentDashSpeed = EditorGUILayout.FloatField("Current Dash Speed", stats.CurrentDashSpeed);
        stats.DashReduction = EditorGUILayout.FloatField("Dash Reduction", stats.DashReduction);

        GUILayout.Space(5f);
        GUILayout.Label("Damage", EditorStyles.boldLabel);
        stats.BaseDamage = EditorGUILayout.FloatField("Base Damage", stats.BaseDamage);
        stats.WeaponDamage = EditorGUILayout.FloatField("Weapon Damage", stats.WeaponDamage);

        GUILayout.Space(5f);
        GUILayout.Label("Defense", EditorStyles.boldLabel);
        stats.Defense = EditorGUILayout.FloatField("Defense", stats.Defense);

        GUILayout.Space(5f);
        GUILayout.Label("Status Effects", EditorStyles.boldLabel);

        if (stats.StatEffects.Count > 0)
        {
            statsScrollPosition = EditorGUILayout.BeginScrollView(statsScrollPosition, GUILayout.MinHeight(100f));

            foreach (var effect in stats.StatEffects)
            {
                effect.StatName = EditorGUILayout.TextField("Name", effect.StatName);
                effect.Remove = EditorGUILayout.Toggle("Remove?", effect.Remove);
                effect.Type = (StatEffectType)EditorGUILayout.EnumPopup("Type", effect.Type);
                effect.Value = EditorGUILayout.FloatField("Value", effect.Value);
            }

            EditorGUILayout.EndScrollView();
        }
        else
            GUILayout.Label("No Status Effects", EditorStyles.centeredGreyMiniLabel);

        //if (GUILayout.Button("Add Status Effect"))

    }
}
