using UnityEngine;
using UnityEngine.UI;

public class WebcamCapture1 : MonoBehaviour
{
    public RawImage displayImage;  // UI RawImage to display the webcam feed
    private WebCamTexture webCamTexture;

    void Start()
    {
        // Initialize the webcam texture
        webCamTexture = new WebCamTexture();
        
        // Assign the webcam texture to the RawImage
        displayImage.texture = webCamTexture;
        
        // Start the webcam
        webCamTexture.Play();
        
       
    }

    

    void OnDestroy()
    {
        // Stop the webcam when the object is destroyed
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }
}