using UnityEngine;
using UnityEngine.UI; // Only needed if you connect buttons or UI
using System.Collections;
using UnityEngine.Android;

public class AndroidPermissions : MonoBehaviour
{
    // Example permissions you might want to request
    private string[] permissionsToRequest = {
        Permission.Camera,
        Permission.Microphone,
        Permission.FineLocation,
        Permission.ExternalStorageWrite
    };

    void Start()
    {
        // Automatically check and request all permissions on start
        StartCoroutine(RequestAllPermissions());
    }

    /// <summary>
    /// Example 1: Request a single permission
    /// </summary>
    public void RequestCameraPermission()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Debug.Log("Camera permission already granted.");
        }
        else
        {
            Debug.Log("Requesting camera permission...");
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    /// <summary>
    /// Example 2: Request multiple permissions at once using a coroutine
    /// </summary>
    private IEnumerator RequestAllPermissions()
    {
        foreach (string perm in permissionsToRequest)
        {
            if (!Permission.HasUserAuthorizedPermission(perm))
            {
                Debug.Log("Requesting permission: " + perm);
                Permission.RequestUserPermission(perm);
                // Wait one frame to allow dialog to pop up
                yield return null;
            }
            else
            {
                Debug.Log("Permission already granted: " + perm);
            }
        }
    }

    /// <summary>
    /// Example 3: Ask for permission when player presses a button
    /// (You can connect this to a UI Button)
    /// </summary>
    public void AskMicrophoneOnButtonClick()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Debug.Log("Microphone access already granted.");
        }
        else
        {
            Debug.Log("Asking for microphone access...");
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    /// <summary>
    /// Example 4: Conditional usage after permission check
    /// </summary>
    public void UseLocationIfAllowed()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Debug.Log("Accessing location services...");
            // Add your location logic here
        }
        else
        {
            Debug.LogWarning("Location permission not granted.");
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }
}
