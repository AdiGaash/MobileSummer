using UnityEngine;



[CreateAssetMenu(fileName = "FILENAME", menuName = "GameParameters", order = 0)]
public class GameParameters : ScriptableObject
{
    public float speed;
    public bool finishTaskAtLevel1 = false;
}
