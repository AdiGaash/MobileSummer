using System;
using UnityEngine;


public class RoadPart : MonoBehaviour
{
    public GameParameters GameParameters;

    private void Update()
    {
        transform.Translate(Vector3.back * GameParameters.speed);
    }
}
