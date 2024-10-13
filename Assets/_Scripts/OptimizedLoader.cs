using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class OptimizedLoader : MonoBehaviour
{
    // Assets to load
    public GameObject[] objectsToLoad;
    private List<GameObject> objectPool = new List<GameObject>();

    private GameObject player;
    private PlayerHealth playerHealth;

    void Start()
    {
        // Asynchronously load the new scene with a loading screen
        StartCoroutine(LoadSceneAsync("GameplayScene"));

        // Cache the player reference
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        // Use object pooling for assets
        InitObjectPool();
    }

    void Update()
    {
        // Use cached player reference
        if (player != null)
        {
            playerHealth.DecreaseHealth(1);
        }
    }

    // Asynchronously loads a scene and shows a loading progress
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            yield return null;
        }
    }

    // Initializes the object pool
    void InitObjectPool()
    {
        foreach (GameObject obj in objectsToLoad)
        {
            GameObject pooledObject = Instantiate(obj);
            pooledObject.SetActive(false);
            objectPool.Add(pooledObject);
        }
    }

    // Periodic expensive task, executed using Coroutine
    IEnumerator PerformExpensiveTaskPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);  // Run the task every 2 seconds

            // Perform expensive calculation (optimized)
            for (int i = 0; i < objectPool.Count; i++)
            {
                // Reuse objectPool instead of recreating large lists
            }

            Debug.Log("Expensive task performed!");
        }
    }
}

internal class PlayerHealth
{
    public void DecreaseHealth(int i)
    {
        throw new System.NotImplementedException();
    }
}
