using UnityEngine;
using UnityEngine.UI;


public class CameraCapture : MonoBehaviour
{
    public RawImage rawImage; // Assign this in the Inspector
    private Texture2D cameraTexture;

    // Call this method to capture the camera feed
    public void CaptureCamera()
    {
        // Call the new method to open the camera
        OpenCamera();
    }

    // Method to open the camera and take a picture
    private void OpenCamera()
    {
        NativeCamera.TakePicture(OnPictureTaken, 512); // Adjust the size and quality as needed
    }

    // This method will be called when the picture is taken
    private void OnPictureTaken(string path)
    {
        if (path != null)
        {
            LoadImage(path);
        }
    }

    // Load the image into a Texture2D
    private void LoadImage(string path)
    {
        // Create a new Texture2D
        cameraTexture = new Texture2D(2, 2);
        byte[] imageData = System.IO.File.ReadAllBytes(path);
        cameraTexture.LoadImage(imageData); // Load image data into texture

        // Set the texture to the RawImage component
        rawImage.texture = cameraTexture;
        rawImage.rectTransform.sizeDelta = new Vector2(cameraTexture.width, cameraTexture.height); // Adjust RawImage size
    }
}