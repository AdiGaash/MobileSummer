using Unity.Mathematics;
using UnityEngine;

namespace PathCreation.Examples
{
    public class PathFollower : MonoBehaviour
    {
        private SectionPathPlacer _sectionPathPlacer;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public float DistanceModifier = -5;
        float distanceTravelled;
        private float distanceToNextPoint;
        private float distanceLookAt;
        private int nextPoint = 3;
        public bool hasPassedPoint = false;  // Flag to ensure point is only passed once.
        public Transform CameraTransform;
        private Vector3 cameraPos;
        private bool setCamera = false;
        public float spacing = 0.5f;

        // Reference to the ScriptableObject
        public CameraSettings cameraSettings; 

        void Start()
        {
            _sectionPathPlacer = GetComponent<SectionPathPlacer>();
            if (pathCreator != null)
            {
                // Subscribe to the pathUpdated event so that we're notified if the path changes during the game
                //pathCreator.pathUpdated += OnPathChanged;
            }

            distanceToNextPoint = GetDistanceToNextPoint();

            setCamera = CameraTransform != null;
        }


        void PositionCamera()
        {
            {
                // Use the offset from the ScriptableObject
                cameraPos = pathCreator.path.GetPointAtDistance(distanceTravelled - cameraSettings.lookAtDistanceOffset, endOfPathInstruction);

                CameraTransform.position = cameraPos;

                distanceLookAt = (speed + 1) * Time.deltaTime;

                CameraTransform.LookAt(pathCreator.path.GetPointAtDistance(distanceLookAt, endOfPathInstruction));
                CameraTransform.Rotate(cameraSettings.rotationOffset);  // Apply the rotation offset
                CameraTransform.Translate(cameraSettings.positionOffset);  // Apply the position offset
            }
        }

        void AddObjectsToPath()
        {
            
        }
        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.Translate(0,1,0);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled - 1, endOfPathInstruction);
                transform.Rotate(0,0,90);
                if (setCamera)
                    PositionCamera();

                AddObjectsToPath();
            }

            // Check if player passed the next point only if they haven't already
            if (!hasPassedPoint && PlayerPassedNextPoint())
            {
                nextPoint += 3;
                distanceToNextPoint = GetDistanceToNextPoint();
                Debug.Log("Player passed the next point. next point: " + nextPoint);
                hasPassedPoint = true;  // Set flag to true so it won't be checked again.
                UpdatePathObjects();

            }
        }

        private void UpdatePathObjects()
        {
            _sectionPathPlacer.Generate(distanceTravelled, distanceToNextPoint, spacing);
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        public void OnPathChanged()
        {
            // Reset the flag so the next point can be checked
            hasPassedPoint = false;
        }

        bool PlayerPassedNextPoint()
        {
            // Check if the distance travelled by the player exceeds the distance to the next point
            return distanceTravelled + DistanceModifier > distanceToNextPoint;
        }

        float GetDistanceToNextPoint()
        {
            // Retrieve the actual distance to the next point
            return pathCreator.path.GetClosestDistanceAlongPath(pathCreator.bezierPath.GetPoint(nextPoint)); // Get next waypoint distance
        }
        
    }
}
