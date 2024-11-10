using System.Collections.Generic;
using UnityEngine;

// ScriptableObject to store the list of prefabs
[CreateAssetMenu(fileName = "PrefabListContainer", menuName = "ScriptableObjects/PrefabListContainer", order = 1)]
public class PrefabListContainer : ScriptableObject
{
    // List to store your prefab references
    public List<GameObject> prefabList;
    public int NumObjectToInit = 10;
}