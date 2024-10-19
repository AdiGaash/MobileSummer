using System;
using UnityEngine;
using System.Collections.Generic;
using PathCreation;

public class PathPlanner : Singleton<PathPlanner>
{
    public GameObject PathPrefab;

    public PathRider PathRider;
    private BezierPath lastBeizerPath;
    private List<VertexPath> pathsToRide;
    // for testings
    public List<Vector3> pointsA;
    public List<Vector3> pointsB;
    private bool multiPath = false;
    
    
    public GameObject AddPath(List<Vector3> pathPoints )
    {
        var pathGameObject = Instantiate(PathPrefab);
        pathGameObject.name = "Path";

        PathMaker pathMaker = pathGameObject.GetComponent<PathMaker>();
        
        pathMaker.points = pathPoints;
        pathMaker.Start();
        lastBeizerPath = pathMaker.bezierPath;
        pathsToRide.Add(pathMaker.vertexPath);
        multiPath = false;
        
        return pathGameObject;
    }


    public void AddSplitPaths(List<Vector3> path1Points, List<Vector3> path2Points )
    {
       
        var path1GameObject = AddPath(path1Points);
        path1GameObject.name = "Path1";

        var path2GameObject = AddPath(path2Points);
        path1GameObject.name = "Path2";


        multiPath = true;
    }


    private void Update()
    {
        // for testings

        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearPaths();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("create single path");
            ClearPaths();
            AddPath(pointsA);

        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("create multi path");
            ClearPaths();

            AddSplitPaths(pointsA, pointsB);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            float distanceToDecide = -1;
            if (multiPath)
            {
                 distanceToDecide = 
                     pathsToRide[0].GetClosestDistanceAlongPath(lastBeizerPath.GetPoint(7));
            }
            PathRider.SetRidingPath(pathsToRide[0], multiPath,distanceToDecide,true);
            PathRider.enabled = true;
            multiPath = false;
        }
    }

    public void ChooseSpecificPath(int pathNum)
    {
        PathRider.SetRidingPath(pathsToRide[pathNum], false, -1, false);
    }
    
    private void ClearPaths()
    {
        pathsToRide = new List<VertexPath>();
    }
}
