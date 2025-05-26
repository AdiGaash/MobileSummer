
using UnityEngine;

public class CameraScreenCapture : MonoBehaviour
{
    public void CaptureAndSaveCamera()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;
        
        // Create a RenderTexture with the same dimensions as the camera's target texture
        RenderTexture renderTexture = new RenderTexture(
            mainCamera.pixelWidth,
            mainCamera.pixelHeight,
            24  // depth buffer
        );
        
        // Store the camera's current render texture
        RenderTexture previousRenderTexture = mainCamera.targetTexture;
        
        // Set the camera to render to our texture
        mainCamera.targetTexture = renderTexture;
        
        // Render the camera's view
        mainCamera.Render();
        
        // Create a new Texture2D and read the RenderTexture data into it
        Texture2D screenshot = new Texture2D(
            mainCamera.pixelWidth,
            mainCamera.pixelHeight,
            TextureFormat.RGB24,
            false
        );
        
        // Store the currently active render texture
        RenderTexture previousActive = RenderTexture.active;
        
        // Set the render texture as active
        RenderTexture.active = renderTexture;
        
        // Read the pixels from the active render texture
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();
        
        // Restore previous render texture states
        RenderTexture.active = previousActive;
        mainCamera.targetTexture = previousRenderTexture;
        
        ImageHandler.SaveImage(screenshot, "camera_capture.png");
        
        
        
        // Clean up
        Destroy(screenshot);
        Destroy(renderTexture);
        
        Debug.Log($"Camera screenshot saved to: camera_capture.png");
    }
    
}