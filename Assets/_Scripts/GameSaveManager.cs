
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    public int currentLevel;
    public float playerHealth;
    public Vector3 playerPosition;
    public Dictionary<string, bool> unlockables;
    public DateTime lastSaveTime;

    public GameSaveData()
    {
        unlockables = new Dictionary<string, bool>();
        lastSaveTime = DateTime.Now;
    }
}

public class SaveManager : Singleton<SaveManager>
{
    private const string SAVE_FILE_NAME = "GameSaveData";
    private const string SAVE_FOLDER_NAME = "LocalSavedGamesFolder";
    private GameSaveData currentSaveData;

    private string SaveFolderPath => Path.Combine(Application.dataPath, SAVE_FOLDER_NAME);
    private string SaveFilePath => Path.Combine(SaveFolderPath, $"{SAVE_FILE_NAME}.json");

    protected override void Awake()
    {
        base.Awake();
        InitializeSaveSystem();
    }

    private void InitializeSaveSystem()
    {
        if (!Directory.Exists(SaveFolderPath))
        {
            Directory.CreateDirectory(SaveFolderPath);
        }

        LoadGame();
    }

    public void SaveGame()
    {
        try
        {
            if (currentSaveData == null)
            {
                currentSaveData = new GameSaveData();
            }

            currentSaveData.lastSaveTime = DateTime.Now;
            List<GameSaveData> saveDataList = new List<GameSaveData> { currentSaveData };
            //JsonWithNewtonsoft.SaveGameDataList(saveDataList, SAVE_FILE_NAME);
            Debug.Log($"Game saved successfully at {DateTime.Now}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving game: {e.Message}");
        }
    }

    public void LoadGame()
    {
        try
        {
            if (File.Exists(SaveFilePath))
            {
                List<GameSaveData> loadedData = JsonWithNewtonsoft.ReadJsonList<GameSaveData>(SaveFilePath);
                if (loadedData != null && loadedData.Count > 0)
                {
                    currentSaveData = loadedData[0];
                    Debug.Log($"Game loaded successfully. Last save: {currentSaveData.lastSaveTime}");
                }
            }
            else
            {
                currentSaveData = new GameSaveData();
                Debug.Log("No save file found. Created new save data.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading game: {e.Message}");
            currentSaveData = new GameSaveData();
        }
    }

    public void UpdateGameState(int level, float health, Vector3 position)
    {
        if (currentSaveData == null)
        {
            currentSaveData = new GameSaveData();
        }

        currentSaveData.currentLevel = level;
        currentSaveData.playerHealth = health;
        currentSaveData.playerPosition = position;
    }

    public void SetUnlockable(string unlockableId, bool state)
    {
        if (currentSaveData == null)
        {
            currentSaveData = new GameSaveData();
        }

        currentSaveData.unlockables[unlockableId] = state;
    }

    public bool GetUnlockableState(string unlockableId)
    {
        if (currentSaveData?.unlockables == null)
        {
            return false;
        }

        return currentSaveData.unlockables.TryGetValue(unlockableId, out bool state) && state;
    }

    public GameSaveData GetCurrentSaveData()
    {
        return currentSaveData;
    }

    public void DeleteSaveData()
    {
        try
        {
            if (File.Exists(SaveFilePath))
            {
                File.Delete(SaveFilePath);
                currentSaveData = new GameSaveData();
                Debug.Log("Save data deleted successfully");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error deleting save data: {e.Message}");
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }
}