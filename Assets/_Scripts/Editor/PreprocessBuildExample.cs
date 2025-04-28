using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreprocessBuildExample : IPreprocessBuildWithReport
{
    // This property specifies the order in which preprocessors are executed.
    // Lower values are executed before higher values.
    public int callbackOrder
    {
        get { return 0; }
    }

    // This method is called before the build process starts.
    // The 'report' parameter provides information about the build.
    public void OnPreprocessBuild(BuildReport report)
    {
        // Perform your preprocessing tasks here.
        // For example, you could check for missing assets, validate project settings, check every scene,etc.
        Debug.Log("Preprocessing build...");
        ValidateSettings();
        EnsureAssetsExist();

        CheckEveryScene();
    }

    // Example for check every scene - change it and save it 
    void CheckEveryScene()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (var scene in scenes)
        {
            var path = scene.path;
           
            bool didFixThisScene = false;
            Debug.Log("Scene checking: " + scene);
            // load relevant scene from build setting
            EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            // do something in a scene 
            
            
            // save scene
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), path);
                
        }
        AssetDatabase.SaveAssets();
    }
    
    
    // Example method to validate project settings
    private void ValidateSettings()
    {
        // Add your validation logic here
        Debug.Log("Validating project settings...");
    }

    // Example method to ensure certain assets exist
    private void EnsureAssetsExist()
    {
        // Add your asset checking logic here
        Debug.Log("Checking for required assets...");
    }
}