using UnityEngine;
using UnityEngine.Pool;

public class ObsticlePoolManager : MonoBehaviour
{
    // Pool for GameObjects
    private ObjectPool<GameObject> gameObjectPool;

    // The prefab for pooling
    public GameObject objectPrefab;

    // Pool size (optional)
    public int initialPoolSize = 10;

    private void Awake()
    {
        // Initialize the pool
        gameObjectPool = new ObjectPool<GameObject>(
            createFunc: CreatePooledItem,    // Function to create new objects when the pool is empty
            actionOnGet: OnTakeFromPool,     // What happens when an object is taken from the pool
            actionOnRelease: OnReturnedToPool, // What happens when an object is returned to the pool
            actionOnDestroy: OnDestroyPoolObject, // What happens when an object is destroyed
            collectionCheck: true,         // Optional: check for pool overflows
            defaultCapacity: initialPoolSize, // Initial capacity of the pool
            maxSize: 100 // Max pool size (optional)
        );

        // Prepopulate the pool with inactive objects
        PrepopulatePool(initialPoolSize);
    }

    // Prepopulate the pool with inactive objects
    private void PrepopulatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            // Return new objects to the pool to populate it
            GameObject obj = gameObjectPool.Get();
            obj.transform.parent = this.transform;
            gameObjectPool.Release(obj); // Immediately release it so the pool is filled
        }
    }

    // Function to create a new GameObject when the pool needs more
    private GameObject CreatePooledItem()
    {
        GameObject obj = Instantiate(objectPrefab);
        obj.SetActive(false); // Disable the object when it's created
        return obj;
    }

    // Function to handle what happens when an object is taken from the pool
    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true); // Enable the object when taken from the pool
    }

    // Function to handle what happens when an object is returned to the pool
    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false); // Disable the object when returned to the pool
    }

    // Function to destroy objects when they are no longer needed
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj); // Properly clean up the object
    }

    // Get a GameObject from the pool
    public GameObject Get()
    {
        return gameObjectPool.Get(); // Get an object from the pool
    }

    // Return a GameObject to the pool
    public void ReturnToPool(GameObject obj)
    {
        obj.transform.parent = this.transform;
        gameObjectPool.Release(obj); // Return an object to the pool
    }

    // OnDestroy to make sure the pool is cleaned up when this object is destroyed
    private void OnDestroy()
    {
        gameObjectPool.Clear(); // Clear the pool when the script is destroyed
    }
}
