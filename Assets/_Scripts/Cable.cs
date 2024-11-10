using System.Collections;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public Transform object1;  // The first mesh (starting point)
    public Transform object2;  // The second mesh (end point)
    
    public int segmentCount = 10;  // Number of segments for the cable
    public GameObject segmentPrefab;  // Prefab for each segment (e.g., a small capsule or cylinder)
    
    private LineRenderer lineRenderer;
    private GameObject[] segments;

    void Start()
    {
        // Initialize the LineRenderer
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;

        // Create the cable segments
        segments = new GameObject[segmentCount];
        Vector3 segmentPosition = object1.position;

        for (int i = 0; i < segmentCount; i++)
        {
            // Instantiate segments and add Rigidbody + Joint components
            segments[i] = Instantiate(segmentPrefab, segmentPosition, Quaternion.identity);
            Rigidbody rb = segments[i].AddComponent<Rigidbody>();
            rb.mass = 0.001f;
            rb.angularDrag = 0;
            rb.useGravity = false;
            
            
            SpringJoint joint = segments[i].AddComponent<SpringJoint>();

            if (i > 0)
            {
                // Connect this segment to the previous one
                joint.connectedBody = segments[i - 1].GetComponent<Rigidbody>();
            }
            else
            {
                // The first segment connects to object1
                joint.connectedBody = object1.GetComponent<Rigidbody>();
            }

            // **Limit the cable stretching** by reducing maxDistance and increasing spring
            joint.spring = 0.001f;  // Increase spring value to reduce stretching
            joint.damper = 0.01f;
            joint.minDistance = 0.0001f;
            joint.maxDistance = 0.005f;
            joint.massScale = 0.1f;
            joint.connectedMassScale = 1f;// Reduce maxDistance to limit stretching

            // Spread the segments evenly between object1 and object2
            segmentPosition = Vector3.Lerp(object1.position, object2.position, (i + 1) / (float)segmentCount);
        }

        // Connect the last segment to object2
        SpringJoint lastJoint = segments[segmentCount - 1].AddComponent<SpringJoint>();
        lastJoint.connectedBody = object2.GetComponent<Rigidbody>();
/*
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < segmentCount; i++)
        {
            segments[i].GetComponent<Rigidbody>().velocity = Vector3.one;
        }
        */
    }

    void Update()
    {
        // Ensure the first and last segment positions stay attached to object1 and object2
        segments[0].transform.position = object1.position;
        segments[segmentCount - 1].transform.position = object2.position;

        // Update LineRenderer positions to match the cable segments
        for (int i = 0; i < segmentCount; i++)
        {
            lineRenderer.SetPosition(i, segments[i].transform.position);
        }
    }
}
