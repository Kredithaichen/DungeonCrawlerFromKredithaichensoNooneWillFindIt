using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private float cameraTurnSpeed = 50f;
    [SerializeField]
    private Vector3 cameraOffset = new Vector3(0, -2.5f, 5f);

    [SerializeField]
    private float currentCameraZoom = 1f;
    [SerializeField]
    private float cameraZoomSpeed = 4f;
    [SerializeField]
    private float cameraMinZoom = 1f;
    [SerializeField]
    private float cameraMaxZoom = 5f;

    public float CameraTurnSpeed
    {
        get { return cameraTurnSpeed; }
        set { cameraTurnSpeed = value; }
    }

    public Vector3 CameraOffset
    {
        get { return cameraOffset; }
        set { cameraOffset = value; }
    }

    public float CurrentCameraZoom
    {
        get { return currentCameraZoom; }
        set { currentCameraZoom = value; }
    }

    public float CameraZoomSpeed
    {
        get { return cameraZoomSpeed; }
        set { cameraZoomSpeed = value; }
    }

    public float CameraMinZoom
    {
        get { return cameraMinZoom; }
        set { cameraMinZoom = value; }
    }

    public float CameraMaxZoom
    {
        get { return cameraMaxZoom; }
        set { cameraMaxZoom = value; }
    }

    public Camera PlayerCamera
    {
        get { return playerCamera; }
        set { playerCamera = value; }
    }

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    public void UpdateCameraPosition(Transform playerTransform)
    {
        // zoom camera
        currentCameraZoom -= Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed;
        currentCameraZoom = Mathf.Clamp(currentCameraZoom, cameraMinZoom, cameraMaxZoom);

        // rotate camera
        var desiredAngle = playerTransform.eulerAngles.y;
        var rotation = Quaternion.Euler(0, desiredAngle, 0);

        playerCamera.transform.position = playerTransform.position - (rotation * cameraOffset * currentCameraZoom);
        playerCamera.transform.LookAt(playerTransform.position + Vector3.up);
    }
}
