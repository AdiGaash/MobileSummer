using UnityEngine;
using UnityEditor;

// This script must be placed in an Editor folder to work correctly
public class PlayModeTracker : MonoBehaviour
{
    [InitializeOnLoadMethod]
    static void Initialize()
    {
        // Subscribe to the playModeStateChanged event
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        
        Debug.Log("PlayModeTracker initialized");
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.EnteredEditMode:
                Debug.Log("Entered Edit Mode");
                // Add your edit mode code here
                break;
                
            case PlayModeStateChange.ExitingEditMode:
                Debug.Log("Exiting Edit Mode");
                // Add code to execute just before entering play mode
                break;
                
            case PlayModeStateChange.EnteredPlayMode:
                Debug.Log("Entered Play Mode");
                // Add your play mode code here
                DoSomethingInPlayMode();
                break;
                
            case PlayModeStateChange.ExitingPlayMode:
                Debug.Log("Exiting Play Mode");
                // Add code to execute just before returning to edit mode
                break;
        }
    }
    
    private static void DoSomethingInPlayMode()
    {
        // This is where you would put the code you want to execute 
        // when transitioning from edit mode to play mode
        Debug.Log("Something happened because we entered Play Mode!");
        
        // Example: Find all GameObjects with a specific tag and do something with them
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var go in gameObjects)
        {
            Debug.Log($"Found player object: {go.name}");
            // Do something with each game object
        }
    }
}