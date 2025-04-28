/*
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif
public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        #if UNITY_ANDROID
            CreateAndroidNotificationChannel();
            SendAndroidNotification("Title", "Message", 5); // Sends notification after 5 seconds
        #elif UNITY_IOS
            RequestIOSPermission();
            SendIOSNotification("Title", "Message", 5); // Sends notification after 5 seconds
        #endif
    }

    public bool SendNotification(string title, string text, int delaySeconds)
    {
#if UNITY_ANDROID
        SendAndroidNotification(title, text, delaySeconds);
        return true;
#elif UNITY_IOS
        SendIOSNotification( title,  body,  delaySeconds);
        return true;
#endif
        // as it not android or iOS - we cant send notification for mobile with the class
        // so we return false to show that nothing was sent
        return false;
    }

#if UNITY_ANDROID
    void CreateAndroidNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    void SendAndroidNotification(string title, string text, int delaySeconds)
    {
        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            FireTime = System.DateTime.Now.AddSeconds(delaySeconds),
        };
        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }
#elif UNITY_IOS
    void RequestIOSPermission()
    {
        iOSNotificationCenter.RequestAuthorization(
            AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound);
    }

    void SendIOSNotification(string title, string body, int delaySeconds)
    {
        var notification = new iOSNotification
        {
            Identifier = "_notification_01",
            Title = title,
            Body = body,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            Trigger = new iOSNotificationTimeIntervalTrigger
            {
                TimeInterval = new System.TimeSpan(0, 0, delaySeconds),
                Repeats = false
            },
        };
        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
    
    
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
#if UNITY_ANDROID
            var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();
            if (notificationIntentData != null)
            {
                // Handle notification tap here, for example: 
                
                AndroidNotificationIntentData data = notificationIntentData;

                // Access notification title, text, etc.
                string title = data.Notification.Title;
                string text = data.Notification.Text;

                // Example action: Display the notification data in the console
                Debug.Log("Notification Tapped - Title: " + title + ", Text: " + text);
            }
#elif UNITY_IOS
            iOSNotificationCenter.OnNotificationReceived += notification =>
            {
                // Handle notification tap here
            };
#endif
        }
    }
}
*/