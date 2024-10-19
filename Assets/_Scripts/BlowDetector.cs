using UnityEngine;

public class BlowDetector : MonoBehaviour
{
    public float blowThreshold = 0.2f; // Adjust the threshold to match blowing strength
    private AudioClip microphoneInput;
    private int sampleWindow = 128; // Number of audio samples to analyze
    private bool isBlowing = false;

    void Start()
    {
        // Start capturing audio from the microphone
        microphoneInput = Microphone.Start(null, true, 10, 44100);
    }

    void Update()
    {
        // Check if the microphone has captured enough audio
        if (Microphone.GetPosition(null) > 0 && microphoneInput != null)
        {
            float currentVolume = GetAverageVolume();
            
            // Detect blowing based on volume threshold
            if (currentVolume > blowThreshold)
            {
                if (!isBlowing)
                {
                    isBlowing = true;
                    Debug.Log("Blowing detected!");
                    // Add your custom blowing behavior here
                }
            }
            else
            {
                if (isBlowing)
                {
                    isBlowing = false;
                    Debug.Log("Blowing stopped.");
                }
            }
        }
    }

    float GetAverageVolume()
    {
        float[] data = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1);
        if (micPosition < 0)
            return 0;

        microphoneInput.GetData(data, micPosition);
        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += Mathf.Abs(data[i]);
        }
        return sum / sampleWindow;
    }
}