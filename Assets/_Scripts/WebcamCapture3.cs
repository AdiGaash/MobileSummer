using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WebcamCapture3 : MonoBehaviour
{
    public RawImage displayImage;
    private WebCamTexture webCamTexture;

    

    // Coroutine to request camera permission
    private IEnumerator RequestCameraPermissionCoroutine()
    {
        // Check if the camera permission is already granted
        if (!HasCameraPermission())
        {
            // Request the camera permission
            RequestCameraPermission();

            // Wait for the user to respond to the permission request
            yield return new WaitForSeconds(1);

            // Check again if the permission is granted
            if (!HasCameraPermission())
            {
                Debug.LogError("Camera permission is not granted!");
                yield break;
            }
        }

        // Initialize the webcam if permission is granted
        InitializeWebcam();
    }

    // Method to initialize the webcam
    private void InitializeWebcam()
    {
        webCamTexture = new WebCamTexture();
        if(displayImage!=null)
            displayImage.texture = webCamTexture;
        webCamTexture.Play();
    }

    // Check if camera permission is granted
    private bool HasCameraPermission()
    {
        if (Application.platform != RuntimePlatform.Android)
            return true;

        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity"))
        {
            using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext"))
            {
                using (AndroidJavaObject permissionChecker = new AndroidJavaClass("androidx.core.content.ContextCompat"))
                {
                    int result = permissionChecker.CallStatic<int>("checkSelfPermission", context, "android.permission.CAMERA");
                    return result == 0; // 0 means PERMISSION_GRANTED
                }
            }
        }
    }

    // Request camera permission
    private void RequestCameraPermission()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

       
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity"))
        {
            using (AndroidJavaObject permissionRequester = new AndroidJavaClass("androidx.core.app.ActivityCompat"))
            {
                permissionRequester.CallStatic("requestPermissions", activity, new string[] { "android.permission.CAMERA" }, 0);
            }
        }
    }

    void OnDestroy()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }

    // Add method to capture and save the image (same as in the previous example)
    public void CaptureAndSaveImage(string fileName)
    {
        
        StartCoroutine(RequestCameraPermissionCoroutine());
        
        StartCoroutine(CaptureAndSave(fileName));
    }

    private IEnumerator CaptureAndSave(string fileName)
    {
        yield return new WaitForEndOfFrame();

        Texture2D capturedImage = new Texture2D(webCamTexture.width, webCamTexture.height);
        capturedImage.SetPixels(webCamTexture.GetPixels());
        capturedImage.Apply();

        byte[] bytes = capturedImage.EncodeToPNG();
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName+".png");
        System.IO.File.WriteAllBytes(filePath, bytes);
        Debug.Log("Image saved to: " + filePath);
    }
}
