using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TinderCard : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public RawImage profileImage;
    
    public void Initialize(TinderCardData data)
    {
        // Set the text fields
        nameText.text = data.name;
        descriptionText.text = data.description;
        
        // Load and set the profile image
        Texture2D image = ImageHandler.LoadImage(data.imagePath);
        if (image != null)
        {
            profileImage.texture = image;
        }
        else
        {
            Debug.LogWarning($"Failed to load image at path: {data.imagePath}");
        }
    }
}