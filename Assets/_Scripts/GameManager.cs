using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Your custom logic here
    public int score;

    // Example method
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score is now: " + score);
    }

    public void LoadAnotherScene()
    {
        SceneManager.LoadScene(0);
    }


    private void OnApplicationPause(bool pauseStatus)
    {
        
    }
}