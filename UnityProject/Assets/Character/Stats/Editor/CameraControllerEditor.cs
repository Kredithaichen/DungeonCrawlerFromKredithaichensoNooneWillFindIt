using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var camera = target as CameraController;

        camera.PlayerCamera = (Camera)EditorGUILayout.ObjectField("Used Camera", camera.PlayerCamera, typeof(Camera), true);

        GUILayout.Space(5f);
        GUILayout.Label("General", EditorStyles.boldLabel);
        camera.CameraTurnSpeed = EditorGUILayout.FloatField("Turn Speed", camera.CameraTurnSpeed);
        camera.CameraOffset = EditorGUILayout.Vector3Field("Offset", camera.CameraOffset);

        GUILayout.Space(5f);
        GUILayout.Label("Zoom", EditorStyles.boldLabel);
        camera.CurrentCameraZoom = EditorGUILayout.FloatField("Current Zoom", camera.CurrentCameraZoom);
        camera.CameraZoomSpeed = EditorGUILayout.FloatField("Zoom Speed", camera.CameraZoomSpeed);
        camera.CameraMinZoom = EditorGUILayout.FloatField("Minimum Zoom", camera.CameraMinZoom);
        camera.CameraMaxZoom = EditorGUILayout.FloatField("Maximum Zoom", camera.CameraMaxZoom);
    }
}
