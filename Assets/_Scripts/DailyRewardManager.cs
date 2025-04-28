using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewardManager : MonoBehaviour
{
    public Text rewardText;
    public Button claimButton;
    
    private int currentStreak;
    private DateTime lastLoginDate;

    void Start()
    {
        // Load last login date and streak from PlayerPrefs
        if (PlayerPrefs.HasKey("LastLoginDate"))
        {
            lastLoginDate = DateTime.Parse(PlayerPrefs.GetString("LastLoginDate"));
            currentStreak = PlayerPrefs.GetInt("CurrentStreak");
        }
        else
        {
            lastLoginDate = DateTime.MinValue;
            currentStreak = 0;
        }

        CheckDailyReward();
    }

    void CheckDailyReward()
    {
        DateTime currentDate = DateTime.Now.Date;

        if (lastLoginDate == DateTime.MinValue || (currentDate - lastLoginDate).Days > 1)
        {
            // Missed a day, reset streak
            currentStreak = 0;
        }

        if ((currentDate - lastLoginDate).Days >= 1)
        {
            // New day, increment streak
            currentStreak++;
            rewardText.text = "Day " + currentStreak + " reward: " + GetRewardForDay(currentStreak);
            claimButton.interactable = true;
        }
        else
        {
            // Already claimed today's reward
            rewardText.text = "Come back tomorrow for your next reward!";
            claimButton.interactable = false;
        }
    }

    public void ClaimReward()
    {
        // Give reward to the player
        string reward = GetRewardForDay(currentStreak);
        Debug.Log("Claimed reward: " + reward);
        
        // Update last login date
        lastLoginDate = DateTime.Now.Date;
        PlayerPrefs.SetString("LastLoginDate", lastLoginDate.ToString());
        PlayerPrefs.SetInt("CurrentStreak", currentStreak);

        // Disable the claim button until next day
        claimButton.interactable = false;
        rewardText.text = "Come back tomorrow for your next reward!";
    }

    string GetRewardForDay(int day)
    {
        // Define rewards for each day, you can customize this
        switch (day)
        {
            case 1: return "100 Coins";
            case 2: return "200 Coins";
            case 3: return "1 Power-Up";
            case 4: return "300 Coins";
            case 5: return "1 Special Item";
            default: return "500 Coins";
        }
    }
}