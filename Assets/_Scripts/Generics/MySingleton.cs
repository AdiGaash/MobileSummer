/// <summary>
/// A basic Singleton class without thread-safety (no locking).
/// Suitable for Unity usage when accessed from the main thread only.
/// </summary>
public class MySingleton
{
    // Static instance of the class
    private static MySingleton _instance;

    // Public property to access the singleton instance
    public static MySingleton Instance
    {
        get
        {
            // If instance doesn't exist, create it
           if (_instance == null) 
            {
                _instance = new MySingleton();
            }

            return _instance;
        }
    }

    // Private constructor to prevent external instantiation
    private MySingleton()
    {
        // Initialization logic here
        DoSomething();
    }

    // Example method
    public void DoSomething()
    {
        UnityEngine.Debug.Log("Singleton without locking is working.");
    }
}