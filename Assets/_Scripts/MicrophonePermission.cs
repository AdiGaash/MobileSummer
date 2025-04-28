using UnityEngine;
using UnityEngine.Android;

public class MicrophonePermission : MonoBehaviour
{
    void Start()
    {
        // Check if the permission is already granted
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // Request permission
            Permission.RequestUserPermission(Permission.Microphone);
        }
        else
        {
            // Permission is already granted
            Debug.Log("Microphone permission is already granted");
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Check again when the application gains focus
            if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                // Permission is granted
                Debug.Log("Microphone permission is granted");
            }
            else
            {
                // Permission is not granted
                Debug.Log("Microphone permission is not granted");
            }
        }
    }
}

