using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CaptureImage : MonoBehaviour
{
    [SerializeField] GameObject previewImage;
    //[SerializeField] RawImage previewImage;

    public Camera captureCamera; // The camera from which to capture
    public int textureWidth = 1920; // Width of the output texture
    public int textureHeight = 1080; // Height of the output texture

    private string filePath;

    // Start is called before the first frame update
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "capturedImage.png");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Capture()
    {
        // Step 1: Create a temporary RenderTexture with the specified width and height
        RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 24);
        captureCamera.targetTexture = renderTexture;

        // Step 2: Create a new Texture2D to store the captured image
        Texture2D capturedTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGB24, false);

        // Step 3: Render the camera's view
        captureCamera.Render();

        // Step 4: Set the active RenderTexture (this is where the camera rendered the image)
        RenderTexture.active = renderTexture;

        // Step 5: Copy the pixels from the RenderTexture into the Texture2D
        capturedTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
        capturedTexture.Apply(); // Apply the changes to the texture



        // Step 6: Reset the active RenderTexture and release resources
        RenderTexture.active = null;
        captureCamera.targetTexture = null;

        byte[] pngData = capturedTexture.EncodeToPNG();

        File.WriteAllBytes(filePath, pngData);
        Debug.Log("Image saved to: " + filePath);

        // Step 7: Release and destroy the temporary RenderTexture to free up memory
        renderTexture.Release();
    }


    public void LoadImage()
    {
        Texture2D tex = null;
        byte[] fileData;
        

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(textureWidth/10, textureHeight/10);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            previewImage.GetComponent<Renderer>().material.mainTexture = tex;
            //preview.GetComponent<Image>().GetComponent<Renderer>().material.mainTexture = tex;
            
        }
    }
}
