using System.Collections.Generic; // For working with lists
using System.IO; // For file operations
using UnityEngine; // Still needed for JsonUtility and Application.persistentDataPath



public class JsonListSaver
{
    // This function will save a list of GameData objects as a JSON file
    public void SaveGameDataList(List<GameData> dataList, string fileName)
    {
        // Convert the list of objects to JSON format
        string json = JsonUtility.ToJson(new GameDataListWrapper(dataList), true); // 'true' makes the output human-readable
        
        // Create a file path (this saves the file in the persistent data path of the game)
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        // Write the JSON data to a file
        File.WriteAllText(path, json);
        
        Debug.Log("List of data saved as JSON to: " + path); // For debugging purposes
    }

    // Wrapper class to serialize a list using JsonUtility (which doesn’t directly support lists)
    [System.Serializable]
    private class GameDataListWrapper
    {
        public List<GameData> gameDataList;

        public GameDataListWrapper(List<GameData> dataList)
        {
            gameDataList = dataList;
        }
    }
}

public class ExampleJsonListSaver
{
    public void SaveExampleList()
    {
        // Create a list of GameData objects and populate it with example data
        List<GameData> gameDataList = new List<GameData>
        {
            new GameData { playerName = "PlayerOne", score = 1500, playTime = 25.5f },
            new GameData { playerName = "PlayerTwo", score = 2300, playTime = 18.75f },
            new GameData { playerName = "PlayerThree", score = 1000, playTime = 35.0f }
        };

        // Create an instance of the JsonSaver class
        JsonListSaver saver = new JsonListSaver();

        // Save the list of game data as JSON, file will be named "GameDataList.json"
        saver.SaveGameDataList(gameDataList, "GameDataList");
    }
}
