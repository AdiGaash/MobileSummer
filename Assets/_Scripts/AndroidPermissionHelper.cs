using UnityEngine;
using UnityEngine.Android; // Only works on Android builds

public class AndroidPermissionHelper : MonoBehaviour
{
    /// <summary>
    /// Returns Android OS version (API level)
    /// </summary>
    private static int GetAndroidApiLevel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
#else
        return -1; // Not Android
#endif
    }

    /// <summary>
    /// Request camera and microphone permissions
    /// </summary>
    public static void RequestCameraAndMicrophone()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    /// <summary>
    /// Request location permissions (handles foreground/background)
    /// </summary>
    public static void RequestLocationPermissions()
    {
        int api = GetAndroidApiLevel();

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // Request background location only on Android 10+ and separately
        if (api >= 29 && !Permission.HasUserAuthorizedPermission("android.permission.ACCESS_BACKGROUND_LOCATION"))
        {
            Debug.Log("Requesting background location separately.");
            Permission.RequestUserPermission("android.permission.ACCESS_BACKGROUND_LOCATION");
        }
    }

    /// <summary>
    /// Request external storage permission (pre Android 11 only)
    /// </summary>
    public static void RequestStoragePermissions()
    {
        int api = GetAndroidApiLevel();

        if (api < 30)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
            }
        }
        else
        {
            Debug.Log("Scoped storage in Android 11+; use persistentDataPath instead.");
        }
    }

    /// <summary>
    /// Request notification permission (Android 13+)
    /// </summary>
    public static void RequestNotificationPermission()
    {
        int api = GetAndroidApiLevel();

        if (api >= 33 && !Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    /// <summary>
    /// Request Bluetooth / Nearby device permissions (Android 12+)
    /// </summary>
    public static void RequestNearbyPermissions()
    {
        int api = GetAndroidApiLevel();

        if (api >= 31)
        {
            if (!Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN"))
                Permission.RequestUserPermission("android.permission.BLUETOOTH_SCAN");

            if (!Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
                Permission.RequestUserPermission("android.permission.BLUETOOTH_CONNECT");

            if (!Permission.HasUserAuthorizedPermission("android.permission.NEARBY_WIFI_DEVICES"))
                Permission.RequestUserPermission("android.permission.NEARBY_WIFI_DEVICES");
        }
    }
}
