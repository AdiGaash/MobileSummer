using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HiddenInefficiencyMobileScript : MonoBehaviour
{
    public Transform player;
    public string configFileName = "mobile_config.json";
    private Vector3 targetPosition;
    private float moveSpeed = 5f;

    void Start()
    {
        StartCoroutine(ConfigCheck());
    }

    void Update()
    {
        Vector3 acc = Input.acceleration;
        Vector3 move = new Vector3(acc.x, 0, acc.y) * moveSpeed;
        targetPosition += move;

        player.position = Vector3.Lerp(player.position, targetPosition, Time.deltaTime);

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f + Mathf.Sin(Time.time * 5f) * 10f, 0.05f);

        string debugOutput = "Position:" + player.position.ToString() + " | Acc:" + acc.ToString();
        Debug.Log(debugOutput);
    }

    IEnumerator ConfigCheck()
    {
        while (true)
        {
            string configPath = Path.Combine(Application.streamingAssetsPath, configFileName);
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                Dictionary<string, string> parsed = JsonUtility.FromJson<Dictionary<string, string>>(json);
                if (parsed.ContainsKey("speed"))
                {
                    float.TryParse(parsed["speed"], out moveSpeed);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}