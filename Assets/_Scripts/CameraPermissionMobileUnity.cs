using System;
using UnityEngine;
using UnityEngine.Android;

/// <summary>
/// Checking for permission c# code MonoBehaviour (not calling the java script) example
/// </summary>
public class CameraPermissionMobileUnity : MonoBehaviour
{
    void Start()
    {
        // Check if the permission is already granted
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // Request permission
            Permission.RequestUserPermission(Permission.Camera);
        }
        else
        {
            // Permission is already granted
            Debug.Log("Camera permission is already granted");
        }
    }

   

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Check again when the application gains focus
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                // Permission is granted
                Debug.Log("Camera permission is granted");
            }
            else
            {
                // Permission is not granted
                Debug.Log("Camera permission is not granted");
            }
        }
    }
}
