using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // The static instance of the singleton
    private static T _instance;

    // Lock for thread safety if needed
    private static readonly object _lock = new object();

    // Flag to track if the application is shutting down
    private static bool applicationIsQuitting = false;

    // Public property to access the singleton instance
    public static T Instance
    {
        get
        {
            // If the application is quitting, return null
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                 "' already destroyed on application quit. Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                // If instance is null, try to find it in the scene
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    // If it's still null, create a new game object and attach the singleton component
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        // Make the object persistent across scenes
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
    }

    // Called when the application quits, to prevent creating a new instance
    private void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }

    // Called when the singleton object is destroyed
    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}