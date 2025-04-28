using System.IO;
using UnityEngine;

public static class ImageHandler
{
    // Method to save a texture as a PNG file
    public static void SaveImage(Texture2D texture, string fileName)
    {
        byte[] bytes = texture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Image saved to: " + filePath);
    }
    
    public static Texture2D LoadImage(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }
        Debug.LogError("Image not found at: " + filePath);
        return null;
    }
}