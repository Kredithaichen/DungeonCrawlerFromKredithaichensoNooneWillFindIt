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
        GUILayout.Space(10f);
        GUILayout.Label("Status Effects", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < consumable.StatEffects.Count; i++)
            consumable.StatEffects[i] =
                (TemporaryStatEffect)EditorGUILayout.ObjectField((i + 1).ToString(), consumable.StatEffects[i], typeof(TemporaryStatEffect), true);

        EditorGUILayout.EndVertical();

        GUILayout.Space(10f);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("New"))
            consumable.StatEffects.Add(null);

        if (GUILayout.Button("Remove"))
            consumable.StatEffects.RemoveAt(consumable.StatEffects.Count - 1);

        EditorGUILayout.EndHorizontal();
    }
}
