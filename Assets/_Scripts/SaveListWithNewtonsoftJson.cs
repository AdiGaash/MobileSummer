using System.Collections.Generic; // For lists
using System.IO; // For file operations
using UnityEngine; // For Application.persistentDataPath
using Newtonsoft.Json; // For JSON serialization and deserialization

public class JsonSaverWithNewtonsoft
{
    // This function will save a list of GameData objects as a JSON file
    public void SaveGameDataList(List<GameData> dataList, string fileName)
    {
        // Convert the list of objects directly to JSON format using Newtonsoft.Json
        string json = JsonConvert.SerializeObject(dataList, Formatting.Indented); // 'Indented' makes the output human-readable

        // Create a file path (this saves the file in the persistent data path of the game)
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        
        // Write the JSON data to a file
        File.WriteAllText(path, json);
        
        Debug.Log("List of data saved as JSON to: " + path); // For debugging purposes
    }
}


public class ExampleJsonSaverWithNewtonsoft
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

        // Create an instance of the JsonSaverWithNewtonsoft class
        JsonSaverWithNewtonsoft saver = new JsonSaverWithNewtonsoft();

        // Save the list of game data as JSON, file will be named "GameDataList.json"
        saver.SaveGameDataList(gameDataList, "GameDataList");
    }
}
