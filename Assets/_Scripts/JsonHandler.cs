using System.IO; // For file operations
using UnityEngine; // Still needed for JsonUtility and Application.persistentDataPath

// Define the class structure for the object you want to save
[System.Serializable] // Necessary to make the class serializable to JSON
public class GameData
{
    public string playerName;
    public int score;
    public float playTime;
}

// This class no longer inherits from MonoBehaviour
public class JsonHandler
{
    // This function will load the object from a JSON file
    public GameData LoadGameData(string fileName)
    {
        // Create a file path (this loads the file from the persistent data path of the game)
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        // Check if the file exists
        if (File.Exists(path))
        {
            // Read the JSON data from the file
            string json = File.ReadAllText(path);
            // Deserialize the JSON data back into a GameData object
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogError("Save file not found: " + path);
            return null; // Return null if the file does not exist
        }
    }
    
    
}
{
    // This function will save the object as a JSON file
    public void SaveGameData(GameData data, string fileName)
    {
        // Convert the object to JSON format
        string json = JsonUtility.ToJson(data, true); // 'true' makes the output human-readable
        // Create a file path (this saves the file in the persistent data path of the game)
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        // Write the JSON data to a file
        File.WriteAllText(path, json);
        
        Debug.Log("Data saved as JSON to: " + path); // For debugging purposes
    }
}


public class ExampleJsonSaver
{
    public void SaveExample()
    {
        // Create a new GameData object and populate it with example data
        GameData newGameData = new GameData
        {
            playerName = "PlayerOne",
            score = 1500,
            playTime = 25.5f
        };

        // Create an instance of the JsonSaver class
        JsonSaver saver = new JsonSaver();

        // Save the game data as JSON, file will be named "SaveFile.json"
        saver.SaveGameData(newGameData, "SaveFile");
    }
}