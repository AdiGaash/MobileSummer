using System;
using PathCreation;
using UnityEditor.Rendering;
using UnityEngine;


public class PathRider : MonoBehaviour
{
    public PlayerInPathMovement _playerInPathMovement;
    private VertexPath path;
    public float speed = 5;
    float distanceTravelled;
    float distanceToDecide = -1;
    private bool isMultiPath = false;
    private int currentChosenPath = 0;

   

    public void SetRidingPath(VertexPath _path, bool multipath,float _distanceToDecide, bool resetPos )
    {
        Debug.Log("SetRidingPath");
        isMultiPath = multipath;
        distanceToDecide = _distanceToDecide;
        path = _path;
        if (resetPos)
            distanceTravelled = 0;
    }
    
    
    void Update()
    {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.Rotate(0,0,90);
            if (isMultiPath)
            {
                if (distanceToDecide > distanceTravelled)
                    currentChosenPath = SetCurrentChosenPath();
                else
                {
                    isMultiPath = false;
                    Debug.Log("choose path: " + currentChosenPath);
                    PathPlanner.Instance.ChooseSpecificPath(currentChosenPath);
                }
            }

         
    }

    private int SetCurrentChosenPath()
    {
        if (_playerInPathMovement.currentPosition == PlayerPosition.Left ||
            _playerInPathMovement.currentPosition == PlayerPosition.LeftUp)
            return 0;
        if (_playerInPathMovement.currentPosition == PlayerPosition.Right ||
            _playerInPathMovement.currentPosition == PlayerPosition.RightUp)
            return 1;

        return -1;

    }
}
