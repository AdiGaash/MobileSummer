using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipParameters", menuName = "Scriptable Objects/ShipParameters")]
public class ShipParameters : ScriptableObject
{
    public float Speed = 5;

    private void OnValidate()
    {
        if (Speed < 6) Speed = 6;
    }
}
