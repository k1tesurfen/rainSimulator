using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkGenerator: MonoBehaviour
{

    private Mesh mesh;
    public float size = 10f;
    [Range(0f, 10f)]
    public float angle;

    private Vector3[] oldVertices;
    private Vector2[] oldUV;
    private int[] oldTriangles;

    void Start()
    {
        GenerateCube();
        
    }

    public void Update()
    {
        oldVertices = mesh.vertices;
        oldUV = mesh.uv;
        oldTriangles = mesh.triangles;

        Debug.Log(GetHeightFromAngle(angle));
        oldVertices[2] = new Vector3(size, size + GetHeightFromAngle(angle), 0);
        oldVertices[9] = new Vector3(size, size + GetHeightFromAngle(angle), 0);
        oldVertices[12] = new Vector3(size, size + GetHeightFromAngle(angle), 0);

        oldVertices[0] = new Vector3(0, size + GetHeightFromAngle(angle), 0);
        oldVertices[8] = new Vector3(0, size + GetHeightFromAngle(angle), 0);
        oldVertices[10] = new Vector3(0, size + GetHeightFromAngle(angle), 0);

        mesh.Clear();
        mesh.vertices = oldVertices;
        mesh.triangles = oldTriangles;
        mesh.uv = oldUV;
        mesh.RecalculateNormals();

    }

    void GenerateCube()
    {
            Vector3[] vertices = {
            new Vector3(0, size, 0),
            new Vector3(0, 0, 0),
            new Vector3(size, size, 0),
            new Vector3(size, 0, 0),

            new Vector3(0, 0, size),
            new Vector3(size, 0, size),
            new Vector3(0, size, size),
            new Vector3(size, size, size),

            new Vector3(0, size, 0),
            new Vector3(size, size, 0),

            new Vector3(0, size, 0),
            new Vector3(0, size, size),

            new Vector3(size, size, 0),
            new Vector3(size, size, size),
        };

            int[] triangles = {
            0, 2, 1, // front
			1, 2, 3,
            4, 5, 6, // back
			5, 7, 6,
            6, 7, 8, //top
			7, 9 ,8,
            1, 3, 4, //bottom
			3, 5, 4,
            1, 11,10,// left
			1, 4, 11,
            3, 12, 5,//right
			5, 12, 13


        };


            Vector2[] uvs = {
            new Vector2(0, 0.6666f),
            new Vector2(0.25f, 0.6666f),
            new Vector2(0, 0.3333f),
            new Vector2(0.25f, 0.3333f),

            new Vector2(0.5f, 0.6666f),
            new Vector2(0.5f, 0.3333f),
            new Vector2(0.75f, 0.6666f),
            new Vector2(0.75f, 0.3333f),

            new Vector2(1, 0.6666f),
            new Vector2(1, 0.3333f),

            new Vector2(0.25f, 1),
            new Vector2(0.5f, 1),

            new Vector2(0.25f, 0),
            new Vector2(0.5f, 0),
        };

            mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
    }

    private float GetHeightFromAngle(float angle)
    {
        return size / Mathf.Sin((180f - angle - 90f) * Mathf.Deg2Rad)*Mathf.Sin(angle * Mathf.Deg2Rad);
    }
}