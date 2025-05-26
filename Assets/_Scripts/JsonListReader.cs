
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonReader : MonoBehaviour
{
    // Generic function to read a list of any type from JSON
    public List<T> ReadJsonList<T>(string filePath)
    {
        try
        {
            // Read the JSON file content
            string jsonContent = File.ReadAllText(filePath);
            
            // Deserialize the JSON to List<T>
            List<T> items = JsonConvert.DeserializeObject<List<T>>(jsonContent);
            
            Debug.Log($"Successfully read {items.Count} items from {filePath}");
            return items;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error reading JSON file: {e.Message}");
            return new List<T>();
        }
    }

    // Example usage with a specific data type
    [System.Serializable]
    public class ItemData
    {
        public string name { get; set; }
        public int value { get; set; }
        public bool isActive { get; set; }
    }

    // Example method showing how to use the generic reader
    public void LoadItemsExample()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "items.json");
        List<ItemData> items = ReadJsonList<ItemData>(filePath);
        
        // Process the loaded items
        foreach (var item in items)
        {
            Debug.Log($"Loaded item: {item.name}, Value: {item.value}, Active: {item.isActive}");
        }
    }
}