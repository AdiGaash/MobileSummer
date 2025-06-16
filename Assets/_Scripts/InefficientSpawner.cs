using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InefficientSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Text fpsText;

    private List<GameObject> allSpawnedObjects = new List<GameObject>();

    void Start()
    {
        // Spawns 1000 objects immediately at startup
        for (int i = 0; i < 1000; i++)
        {
            GameObject obj = Instantiate(objectToSpawn, Random.insideUnitSphere * 10, Quaternion.identity);
            obj.transform.localScale = new Vector3(Random.value, Random.value, Random.value); // Different sizes
            allSpawnedObjects.Add(obj);
        }
    }

    void Update()
    {
        // Moves all objects every frame, using Find
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Untagged"))
        {
            obj.transform.Rotate(Vector3.up * Time.deltaTime * 30f);
        }

        // Shows FPS every frame, uses string concatenation inside UI update
        float fps = 1.0f / Time.deltaTime;
        fpsText.text = "Current FPS: " + fps;
    }

    void OnGUI()
    {
        // Creates a new GUI button every frame
        if (GUI.Button(new Rect(10, 10, 200, 30), "Destroy All Objects"))
        {
            foreach (GameObject obj in allSpawnedObjects)
            {
                Destroy(obj);
            }
            allSpawnedObjects.Clear();
        }
    }
}