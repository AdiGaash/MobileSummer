using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
   
    public UnityEvent onTriggered;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TriggerSomething();
        }
    }

    // Call this method from another script or on collision etc.
    public void TriggerSomething()
    {
        Debug.Log("TriggerSomething() called!");

        // Call all the functions assigned in the Inspector
        if (onTriggered != null)
        {
            onTriggered.Invoke();
        }
    }
}