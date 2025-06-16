using UnityEngine;
using UnityEditor;
using System.IO;

// Remove the CreateAssetMenu attribute as we'll handle creation through the menu item
public class GameSettings : ScriptableObject
{
    [Header("General Settings")]
    public string gameName = "My Game";
    public bool debugMode = false;
    
    [Header("Gameplay Settings")]
    public float playerSpeed = 5f;
    public int maxHealth = 100;
    public float jumpForce = 10f;
    
    [Header("Audio Settings")]
    [Range(0, 1)]
    public float masterVolume = 1f;
    [Range(0, 1)]
    public float musicVolume = 0.8f;
    [Range(0, 1)]
    public float sfxVolume = 1f;
    
    // Singleton pattern to access settings throughout the project
    private static GameSettings _instance;
    public static GameSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GameSettings>("GameSettings");
                if (_instance == null)
                {
                    Debug.LogError("Game Settings not found in Resources folder. Access it via the top menu: MyProject > Game Settings.");
                }
            }
            return _instance;
        }
    }
}

#if UNITY_EDITOR
public static class GameSettingsMenu
{
    private const string ResourcesPath = "Assets/Resources";
    private const string AssetName = "GameSettings.asset";
    private const string FullPath = ResourcesPath + "/" + AssetName;

    [MenuItem("MyProject/Game Settings")]
    public static void OpenOrCreateGameSettings()
    {
        // Try to find existing GameSettings asset
        GameSettings settings = Resources.Load<GameSettings>("GameSettings");
        
        if (settings != null)
        {
            // If it exists, select and ping it in the Project window
            Selection.activeObject = settings;
            EditorGUIUtility.PingObject(settings);
        }
        else
        {
            // Create Resources folder if it doesn't exist
            if (!Directory.Exists(ResourcesPath))
            {
                Directory.CreateDirectory(ResourcesPath);
                AssetDatabase.Refresh();
            }
            
            // Create a new GameSettings asset
            GameSettings newSettings = ScriptableObject.CreateInstance<GameSettings>();
            
            // Save it to the Resources folder
            AssetDatabase.CreateAsset(newSettings, FullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            // Select and ping the newly created asset
            Selection.activeObject = newSettings;
            EditorGUIUtility.PingObject(newSettings);
            
            Debug.Log("GameSettings asset created at " + FullPath);
        }
    }
}
#endif