using UnityEngine;

public class CameraCaptureToTexture : MonoBehaviour
{
    public Camera captureCamera; // The camera from which to capture
    public int textureWidth = 1920; // Width of the output texture
    public int textureHeight = 1080; // Height of the output texture
    public Texture2D capturedTexture; // Publicly accessible captured texture

    // This method captures the current camera view and stores it in 'capturedTexture'
    public void Capture()
    {
        // Step 1: Create a temporary RenderTexture with the specified width and height
        RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 24);
        captureCamera.targetTexture = renderTexture;

        // Step 2: Create a new Texture2D to store the captured image
        capturedTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGB24, false);

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

        // Step 7: Release and destroy the temporary RenderTexture to free up memory
        renderTexture.Release();
       
    }
}