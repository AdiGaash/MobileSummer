using UnityEngine;

[System.Serializable]
public class InteractiveObjectData
{
    public string objectName;
    public Vector3 initialPosition;

    public InteractiveObjectData(string name, Vector3 position)
    {
        objectName = name;
        initialPosition = position;
    }
}