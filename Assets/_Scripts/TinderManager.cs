using System;
using System.Collections.Generic;
using UnityEngine;


public class TinderManager : MonoBehaviour
{
    public GameObject tinderCardPrefab; // Prefab for the Tinder card
    public Transform cardContainer; // Parent transform where cards will be instantiated
    
    private TinderCardData[] cardDataArray;
    private int currentCardIndex = 0;
    private TinderUserSwiptData userSwipeData;

    private void Start()
    {
        // load tinder user swipe data
        userSwipeData = LoadTinderUserSwipeData();
        
        // Load Tinder card data from a JSON file
        cardDataArray = LoadTinderCardData();
        
        // Show the first card
        if (cardDataArray.Length > 0)
        {
            ShowNextCard();
        }
        else
        {
            Debug.LogError("No card data available to display");
        }
    }

    private TinderUserSwiptData LoadTinderUserSwipeData()
    {
        // Path to the JSON file (in Resources folder)
        string jsonFileName = "tinder_user_swipe_data";
        
        // Load the JSON text asset from Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        
        if (jsonFile == null)
        {
            Debug.LogWarning("Failed to load user swipe data JSON file: " + jsonFileName);
            return new TinderUserSwiptData
            {
                likedCards = new List<TinderCardData>(),
                dislikedCards = new List<TinderCardData>(),
                superLikedCards = new List<TinderCardData>()
            };
        }
        
        // Deserialize the JSON data into TinderUserSwiptData
        TinderUserSwiptData userData = JsonUtility.FromJson<TinderUserSwiptData>(jsonFile.text);
        
        if (userData == null)
        {
            Debug.LogWarning("Failed to parse user swipe data from JSON");
            return new TinderUserSwiptData
            {
                likedCards = new List<TinderCardData>(),
                dislikedCards = new List<TinderCardData>(),
                superLikedCards = new List<TinderCardData>()
            };
        }
        
        Debug.Log($"Successfully loaded user swipe data: {userData.likedCards.Count} liked, " +
                  $"{userData.dislikedCards.Count} disliked, {userData.superLikedCards.Count} super liked");
        return userData;
    }
    
    private void ShowNextCard()
    {
        if (currentCardIndex >= cardDataArray.Length)
        {
            Debug.Log("No more cards to show");
            return;
        }
        
        // Instantiate the card prefab
        GameObject cardObject = Instantiate(tinderCardPrefab, cardContainer);
        
        // Get the TinderCard component
        TinderCard card = cardObject.GetComponent<TinderCard>();
        
        if (card != null)
        {
            // Initialize the card with data
            card.Initialize(cardDataArray[currentCardIndex]);
            currentCardIndex++;
        }
        else
        {
            Debug.LogError("TinderCard component not found on the prefab");
        }
    }

    private TinderCardData[] LoadTinderCardData()
    {
        // Path to the JSON file (in Resources folder)
        string jsonFileName = "tinder_cards";
        
        // Load the JSON text asset from Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        
        if (jsonFile == null)
        {
            Debug.LogError("Failed to load JSON file: " + jsonFileName);
            return new TinderCardData[0]; // Return empty array to avoid null reference
        }
        
        // Deserialize the JSON data into an array of TinderCardData
        TinderCardData[] cardData = JsonUtility.FromJson<TinderCardDataWrapper>(jsonFile.text).cards;
        
        if (cardData == null || cardData.Length == 0)
        {
            Debug.LogWarning("No tinder card data found in JSON file");
            return new TinderCardData[0];
        }
        
        Debug.Log($"Successfully loaded {cardData.Length} tinder cards");
        return cardData;
    }
    
}