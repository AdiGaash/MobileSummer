using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Class to store interactive object data

// Create a wrapper class if needed for proper JSON serialization


public class StarMovmentManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool enableDebugLogs = true;
    [SerializeField] private LayerMask raycastLayerMask = -1;
    [SerializeField] private float maxRaycastDistance = 100f;
    [SerializeField] private float faceDownThreshold = 0.8f; // Threshold for detecting face-down orientation
    
    private GameObject selectedObject;
    private List<Vector3> drawnPath = new List<Vector3>();
    private bool isDrawing = false;
    private bool isFollowingPath = false;
    private int currentPathIndex = 0;
    private bool isCurrentlyFaceDown = false;
    
    // Dictionary to store interactive objects data (game object reference -> data)
    private Dictionary<GameObject, InteractiveObjectData> interactiveObjectsData = new Dictionary<GameObject, InteractiveObjectData>();
    
    // List to store all interactive object data (useful for persistence/serialization)
    private List<InteractiveObjectData> allInteractiveObjectsData = new List<InteractiveObjectData>();
    
    void Start()
    {
        LoadInteractiveObjectsFromJson(); // Load any previously saved data
        InitDrawPath();
    }

    public void StoreInteractiveObjectsPositions()
    {
        // Find all objects with the "InteractiveObjects" tag and store their positions
        GameObject[] interactiveObjects = GameObject.FindGameObjectsWithTag("InteractiveObject");
        
        foreach (GameObject obj in interactiveObjects)
        {
            // Create data object to store name and position
            InteractiveObjectData data = new InteractiveObjectData(obj.name, obj.transform.position);
            
            // Store in dictionary for quick lookup by GameObject reference
            interactiveObjectsData[obj] = data;
            
            // Add to list for persistence
            allInteractiveObjectsData.Add(data);
            
            DebugLog($"Stored initial position for {data.objectName}: {data.initialPosition}");
        }
        
        DebugLog($"Stored positions for {interactiveObjectsData.Count} interactive objects");
        SaveInteractiveObjectsToJson();
        LoadInteractiveObjectsFromJson();
    }

    private void InitDrawPath()
    {
        // Initialize the drawn path
        drawnPath.Clear();
        isDrawing = false;
        isFollowingPath = false;
        currentPathIndex = 0;
        
        DebugLog("StarMovementManager initialized");
    }

    void Update()
    {
        // Check device orientation
        CheckDeviceOrientation();
        
        // Rest of the code remains the same
        // Check if we have at least one touch
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch firstTouch = Input.GetTouch(0);
            
            // If first touch just began
            if (firstTouch.phase == TouchPhase.Began)
            {
                DebugLog($"First touch began at: {firstTouch.position}");
                
                // Reset path and state
                drawnPath.Clear();
                isDrawing = false;
                isFollowingPath = false;
                currentPathIndex = 0;
                
                // Try to select an object with the first touch
                SelectObject(firstTouch.position);
            }
            else if (firstTouch.phase == TouchPhase.Moved)
            {
                DebugLog($"First touch moved to: {firstTouch.position}");
            }
            else if (firstTouch.phase == TouchPhase.Ended || firstTouch.phase == TouchPhase.Canceled)
            {
                DebugLog($"First touch ended at: {firstTouch.position}");
                
                // If not already following, start following
                if (!isFollowingPath && drawnPath.Count > 0 && selectedObject != null)
                {
                    DebugLog($"Starting to follow path with {drawnPath.Count} points");
                    isFollowingPath = true;
                    currentPathIndex = 0;
                }
            }
            
            // If we have a second touch for drawing
            if (Input.touchCount > 1 && selectedObject != null)
            {
                Touch secondTouch = Input.GetTouch(1);
                
                // If second touch just began
                if (secondTouch.phase == TouchPhase.Began)
                {
                    DebugLog($"Second touch began at: {secondTouch.position}");
                    
                    // Start drawing
                    isDrawing = true;
                    drawnPath.Clear();
                    
                    // Add first point
                    Vector3 worldPoint = GetWorldPositionFromTouch(secondTouch.position);
                    
                    drawnPath.Add(worldPoint);
                    DebugLog($"Added first path point at world position: {worldPoint}");
                }
                
                // If second touch is moving
                if (secondTouch.phase == TouchPhase.Moved && isDrawing)
                {
                    // Add point to path
                    Vector3 worldPoint = GetWorldPositionFromTouch(secondTouch.position);
                    
                    // Only add points if they're far enough from the last point
                    if (drawnPath.Count == 0 || Vector3.Distance(worldPoint, drawnPath[drawnPath.Count - 1]) > 0.1f)
                    {
                        drawnPath.Add(worldPoint);
                        DebugLog($"Added path point #{drawnPath.Count} at world position: {worldPoint}");
                    }
                }
                
                // If second touch ended or was canceled
                if ((secondTouch.phase == TouchPhase.Ended || secondTouch.phase == TouchPhase.Canceled) && isDrawing)
                {
                    DebugLog($"Second touch ended at: {secondTouch.position}");
                    
                    // Stop drawing
                    isDrawing = false;
                    
                    // Start following if we have points
                    if (drawnPath.Count > 0)
                    {
                        DebugLog($"Drawing complete. Path has {drawnPath.Count} points");
                        if (firstTouch.phase == TouchPhase.Ended || firstTouch.phase == TouchPhase.Canceled)
                        {
                            DebugLog("Both touches ended, starting to follow path");
                            isFollowingPath = true;
                            currentPathIndex = 0;
                        }
                        else
                        {
                            DebugLog("Waiting for first touch to end before following path");
                        }
                    }
                    else
                    {
                        DebugLog("Drawing ended but no path points were recorded");
                    }
                }
            }
        }
        
        // Handle object following the path
        if (isFollowingPath && selectedObject != null)
        {
            FollowPath();
        }
        
        // Debug touch count
        if (Input.touchCount > 0 && enableDebugLogs)
        {
            DebugLog($"Current touch count: {Input.touchCount}");
        }
    }
    
    private void CheckDeviceOrientation()
    {
        // Read accelerometer data
        Vector3 acceleration = Input.acceleration;
        
        bool isFacingDown = acceleration.z > faceDownThreshold;

        // Check if the phone is facing down (y-axis points downward)
        if (isFacingDown && !isCurrentlyFaceDown)

        {
            DebugLog("Device is facing down. Resetting interactive objects and path.");
            DebugLog(acceleration.ToString());
            // Reset the drawn path
            drawnPath.Clear();
            isDrawing = false;
            isFollowingPath = false;
            currentPathIndex = 0;
            
            // Reset all interactive objects to their initial positions
            ResetInteractiveObjectsPositions();
        }
        // Update the current state
        isCurrentlyFaceDown = isFacingDown;

    }
    
    private void ResetInteractiveObjectsPositions()
    {
        // Reset using the dictionary (more efficient for direct object access)
        foreach (var entry in interactiveObjectsData)
        {
            GameObject obj = entry.Key;
            InteractiveObjectData data = entry.Value;
            
            if (obj != null) // Check if the object still exists
            {
                obj.transform.position = data.initialPosition;
                DebugLog($"Reset {data.objectName} to initial position: {data.initialPosition}");
            }
        }
    }
    
    // Method to get object data for external access if needed
    public List<InteractiveObjectData> GetAllInteractiveObjectsData()
    {
        return new List<InteractiveObjectData>(allInteractiveObjectsData);
    }
    
    private void SelectObject(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, raycastLayerMask))
        {
            selectedObject = hit.collider.gameObject;
            DebugLog($"Selected 3D object: {selectedObject.name} at position {selectedObject.transform.position}");
            DebugLog($"Hit point: {hit.point}, Hit normal: {hit.normal}, Hit distance: {hit.distance}");
        }
        else
        {
            selectedObject = null;
            DebugLog("No object was selected at touch position. Raycast didn't hit anything.");
        }
    }
    
    private Vector3 GetWorldPositionFromTouch(Vector2 touchPosition)
    {
        // First, try to raycast against objects in the scene
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, raycastLayerMask))
        {
            DebugLog($"Path point raycast hit object: {hit.collider.name} at point {hit.point}");
            return hit.point;
        }
        else
        {
            // If no hit, use a fixed distance from camera
            // You might want to adjust this based on your scene
            float distanceFromCamera = 10f;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(
                touchPosition.x, 
                touchPosition.y, 
                distanceFromCamera));
            
            DebugLog($"Path point using fixed distance: {worldPoint}");
            return worldPoint;
        }
    }
    
    private void FollowPath()
    {
        if (currentPathIndex >= drawnPath.Count)
        {
            // We've reached the end of the path
            DebugLog("Reached the end of the path");
            isFollowingPath = false;
            return;
        }
        
        // Move towards the current target point
        Vector3 targetPosition = drawnPath[currentPathIndex];
        Vector3 startPosition = selectedObject.transform.position;
        float step = moveSpeed * Time.deltaTime;
        selectedObject.transform.position = Vector3.MoveTowards(
            startPosition, 
            targetPosition, 
            step);
        
        // Check if we've reached the current target point
        if (Vector3.Distance(selectedObject.transform.position, targetPosition) < 0.01f)
        {
            DebugLog($"Reached path point #{currentPathIndex} at {targetPosition}");
            currentPathIndex++;
            
            if (currentPathIndex < drawnPath.Count)
            {
                DebugLog($"Moving to next point #{currentPathIndex} at {drawnPath[currentPathIndex]}");
            }
        }
    }
    
    // Helper method for conditional debug logging
    private void DebugLog(string message)
    {
        if (enableDebugLogs)
        {
            Debug.Log($"[StarMovementManager] {message}");
        }
    }
    
    // Visual debugging for the drawn path
    private void OnDrawGizmos()
    {
        if (drawnPath.Count > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < drawnPath.Count - 1; i++)
            {
                Gizmos.DrawLine(drawnPath[i], drawnPath[i + 1]);
            }
            
            // Draw spheres at each point
            Gizmos.color = Color.yellow;
            foreach (Vector3 point in drawnPath)
            {
                Gizmos.DrawSphere(point, 0.05f);
            }
            
            // Highlight the current target point
            if (isFollowingPath && currentPathIndex < drawnPath.Count)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(drawnPath[currentPathIndex], 0.1f);
            }
        }
    }
    
    void SaveInteractiveObjectsToJson()
    {
        // Create the wrapper and assign the data
        InteractiveObjectsDataWrapper wrapper = new InteractiveObjectsDataWrapper
        {
            objects = allInteractiveObjectsData
        };

        // Convert to JSON
        string json = JsonUtility.ToJson(wrapper, true); // true for pretty print

        // Define the path for the JSON file (in persistent data path)
        string filePath = Path.Combine(Application.persistentDataPath, "interactive_objects_data.json");

        // Write to file
        File.WriteAllText(filePath, json);
        
        DebugLog($"Saved {allInteractiveObjectsData.Count} interactive objects data to: {filePath}");
    }
    
    public void LoadInteractiveObjectsFromJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "interactive_objects_data.json");
        
        if (File.Exists(filePath))
        {
            // Read the JSON from file
            string json = File.ReadAllText(filePath);
            
            DebugLog($"Loading interactive objects data from: {filePath}");
            
            // Deserialize the JSON
            InteractiveObjectsDataWrapper wrapper = JsonUtility.FromJson<InteractiveObjectsDataWrapper>(json);
            
            if (wrapper != null && wrapper.objects != null)
            {
                // Replace the current list with loaded data
                allInteractiveObjectsData = wrapper.objects;
                
                // Rebuild the dictionary
                interactiveObjectsData.Clear();
                
                // Find all objects with the tag and match them with the loaded data
                GameObject[] interactiveObjects = GameObject.FindGameObjectsWithTag("InteractiveObject");
                
                foreach (GameObject obj in interactiveObjects)
                {
                    // Find matching data by name
                    InteractiveObjectData matchingData = allInteractiveObjectsData.Find(data => data.objectName == obj.name);
                    
                    if (matchingData != null)
                    {
                        interactiveObjectsData[obj] = matchingData;
                    }
                }
                
                DebugLog($"Loaded {allInteractiveObjectsData.Count} interactive objects data from: {filePath}");
            }
        }
        else
        {
            DebugLog("No saved interactive objects data found.");
        }
    }
}