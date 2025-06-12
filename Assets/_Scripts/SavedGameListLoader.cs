using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;
using UnityEngine.UI;


public class SavedGameListLoader : MonoBehaviour
{
    public GameObject SavedGameButtonPrefab;
    public Transform SavedGameListContainer;
    
    private readonly string SAVE_DIRECTORY = "SavedGames";
    private readonly string SAVE_FILE = "savedgames.json";

    private void Start()
    {
        LoadSavedGames();
    }

    private void LoadSavedGames()
    {
        // Clear existing buttons
        foreach (Transform child in SavedGameListContainer)
        {
            Destroy(child.gameObject);
        }
        
        List<SaveGameInfo> savedGames = LoadSavedGamesList();
        
        foreach (var saveGameInfo in savedGames)
        {
            GameObject buttonGameObject = Instantiate(SavedGameButtonPrefab, SavedGameListContainer);
            // Set button text to show relevant save game information
            string displayText = $"Level: {saveGameInfo.LevelName}\n" +
                                 $"Score: {saveGameInfo.Score}\n" +
                                 $"Date: {saveGameInfo.dateTime}";
                               
            buttonGameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = displayText;
            // Use a lambda to pass the parameter
            buttonGameObject.GetComponent<Button>().onClick.AddListener(() => OnSavedGameButtonClick(saveGameInfo));
           
        }
    }

    private void OnSavedGameButtonClick(SaveGameInfo saveGameInfo)
    {
        
    }

    private List<SaveGameInfo> LoadSavedGamesList()
    {
        try
        {
            string savePath = Path.Combine(Application.persistentDataPath, SAVE_DIRECTORY, SAVE_FILE);
            
            if (!File.Exists(savePath))
            {
                Debug.LogWarning("No saved games file found.");
                return new List<SaveGameInfo>();
            }

            string jsonData = File.ReadAllText(savePath);
            return JsonConvert.DeserializeObject<List<SaveGameInfo>>(jsonData);
        }
        
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading saved games: {e.Message}");
            return new List<SaveGameInfo>();
        }
    }

    
    private SaveGameInfo GenerateRandomSaveData()
    {
        string[] levelNames = { "Forest Temple", "Ice Cavern", "Fire Mountain", "Desert Ruins", "Space Station" };
        string[] controlSchemes = { "Standard", "Advanced", "Custom", "Classic", "Pro" };
        
        // Get current timestamp
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        // Generate random values
        System.Random random = new System.Random();
        
        SaveGameInfo randomSave = new SaveGameInfo
        {
            pathToSaveGameTumbnail = $"thumbnails/save_{timestamp.Replace(":", "-")}.png",
            LevelName = levelNames[random.Next(levelNames.Length)],
            Score = random.Next(0, 100000),
            lives = random.Next(1, 6),
            dateTime = timestamp,
            GameSettingData = new GameSettingData
            {
                ControlScheme = controlSchemes[random.Next(controlSchemes.Length)],
                DifficultyLevel = random.Next(1, 4) // 1 = Easy, 2 = Medium, 3 = Hard
            }
        };

        return randomSave;
    }

    public void OnSaveGameButtonClicked()
    {

        SaveGameInfo newSave = new SaveGameInfo();
        newSave = GenerateRandomSaveData();


       // SaveGameDataManager.SaveGame(newSave);


    }

}

public class SaveGameInfo
{
    public string pathToSaveGameTumbnail;
    public string LevelName;
    public int  Score;
    public int lives;
    public string dateTime;
    public GameSettingData GameSettingData; // Assuming GameSetting is a class that holds game settings

    // Add any other relevant fields here
}

public class GameSettingData
{
    public string ControlScheme; // Example field, adjust as needed
    public int DifficultyLevel; // Example field, adjust as needed
}
