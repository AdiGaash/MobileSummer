using System;
using System.IO;
using UnityEngine;


public class LoadSavedGameMenu : MonoBehaviour
{
    
    public GameObject buttonPrefab; // Prefab for the buttons to load saved games
    void Start()
    {

        
        string folderPath; 
#if UNITY_EDITOR
        folderPath = Path.Combine(Application.dataPath, "/LocalSavedGamesFolder" + "SavedGames");
#else
        folderPath = Path.Combine(Application.persistentDataPath, "SavedGames");
#endif
        var savedGames = JsonWithNewtonsoft.ReadJsonList<SaveGameData>(folderPath);
        foreach (var savedGameData in savedGames)
        {
            
        }
    }
}

public class SaveGameData
{
    public string saveName; // Name of the saved game
    public string saveDate; // Date when the game was saved
    public string saveTime; // Time when the game was saved
    public int playerLevel; // Player's level at the time of saving
    public int playerScore; // Player's score at the time of saving
    public string playerPosition; // Player's position in the game world
    public string playerInventory;
    public string nameOfScene;
    public string screenShotPath; // Player's inventory items
    
    
}


