using UnityEngine;
using DG.Tweening;


public class MoveObjectDOTween : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 10, 0); // Target position
    public float duration = 2.0f; // Duration of the movement

    void Start()
    {
        // Move the object to the target position over the duration
        transform.DOMove(targetPosition, duration);
        
    }
}
