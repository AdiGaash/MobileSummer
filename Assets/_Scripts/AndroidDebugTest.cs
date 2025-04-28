using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidDebugTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_ANDROID
        Debug.Log("Checking debug with unity remote on android");
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
