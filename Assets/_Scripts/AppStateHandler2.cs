using UnityEngine;

public class AppStateHandler2 : MonoBehaviour
{
    // This method is called when the application gains or loses focus.
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Debug.Log("App has gained focus");
            // Add logic to handle when the app gains focus
            ResumeApp();
        }
        else
        {
            Debug.Log("App has lost focus");
            // Add logic to handle when the app loses focus
            PauseApp();
        }
    }

    // This method is called when the application is paused or resumed.
    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Debug.Log("App is paused");
            // Add logic to handle when the app is paused
            PauseApp();
        }
        else
        {
            Debug.Log("App is resumed");
            // Add logic to handle when the app is resumed
            ResumeApp();
        }
    }

    // Method to handle pause logic
    void PauseApp()
    {
        // Example: Pausing game time
        Time.timeScale = 0;
        // Add any additional logic you need for pausing the app
        Debug.Log("Game Paused");
    }

    // Method to handle resume logic
    void ResumeApp()
    {
        // Example: Resuming game time
        Time.timeScale = 1;
        // Add any additional logic you need for resuming the app
        Debug.Log("Game Resumed");
    }
}
