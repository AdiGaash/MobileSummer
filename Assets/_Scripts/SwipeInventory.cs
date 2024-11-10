using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwipeInventorySprites : MonoBehaviour
{
    public Sprite[] inventorySprites;       // Array of item sprites
    public Image currentItemImage;          // UI Image to display the current item
    public TMP_Text itemInfoText;           // TextMeshPro Text for item info
    public Image equippedSlot1Image;        // UI Image for the first equipped slot
    public Image equippedSlot2Image;        // UI Image for the second equipped slot

    private int currentIndex = 0;           // Tracks the currently selected sprite index
    private Vector2 touchStartPos;          // Start position of the touch
    private bool isLongPressing = false;    // Check if long press is ongoing
    private float longPressDuration = 0.75f; // Time needed for a long press
    private float swipeThreshold = 50f;     // Minimum distance for detecting a swipe

    private void Start()
    {
        DisplayCurrentItem(); // Initialize with the first item
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isLongPressing = true;
                    StartCoroutine(CheckLongPress(touch.position));
                    break;

                case TouchPhase.Moved:
                    float swipeDistance = touch.position.x - touchStartPos.x;

                    if (Mathf.Abs(swipeDistance) > swipeThreshold)
                    {
                        if (swipeDistance > 0)
                            SwipeRight();
                        else
                            SwipeLeft();

                        StopCoroutine(CheckLongPress(touch.position)); // Stop long press on swipe
                        isLongPressing = false;
                    }
                    break;

                case TouchPhase.Ended:
                    isLongPressing = false;
                    break;
            }
        }
    }

    // Swipes to the next item (left swipe)
    private void SwipeLeft()
    {
        currentIndex = (currentIndex + 1) % inventorySprites.Length;
        DisplayCurrentItem();
    }

    // Swipes to the previous item (right swipe)
    private void SwipeRight()
    {
        currentIndex = (currentIndex - 1 + inventorySprites.Length) % inventorySprites.Length;
        DisplayCurrentItem();
    }

    // Displays the current item based on `currentIndex`
    private void DisplayCurrentItem()
    {
        currentItemImage.sprite = inventorySprites[currentIndex];
        itemInfoText.text = "Item: " + inventorySprites[currentIndex].name; // Display item name with TMP_Text
    }

    // Checks for long press duration to equip the item
    private IEnumerator CheckLongPress(Vector2 startPosition)
    {
        yield return new WaitForSeconds(longPressDuration);

        if (isLongPressing)
        {
            EquipItem();
        }
    }

    // Equips the currently selected item, checking if the first slot is full
    private void EquipItem()
    {
        if (equippedSlot1Image.sprite == null)  // Check if the first slot is empty
        {
            equippedSlot1Image.sprite = inventorySprites[currentIndex];
            Debug.Log("Equipped in Slot 1: " + inventorySprites[currentIndex].name);
        }
        else if (equippedSlot2Image.sprite == null)  // Check if the second slot is empty
        {
            equippedSlot2Image.sprite = inventorySprites[currentIndex];
            Debug.Log("Equipped in Slot 2: " + inventorySprites[currentIndex].name);
        }
        else
        {
            Debug.Log("Both equip slots are full!"); // Optionally show a message if both slots are full
        }

        // Optional: Add haptic feedback for the long press on mobile devices
        Handheld.Vibrate();
    }

    // Save equipped items' indices to PlayerPrefs
    public void SaveEquippedItems()
    {
        // Save slot 1 and slot 2 items (index of sprite in inventorySprites array)
        int slot1Index = equippedSlot1Image.sprite != null ? System.Array.IndexOf(inventorySprites, equippedSlot1Image.sprite) : -1;
        int slot2Index = equippedSlot2Image.sprite != null ? System.Array.IndexOf(inventorySprites, equippedSlot2Image.sprite) : -1;

        PlayerPrefs.SetInt("EquippedSlot1", slot1Index);
        PlayerPrefs.SetInt("EquippedSlot2", slot2Index);
        PlayerPrefs.Save();

        Debug.Log("Equipped items saved. Slot 1: " + slot1Index + ", Slot 2: " + slot2Index);
    }

    // Load equipped items from PlayerPrefs
    public void LoadEquippedItems()
    {
        // Load saved slot indices (default to -1 if not found)
        int slot1Index = PlayerPrefs.GetInt("EquippedSlot1", -1);
        int slot2Index = PlayerPrefs.GetInt("EquippedSlot2", -1);

        // Load items if they have valid indices, or set to null if -1
        equippedSlot1Image.sprite = (slot1Index >= 0 && slot1Index < inventorySprites.Length) ? inventorySprites[slot1Index] : null;
        equippedSlot2Image.sprite = (slot2Index >= 0 && slot2Index < inventorySprites.Length) ? inventorySprites[slot2Index] : null;

        Debug.Log("Equipped items loaded. Slot 1: " + (equippedSlot1Image.sprite != null ? equippedSlot1Image.sprite.name : "None") +
                  ", Slot 2: " + (equippedSlot2Image.sprite != null ? equippedSlot2Image.sprite.name : "None"));
    }

    // Reset equipped items in both slots
    public void ResetEquippedItems()
    {
        equippedSlot1Image.sprite = null;  // Clear first slot
        equippedSlot2Image.sprite = null;  // Clear second slot
        Debug.Log("Equipped items have been reset.");
    }
}
