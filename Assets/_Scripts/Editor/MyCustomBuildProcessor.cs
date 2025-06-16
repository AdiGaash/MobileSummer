using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class MyCustomBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("MyCustomBuildProcessor.OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
        
        // Check if we're building for Android
        if (report.summary.platform == BuildTarget.Android)
        {
            IncrementAndroidVersion();
        }
    }
    
    private void IncrementAndroidVersion()
    {
        // Get the current bundle version code (Android)
        int bundleVersionCode = PlayerSettings.Android.bundleVersionCode;
        
        // Increment the version code
        bundleVersionCode++;
        
        // Set the new version code
        PlayerSettings.Android.bundleVersionCode = bundleVersionCode;
        
        // Optionally update the version name (semantic versioning)
        // This is what users see, typically in format like "1.2.3"
        // You could implement your own logic for updating this
        string currentVersionName = PlayerSettings.bundleVersion;
        // Example: Split by dots and increment last number
        string[] versionParts = currentVersionName.Split('.');
        if (versionParts.Length > 0 && int.TryParse(versionParts[versionParts.Length - 1], out int patchVersion))
        {
            patchVersion++;
            versionParts[versionParts.Length - 1] = patchVersion.ToString();
            string newVersionName = string.Join(".", versionParts);
            PlayerSettings.bundleVersion = newVersionName;
        }
        
        Debug.Log($"Android version updated - Code: {bundleVersionCode}, Name: {PlayerSettings.bundleVersion}");
    }
}