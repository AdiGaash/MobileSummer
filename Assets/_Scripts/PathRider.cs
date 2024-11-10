using System;
using System.Collections.Generic;
using PathCreation;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;


public class PathRider : MonoBehaviour
{
    public PlayerInPathMovement _playerInPathMovement;
    private VertexPath path;
    public float speed = 5;
    float distanceTravelled;
    float distanceToDecide = -1;
    private bool isMultiPath = false;
    private int currentChosenPath = 0;
    public float spooningDistance = 0.2f;
    float nextSpooning = 5f;
    
    public ObjectPoolManager poolManager; // Reference to the pooling manager

    private Dictionary<string, List<string>> objectNameByType = new Dictionary<string, List<string>>();
    
    // Reference to the ScriptableObject that holds the prefab list
    public PrefabListContainer prefabListContainer;

   
    
    void Start()
    {
        // Initialize the pools with the prefab list from the ScriptableObject
        poolManager.InitializePools(prefabListContainer.prefabList, prefabListContainer.NumObjectToInit);  // Preload with 5 instances of each prefab
        InitPoolNameType();
    }

    private void InitPoolNameType()
    {
        foreach (var prefab in prefabListContainer.prefabList)
        {
            string nameType = prefab.tag;
            if (!objectNameByType.ContainsKey(nameType))
                objectNameByType[nameType] = new List<string>();
            
            objectNameByType[nameType].Add(prefab.name);
        }
    }

    public void SetRidingPath(VertexPath _path, bool multipath,float _distanceToDecide, bool resetPos )
    {
        Debug.Log("SetRidingPath");
        isMultiPath = multipath;
        distanceToDecide = _distanceToDecide;
        path = _path;
        if (resetPos)
            distanceTravelled = 0;
    }
    
    
    void Update()
    {
        // path transform
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.Rotate(0,0,90);
        // choose path on fork
            if (isMultiPath)
            {
                if (distanceToDecide > distanceTravelled)
                    currentChosenPath = SetCurrentChosenPath();
                else
                {
                    isMultiPath = false;
                    Debug.Log("choose path: " + currentChosenPath);
                    PathPlanner.Instance.ChooseSpecificPath(currentChosenPath);
                }
            }
        // place elements
        Debug.Log("distanceTravelled: " + distanceTravelled);
        if (distanceTravelled > nextSpooning + spooningDistance)
        {
            nextSpooning = distanceTravelled + spooningDistance;
            PlaceSideElements();
           
        }
        


    }

    void PlaceSideElements()
    {
        if (Random.value > 0.2f)  // Random.value returns a float between 0.0 and 1.0
        {
            GameObject randomObject = GetRandomSideObjectFromPool();
            var pooledObject = randomObject.GetComponent<PooledObject>();
            if (randomObject != null)
            {
                Vector3 position = new Vector3();
                position = path.GetPointAtDistance(nextSpooning, EndOfPathInstruction.Stop);
                bool isLeft;
                isLeft = !(Random.value > 0.5f);
                var rotation = path.GetRotationAtDistance(nextSpooning, EndOfPathInstruction.Stop);
                pooledObject.SetTransform(position,rotation,1.6f,isLeft);
                
            }
        }
    }

  

    private GameObject GetRandomSideObjectFromPool()
    {

        List<string> listToChooseFrom;
        if (Random.value > 0.2f)
        {
             listToChooseFrom = objectNameByType["smallside"];
        }
        else
        { 
             listToChooseFrom = objectNameByType["bigside"];
        }
        int randomIndex = Random.Range(0, listToChooseFrom.Count);
        GameObject randomPrefab = poolManager.GetObject(listToChooseFrom[randomIndex]);
        return randomPrefab;
    }

    void PlaceObstacles()
    {
        
    }
    private int SetCurrentChosenPath()
    {
        if (_playerInPathMovement.currentPosition == PlayerPosition.Left ||
            _playerInPathMovement.currentPosition == PlayerPosition.LeftUp)
            return 0;
        if (_playerInPathMovement.currentPosition == PlayerPosition.Right ||
            _playerInPathMovement.currentPosition == PlayerPosition.RightUp)
            return 1;

        return -1;
    }
}
