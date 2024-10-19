using System;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;


public class PathMaker : MonoBehaviour
{
    public BezierPath bezierPath;
    public List<Vector3> points = new List<Vector3>();
    public VertexPath vertexPath;
    public Color GizmoPathColor= Color.green;
    public void Start()
    {
        if (points.Count < 3)
        {
            points = new List<Vector3>();
            points.Add(Vector3.zero);
            points.Add(Vector3.zero);
            points.Add(Vector3.zero);
        }

        bezierPath = new BezierPath(points, false, PathSpace.xyz);
        vertexPath = new VertexPath(bezierPath, transform);
    }
    
    
#if UNITY_EDITOR
    void OnDrawGizmos () 
    {
        // Only draw path gizmo if the path object is not selected
        // (editor script is resposible for drawing when selected)
        
        if (vertexPath != null)
        {

            Gizmos.color = GizmoPathColor;

                for (int i = 0; i < vertexPath.NumPoints; i++) {
                    int nextI = i + 1;
                    if (nextI >= vertexPath.NumPoints) {
                        if (vertexPath.isClosedLoop) {
                            nextI %= vertexPath.NumPoints;
                        } else {
                            break;
                        }
                    }
                    Gizmos.DrawLine (vertexPath.GetPoint (i), vertexPath.GetPoint (nextI));
                }
        }
        
    }
#endif
    
}

