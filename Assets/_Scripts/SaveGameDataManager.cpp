using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveGameDataManager
{
    private static readonly string SAVE_DIRECTORY = "SavedGames";
    private static readonly string SAVE_FILE = "savedgames.json";

    public static void SaveGame(SaveGameInfo saveGameInfo)
    {
        try
        {
            // Get the full path for the save directory
            string saveDirectory = Path.Combine(Application.persistentDataPath, SAVE_DIRECTORY);
            string savePath = Path.Combine(saveDirectory, SAVE_FILE);

            // Create the directory if it doesn't exist
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // Load existing saves
            List<SaveGameInfo> savedGames = LoadSavedGames();
            
            // Add new save
            savedGames.Add(saveGameInfo);

            // Convert to JSON
            string jsonData = JsonConvert.SerializeObject(savedGames, Formatting.Indented);

            // Write to file
            File.WriteAllText(savePath, jsonData);

            Debug.Log($"Game saved successfully to: {savePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving game: {e.Message}");
        }
    }

    private static List<SaveGameInfo> LoadSavedGames()
    {
        string savePath = Path.Combine(Application.persistentDataPath, SAVE_DIRECTORY, SAVE_FILE);
        
        if (!File.Exists(savePath))
        {
            return new List<SaveGameInfo>();
        }

        try
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonConvert.DeserializeObject<List<SaveGameInfo>>(jsonData) ?? new List<SaveGameInfo>();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading saved games: {e.Message}");
            return new List<SaveGameInfo>();
        }
    }
}