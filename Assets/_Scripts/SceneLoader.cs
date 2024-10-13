using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    // Assets to load
    public GameObject[] objectsToLoad;

    void Start()
    {
        // Load a new scene
        LoadScene("GameplayScene");

        // Load all assets
        foreach (GameObject obj in objectsToLoad)
        {
            Instantiate(obj);
        }
    }

    void Update()
    {
        // Search for the player
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            // Decrease player health
            player.GetComponent<PlayerHealth>().DecreaseHealth(1);
        }

        // Perform some task every frame
        if (Time.time % 2 == 0)
        {
            PerformTask();
        }
    }

    void LoadScene(string sceneName)
    {
        // Load a scene
        SceneManager.LoadScene(sceneName);
    }

    void PerformTask()
    {
        // Perform some heavy calculation
        List<int> numbers = new List<int>();
        for (int i = 0; i < 100000; i++)
        {
            numbers.Add(i * i);
        }
        Debug.Log("Task performed!");
    }
}