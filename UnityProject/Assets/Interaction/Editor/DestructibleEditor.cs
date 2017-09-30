using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Destructible))]
public class DestructibleEditor : Editor
{
    private Destructible destructible;
    private float timeAfterTest;
    private GameObject testObject;

    void Awake()
    {
        destructible = target as Destructible;

        if (destructible == null)
        {
            Debug.LogError("target could not be cast to Destructible");
            return;
        }

        if (!destructible.Initialized)
            destructible.Initialize();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Destruction Options", EditorStyles.boldLabel);
        destructible.DestroyOnCollision = EditorGUILayout.Toggle("Destroy on Collision", destructible.DestroyOnCollision);

        if (destructible.DestroyOnCollision)
            destructible.DestructionThreshold = EditorGUILayout.FloatField("Destruction Threshold",
                destructible.DestructionThreshold);
        else
        {
            GUI.enabled = false;
            EditorGUILayout.FloatField("Destruction Threshold", destructible.DestructionThreshold);
            GUI.enabled = true;
        }

        destructible.DestroyedVersion = (GameObject)EditorGUILayout.ObjectField("Destroyed Version", destructible.DestroyedVersion, typeof(GameObject), true);

        GUILayout.Space(10f);
        GUILayout.Label("In-Editor Tests", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Test Destruction"))
            {
                destructible.gameObject.SetActive(false);

                var obj = Instantiate(destructible.gameObject, destructible.transform.position,
                    destructible.transform.rotation);

                testObject = obj.GetComponent<Destructible>().DestroyObject();
            }

            if (GUILayout.Button("Clear"))
            {
                if (testObject != null)
                    Destroy(testObject);

                destructible.gameObject.SetActive(true);
            }

            EditorGUILayout.EndHorizontal();
        }
        else
            GUILayout.Label("Only available in play-mode", EditorStyles.centeredGreyMiniLabel);

        GUILayout.Space(10f);
        GUILayout.Label("Destroy", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Destroy"))
                destructible.DestroyObject();
        }
        else
            GUILayout.Label("Only available in play-mode", EditorStyles.centeredGreyMiniLabel);
    }
}
