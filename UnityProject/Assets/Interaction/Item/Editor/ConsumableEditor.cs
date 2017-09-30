using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Consumable))]
public class ConsumableEditor : Editor
{
    private Consumable consumable;

    void Awake()
    {
        consumable = target as Consumable;

        if (consumable == null)
        {
            Debug.LogError("target could not be cast to Consumable");
            return;
        }
    }

    public override void OnInspectorGUI()
    {
        ItemEditor.DrawInspecturGUI(consumable);
        ConsumableEditor.DrawInspectorGUI(consumable);
    }

    public static void DrawInspectorGUI(Consumable consumable)
    {
        var changesMade = false;

        GUILayout.Space(10f);
        GUILayout.Label("Status Effects", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < consumable.StatEffects.Count; i++)
        {
            var effect = (TemporaryStatEffect)EditorGUILayout.ObjectField((i + 1).ToString(), consumable.StatEffects[i], typeof(TemporaryStatEffect), true);

            if (effect != consumable.StatEffects[i])
            {
                changesMade = true;
                consumable.StatEffects[i] = effect;
            }
        }

        EditorGUILayout.EndVertical();

        GUILayout.Space(10f);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("New"))
            consumable.StatEffects.Add(null);

        if (GUILayout.Button("Remove"))
            consumable.StatEffects.RemoveAt(consumable.StatEffects.Count - 1);

        EditorGUILayout.EndHorizontal();

        if (changesMade)
            EditorUtility.SetDirty(consumable);
    }
}
