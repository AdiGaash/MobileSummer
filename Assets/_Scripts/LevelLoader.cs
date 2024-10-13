using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelLoader : MonoBehaviour
{
    // The position to check against
    public Transform nextLevelCenterWorldPos;

    // The distance threshold for loading and unloading the scene
    public float loadLevelDistanceThreshold = 2f;

    // The name of the scene to load/unload
    public string sceneToLoad;

    private bool nextLevelIsLoading = false;

    private void Awake()
    {
        // Ensure this GameObject persists across scene loads
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Calculate the distance between the player and the target position
        float distance = Vector3.Distance(transform.position, nextLevelCenterWorldPos.position);

        // Check if the distance is less than the threshold to load the scene
        if (!nextLevelIsLoading && distance < loadLevelDistanceThreshold)
        {
            nextLevelIsLoading = true;
            LoadNextLevel();
        }
        // Check if the distance is greater than the threshold to unload the scene
        else if (nextLevelIsLoading && distance > loadLevelDistanceThreshold)
        {
            nextLevelIsLoading = false;
            UnloadCurrentLevel();
        }
    }

    private void LoadNextLevel()
    {
        // Load the scene additively
        StartCoroutine(LoadSceneAsync());
    }

    private System.Collections.IEnumerator LoadSceneAsync()
    {
        // Begin loading the scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        // Wait until the asynchronous scene loading is complete
        while (!asyncLoad.isDone)
        {
            // You can add a loading screen or progress indicator here
            yield return null;
        }
    }

    private void UnloadCurrentLevel()
    {
        // Unload the current scene asynchronously
        StartCoroutine(UnloadSceneAsync());
    }

    private System.Collections.IEnumerator UnloadSceneAsync()
    {
        // Begin unloading the scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene unloading is complete
        while (!asyncUnload.isDone)
        {
            // You can add a loading screen or progress indicator here if desired
            yield return null;
        }
        nextLevelIsLoading = false;
    }
}
