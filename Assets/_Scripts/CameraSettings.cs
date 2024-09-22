using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "PathCreation/CameraSettings", order = 1)]
public class CameraSettings : ScriptableObject
{
    public Vector3 positionOffset = new Vector3(0, 2.5f, -3);  // Offset for the camera
    public Vector3 rotationOffset = new Vector3(2, 180, 0);    // Rotation to be applied to the camera
    public float lookAtDistanceOffset = 5;                     // Offset for LookAt target
}