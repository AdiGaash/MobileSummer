/*
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif


public class DailyNotificationManager : MonoBehaviour
{
    void Start()
    {
        #if UNITY_ANDROID
            CreateAndroidNotificationChannel();
            ScheduleDailyAndroidNotification("Daily Reminder", "This is your daily notification!");
        #elif UNITY_IOS
            RequestIOSPermission();
            ScheduleDailyIOSNotification("Daily Reminder", "This is your daily notification!");
        #endif
    }
    
    
#if UNITY_ANDROID
    void CreateAndroidNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "daily_channel",
            Name = "Daily Notifications",
            Importance = Importance.Default,
            Description = "Daily reminders and notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
    
    void ScheduleDailyAndroidNotification(string title, string text)
    {
        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            FireTime = System.DateTime.Now.AddDays(1).Date.AddHours(9), // Adjust time as needed
            RepeatInterval = new System.TimeSpan(24, 0, 0) // Repeat every 24 hours
        };
        AndroidNotificationCenter.SendNotification(notification, "daily_channel");
    }
    
#elif UNITY_IOS
    void RequestIOSPermission()
    {
        iOSNotificationCenter.RequestAuthorization(
            AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound);
    }

    void ScheduleDailyIOSNotification(string title, string body)
    {
        var notification = new iOSNotification
        {
            Identifier = "_daily_notification",
            Title = title,
            Body = body,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            Trigger = new iOSNotificationTimeIntervalTrigger
            {
                TimeInterval = new System.TimeSpan(24, 0, 0), // Repeat every 24 hours
                Repeats = true
            },
        };
        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}
*/