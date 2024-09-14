using UnityEngine;
using PathCreation;  // Assuming you're using the PathCreation plugin.

namespace PathCreation.Examples {
    // Example of creating and updating a path dynamically at runtime.

    [RequireComponent(typeof(PathCreator))]
    public class DynamicBezierPath : MonoBehaviour
    {
        private PathFollower _pathFollower;
        public bool closedLoop = false;  // We don't want a closed loop for endless generation.
        public Transform[] waypoints;
        private BezierPath bezierPath;
        public GameObject holder;
        public float dynamicXPos;
        public float dynamicYPos;
        

       

        void Start ()
        {
            _pathFollower = FindObjectOfType<PathFollower>();
            
            if (waypoints.Length >= 5) {
                // Create a new Bezier path from the initial waypoints.
                bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
            }
        }

        void Update () 
        {
            // Check if the player passed the second point
            //TODO: change this to camera pos as I want the  old part of the path destroy\get back to the pool after its behind the camera...
            
            if (_pathFollower.PlayerPassedSecondPoint())
            {
                // Remove the first point and add a new one after the last point.
                UpdatePath();
                
                // Notify the path follower that the path has changed
                _pathFollower.OnPathChanged();
            }
        }

        void UpdatePath() {
            // Remove the first waypoint
            RemoveFirstWaypoint();

            // Add a new waypoint after the last one
            AddNewWaypoint();

            // Update the Bezier path with the new waypoints
            bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
            GetComponent<PathCreator>().bezierPath = bezierPath;
        }

        void RemoveFirstWaypoint() {
            // Destroy the first waypoint GameObject
            Destroy(waypoints[0].gameObject);

            // Shift the waypoints array by removing the first point
            for (int i = 1; i < waypoints.Length; i++) {
                waypoints[i - 1] = waypoints[i];
            }

            // Nullify the last entry in the array, which is now duplicated
            waypoints[waypoints.Length - 1] = null;
        }

        void AddNewWaypoint() {
            // Generate a new position for the next point.
            Vector3 lastPointPosition = waypoints[waypoints.Length - 2].position;
            Vector3 newPointPosition = lastPointPosition + new Vector3(Random.Range(-1*dynamicXPos, dynamicXPos),
                Random.Range(-1*dynamicYPos, dynamicYPos), 5f+Random.Range(0, 10f)); 
            
            // Create a new GameObject for the new waypoint and set its position
            GameObject newWaypoint = new GameObject("NewWaypoint");
            newWaypoint.transform.position = newPointPosition;
            newWaypoint.transform.parent = holder.transform;

            // Add the new waypoint to the end of the waypoints array
            waypoints[waypoints.Length - 1] = newWaypoint.transform;
        }
    }
}
