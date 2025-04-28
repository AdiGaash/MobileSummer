using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public RawImage displayImage;  // UI RawImage to display the camera feed
    private WebCamTexture webCamTexture;
    private WebCamDevice[] devices;
    private int currentDeviceIndex = 0; // Default to the first camera

    void Start()
    {
        // Get all available devices
        devices = WebCamTexture.devices;
        
        // Check if there are any cameras available
        if (devices.Length > 0)
        {
            // Start the first camera by default
            StartCamera(currentDeviceIndex);
        }
        else
        {
            Debug.LogError("No camera devices found");
        }
    }

    // Method to start a camera by index
    private void StartCamera(int deviceIndex)
    {
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
        }
        
        // Get the selected device name
        string deviceName = devices[deviceIndex].name;

        // Initialize the WebCamTexture with the selected device
        webCamTexture = new WebCamTexture(deviceName);

        // Assign the WebCamTexture to the RawImage
        displayImage.texture = webCamTexture;

        // Start the WebCamTexture
        webCamTexture.Play();
    }

    // Method to switch between front and back cameras
    public void SwitchCamera()
    {
        // Increment the current device index
        currentDeviceIndex = (currentDeviceIndex + 1) % devices.Length;

        // Start the new camera
        StartCamera(currentDeviceIndex);
    }

    void OnDestroy()
    {
        // Stop the camera when the object is destroyed
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }
}