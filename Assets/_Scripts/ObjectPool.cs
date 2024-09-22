using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    private List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    // Retrieve an object from the pool
    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive objects are available, instantiate a new one (optional)
        GameObject newObj = Instantiate(prefab, position, rotation);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    // Return an object to the pool (deactivate it)
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}