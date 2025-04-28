using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CubeWithThroughHole : MonoBehaviour
{
    public float cubeSize = 1f;   // Size of the cube

    // Define the four corners of the hole on the cube's front face
    public Vector2 holeCorner1 = new Vector2(-0.2f, 0.2f);
    public Vector2 holeCorner2 = new Vector2(0.2f, 0.2f);
    public Vector2 holeCorner3 = new Vector2(-0.2f, -0.2f);
    public Vector2 holeCorner4 = new Vector2(0.2f, -0.2f);

    private void Start()
    {
        CreateCubeWithThroughHole();
    }

    private void CreateCubeWithThroughHole()
    {
        Mesh mesh = new Mesh();

        // Define vertices for the cube and hole corners
        Vector3[] vertices = new Vector3[]
        {
            // Front face (without hole)
            new Vector3(-cubeSize, -cubeSize, cubeSize),  // 0
            new Vector3(cubeSize, -cubeSize, cubeSize),   // 1
            new Vector3(cubeSize, cubeSize, cubeSize),    // 2
            new Vector3(-cubeSize, cubeSize, cubeSize),   // 3

            // Back face (without hole)
            new Vector3(-cubeSize, -cubeSize, -cubeSize), // 4
            new Vector3(cubeSize, -cubeSize, -cubeSize),  // 5
            new Vector3(cubeSize, cubeSize, -cubeSize),   // 6
            new Vector3(-cubeSize, cubeSize, -cubeSize),  // 7

            // Front face hole corners
            new Vector3(holeCorner1.x, holeCorner1.y, cubeSize), // 8
            new Vector3(holeCorner2.x, holeCorner2.y, cubeSize), // 9
            new Vector3(holeCorner3.x, holeCorner3.y, cubeSize), // 10
            new Vector3(holeCorner4.x, holeCorner4.y, cubeSize), // 11

            // Back face hole corners
            new Vector3(holeCorner1.x, holeCorner1.y, -cubeSize), // 12
            new Vector3(holeCorner2.x, holeCorner2.y, -cubeSize), // 13
            new Vector3(holeCorner3.x, holeCorner3.y, -cubeSize), // 14
            new Vector3(holeCorner4.x, holeCorner4.y, -cubeSize), // 15
        };
        int[] triangles = new int[]
        {
            // Bottom face
            0, 1, 3,
            1, 2, 3,

            // Top face
            4, 5, 7,
            5, 6, 7,

            // Left face
            0, 4, 7,
            0, 7, 3,

            // Right face
            1, 2, 6,
            1, 6, 5,

            // Front face around the hole
            0, 1, 8,
            1, 9, 8,
            9, 10, 8,
            10, 11, 8,
            11, 3, 0,

            // Back face around the hole
            4, 7, 12,
            5, 4, 13,
            13, 4, 12,
            5, 13, 6,
            13, 12, 14,
            14, 6, 13,
            7, 6, 15,
            6, 14, 15,
            7, 15, 12,

            // Connect front hole corners to back hole corners to form the through-hole walls
            8, 9, 13,
            8, 13, 12,
            9, 10, 14,
            9, 14, 13,
            10, 11, 15,
            10, 15, 14,
            11, 8, 12,
            11, 12, 15
        };
        
        // UV Mapping for texture (optional)
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        // Assign vertices, triangles, and UVs to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        // Recalculate normals for proper lighting
        mesh.RecalculateNormals();

        // Assign the mesh to the MeshFilter component
        GetComponent<MeshFilter>().mesh = mesh;
    }
}