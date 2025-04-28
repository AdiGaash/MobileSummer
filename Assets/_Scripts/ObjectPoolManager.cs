using System.Collections.Generic;
using UnityEngine;

// Pool manager that handles different object types based on prefab list
public class ObjectPoolManager : MonoBehaviour
{
    // A dictionary to hold the pools for different object types
    private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();

    // Method to initialize pools based on a list of prefabs
    public void InitializePools(List<GameObject> prefabList, int initialPoolSize = 10)
    {
        // Loop through the prefabs and create a pool for each one
        foreach (var prefab in prefabList)
        {
            string poolKey = prefab.name;

            // Create the pool for this type of object if it doesn't exist
            if (!objectPools.ContainsKey(poolKey))
            {
                objectPools[poolKey] = new Queue<GameObject>();

                // Preload the pool with the initial number of objects
                for (int i = 0; i < initialPoolSize; i++)
                {
                    GameObject newObj = Instantiate(prefab, transform);
                    newObj.SetActive(false);  // Start with deactivated objects
                    objectPools[poolKey].Enqueue(newObj);
                }
            }
        }
    }

    // Method to get an object from the pool
    public GameObject GetObject(string poolKey)
    {
        
        // If there is no pool for this object type, create one dynamically
        if (!objectPools.ContainsKey(poolKey))
        {
            objectPools[poolKey] = new Queue<GameObject>();
        }

        // Check if the pool has any available objects
        if (objectPools[poolKey].Count > 0)
        {
            GameObject pooledObject = objectPools[poolKey].Dequeue();
            pooledObject.SetActive(true);  // Activate the object before using it
            return pooledObject;
        }
        else
        {
           Debug.Log("no available with that tag: " + poolKey + " found");
            return null;
        }
    }

    // Method to return an object to the pool
    public void ReturnObject(GameObject obj)
    {
        string poolKey = obj.name;

        // Deactivate the object and return it to its pool
        obj.SetActive(false);

        // Ensure the pool exists for this type of object
        if (!objectPools.ContainsKey(poolKey))
        {
            objectPools[poolKey] = new Queue<GameObject>();
        }

        objectPools[poolKey].Enqueue(obj);
    }
}
