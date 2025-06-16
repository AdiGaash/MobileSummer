using System.Collections;
using UnityEngine;
using System.IO;

// Serializable class for clean JSON parsing
[System.Serializable]
public class MobileConfig
{
    public float speed = 5f;
}

public class FixMobileScript : MonoBehaviour
{
    public Transform player;
    public string configFileName = "mobile_config.json";

    private Vector3 targetPosition;
    private float moveSpeed = 5f;

    private Camera mainCam;
    private string configPath;
    private MobileConfig cachedConfig;

    void Start()
    {
        targetPosition = player.position;

        // Cache references
        mainCam = Camera.main;
        configPath = Path.Combine(Application.streamingAssetsPath, configFileName);

        LoadConfig();
        StartCoroutine(CheckConfigChanges());
    }

    void Update()
    {
        Vector3 acc = Input.acceleration;
        
        // Clamp input to avoid runaway movement
        acc = Vector3.ClampMagnitude(acc, 1f);
        Vector3 move = new Vector3(acc.x, 0, acc.y) * moveSpeed;
        targetPosition += move * Time.deltaTime;

        // Smooth movement without GC allocations
        player.position = Vector3.MoveTowards(player.position, targetPosition, moveSpeed * Time.deltaTime);

        // Pre-cached camera reference used for animation
        float targetFOV = 60f + Mathf.Sin(Time.time * 5f) * 10f;
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFOV, 0.05f);

#if UNITY_EDITOR
        // Optional logging only in editor
        Debug.Log($"Pos: {player.position:F2} | Acc: {acc:F2}");
#endif
    }

    void LoadConfig()
    {
        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            cachedConfig = JsonUtility.FromJson<MobileConfig>(json);

            if (cachedConfig != null)
                moveSpeed = cachedConfig.speed;
        }
    }

    IEnumerator CheckConfigChanges()
    {
        while (true)
        {
            // Could be enhanced to check last write time if needed
            LoadConfig();
            yield return new WaitForSeconds(2f); // Less frequent reload
        }
    }
}
