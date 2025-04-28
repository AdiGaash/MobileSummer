using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoaderAsync : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private bool loadOnStart = false; // Option to start loading automatically on start
    [SerializeField] private float minLoadingTime = 2.0f; // Minimum time to display the loading progress (optional)

    private AsyncOperation asyncOperation;
    private bool isLoading = false;

    void Start()
    {
        if (loadOnStart)
        {
            StartLoadingScene();
        }
    }

    public void StartLoadingScene()
    {
        if (!isLoading)
        {
            StartCoroutine(LoadSceneAsync());
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        isLoading = true;

        asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        float startTime = Time.time;

        while (!asyncOperation.isDone)
        {
            // Calculate progress as a percentage
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // asyncOperation.progress goes from 0 to 0.9

            // Optionally display or use the progress value
            Debug.Log("Loading progress: " + (progress * 100f).ToString("F0") + "%");

            // Optionally: Display loading progress UI or animation

            // Wait until the minimum loading time has elapsed
            if (Time.time - startTime >= minLoadingTime)
            {
                // Check if the loading is almost done (progress is at least 90%)
                if (asyncOperation.progress >= 0.9f)
                {
                    // Optionally: Display "Press any key to continue" message

                    // Wait for user input to activate the scene
                    if (Input.anyKeyDown)
                    {
                        asyncOperation.allowSceneActivation = true;
                    }
                }
            }
            
            yield return null;
        }
        isLoading = false;
    }
}
