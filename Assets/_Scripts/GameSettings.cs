using System;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Static instance of the GameSettings which allows it to be accessed by any other script
    public static GameSettings Instance { get; private set; }

    
    // Enum for control settings
    public enum GameControls
    {
        Buttons,      // Control using buttons
        Gestures,     // Control using gestures
        Accelerometer // Control using device accelerometer
    }
    
    // Game settings variables
    
    // Variable to hold the current control type
    public GameControls controlScheme = GameControls.Buttons;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Check if there's already an instance of GameSettings
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
        else
        {
            Instance = this; // Set this instance as the active one
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
    }

    private void Start()
    {
        LoadSettings();
    }


    // Function to update control scheme
    public void SetControlScheme(GameControls newControlScheme)
    {
        controlScheme = newControlScheme; // Set the control scheme
        // You can add specific logic here to switch input types if needed
    }

    // Optionally save settings to PlayerPrefs if needed
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ControlScheme", (int)controlScheme); // Save control scheme as integer
        PlayerPrefs.Save(); // Save the PlayerPrefs
    }

    // Optionally load settings from PlayerPrefs
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("ControlScheme"))
        {
            controlScheme = (GameControls)PlayerPrefs.GetInt("ControlScheme"); // Load control scheme
        }
    }
}
