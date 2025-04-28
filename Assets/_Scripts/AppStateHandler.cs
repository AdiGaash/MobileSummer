using UnityEngine;

public class AppStateHandler : MonoBehaviour
{
    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Debug.Log("Application paused");
            // Handle pause logic (e.g., pause gameplay, save game state)
        }
        else
        {
            Debug.Log("Application resumed");
            // Handle resume logic (e.g., resume gameplay, reload game state)
        }
    }

    
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Debug.Log("Application gained focus");
            // Handle focus gain logic (e.g., resume UI interactions, refresh data)
        }
        else
        {
            Debug.Log("Application lost focus");
            // Handle focus loss logic (e.g., pause UI interactions, save data)
        }
    }
}