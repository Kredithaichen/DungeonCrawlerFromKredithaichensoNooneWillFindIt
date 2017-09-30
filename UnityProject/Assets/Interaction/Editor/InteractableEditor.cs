using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor
{
    private Interactable interactable;

    void Awake()
    {
        interactable = target as Interactable;

        if (interactable == null)
        {
            Debug.LogError("target could not be cast to Interactable");
            return;
        }

        if (!interactable.Initialized)
            interactable.Initialize();
    }

    public override void OnInspectorGUI()
    {
        var changesMade = false;

        var radius = EditorGUILayout.FloatField("Interaction Radius", interactable.InteractionRadius);

        if (radius >= 0 && Mathf.Abs(radius - interactable.InteractionRadius) > 0.01f)
        {
            interactable.InteractionRadius = radius;
            changesMade = true;
        }

        radius = EditorGUILayout.FloatField("Notice Radius", interactable.NoticeRadius);

        if (radius >= 0 && Mathf.Abs(radius - interactable.NoticeRadius) > 0.01f)
        {
            interactable.NoticeRadius = radius;
            changesMade = true;
        }

        if (changesMade)
            SceneView.RepaintAll();
    }
}
