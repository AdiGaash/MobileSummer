using PathCreation;
using UnityEngine;

namespace PathCreation.Examples
{
    [ExecuteInEditMode]
    public class SectionPathPlacer : MonoBehaviour
    {
        public PathCreator pathCreator;
        public ObjectPool objectPool; // Reference to the object pool
        public GameObject holder;
        public GameObject prefab;
        

        const float minSpacing = .1f;

        // Public Generate method that accepts start and end percentage for path placement
        public void Generate(float startDistance, float endDistance, float spacing) 
        {
            if (pathCreator != null && prefab != null && holder != null) 
            {
                RemoveObjects(); // Use object pool to deactivate objects instead of destroying them.

                VertexPath path = pathCreator.path;
                spacing = Mathf.Max(minSpacing, spacing);

                float dst = startDistance;

                while (dst < endDistance) 
                {
                    Vector3 point = path.GetPointAtDistance(dst);
                    Quaternion rot = path.GetRotationAtDistance(dst);

                    // Use the object pool to get an object instead of instantiating it
                    GameObject obj = objectPool.GetObject(point, rot);
                    obj.transform.SetParent(holder.transform); // Parent it to the holder

                    dst += spacing;
                }
            }
        }

        // Deactivate objects instead of destroying them
        void RemoveObjects() 
        {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) 
            {
                GameObject obj = holder.transform.GetChild(i).gameObject;
                objectPool.ReturnObject(obj); // Return object to the pool (deactivate it)
            }
        }
    }
}
