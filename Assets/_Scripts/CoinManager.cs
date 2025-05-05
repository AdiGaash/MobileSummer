using TMPro;
using UnityEngine;
using UnityEngine.UI; // For accessing UI Text

// This script will manage the player's coin count and update the UI text.
public class CoinManager : MonoBehaviour
{
    // Reference to a Text element in the UI (drag in Inspector)
    public TMP_Text coinText;

    // How many coins the player has collected
    private int coinCount = 0;

    // This method will be connected to the UnityEvent
    public void AddCoin()
    {
        coinCount++; // Increase the coin count
        UpdateUI();  // Update the text display
    }

    // Update the UI text to show the current coin count
    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}