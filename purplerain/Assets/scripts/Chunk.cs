using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameManager gm;

    public Material gras;
    public Material dirt;

    private float defaultHeight = 10f;

    public GameObject[] GenerateChunk(GameObject parent)
    {
        GameObject north = GenerateFace("North", parent.transform, dirt, gm.chunkSize, false);
        north.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 180));
        north.transform.localPosition = new Vector3(0f, 0f, gm.chunkSize / 2);

        GameObject south = GenerateFace("South", parent.transform, dirt, gm.chunkSize, false);
        south.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 180));
        south.transform.localPosition = new Vector3(0f, 0f, -gm.chunkSize / 2);

        GameObject east = GenerateFace("East", parent.transform, dirt, gm.chunkSize, false);
        east.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 90));
        east.transform.localPosition = new Vector3(gm.chunkSize / 2, 0f, 0f);

        GameObject west = GenerateFace("West", parent.transform, dirt, gm.chunkSize, false);
        west.transform.rotation = Quaternion.Euler(new Vector3(90, 0, -90));
        west.transform.localPosition = new Vector3(-gm.chunkSize / 2, 0f, 0f);

        GameObject top = GenerateFace("Top", parent.transform, gras, gm.chunkSize, true);
        top.transform.localPosition = new Vector3(0f, 5f, 0f);

        GameObject[] chunk = { north, south, east, west, top };

        return chunk;
    }

    private GameObject GenerateFace(string name, Transform parent, Material mat, float size, bool top)
    {
        GameObject face = new GameObject(name);
        face.transform.parent = parent;
        MeshFilter meshFilter = face.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = face.AddComponent<MeshRenderer>();
        meshRenderer.material = mat;

        Mesh mesh = meshFilter.mesh;

        Vector3[] vertices;
        if (top)
        {
            vertices = new Vector3[]{
                 new Vector3(-size/2, 0, size/2),
                 new Vector3(-size/2, 0, -size/2),
                 new Vector3(size/2, 0, size/2),
                 new Vector3(size/2, 0, -size/2),
            };

        }
        else
        {
            vertices = new Vector3[]{
                 new Vector3(-size/2, 0, 5f),
                 new Vector3(-size/2, 0, -5f),
                 new Vector3(size/2, 0, 5f),
                 new Vector3(size/2, 0, -5f),
            };
        }


        int[] triangles = {
        0, 2, 1, // front
        1, 2, 3,
        };

        Vector2[] uvs =
        {
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0)
        };


        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return face;
    }

    public void UpdateChunk(GameObject[] faces)
    {
        for(int i=0; i<5;i++){
            UpdateFace(faces[i], gm.chunkSize, false);
            faces[i].GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(gm.chunkSize / 2, gm.chunkSize / 2));
        }
        //top face must be updated as square.
        UpdateFace(faces[4], gm.chunkSize, true);
        faces[3].GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(gm.chunkSize / 2, gm.chunkSize / 2));

        faces[0].transform.localPosition = new Vector3(0f, 0f, gm.chunkSize / 2);
        faces[1].transform.localPosition = new Vector3(0f, 0f, -gm.chunkSize / 2);
        faces[2].transform.localPosition = new Vector3(gm.chunkSize / 2, 0f, 0f);
        faces[3].transform.localPosition = new Vector3(-gm.chunkSize / 2, 0f, 0f);
        faces[4].transform.localPosition = new Vector3(0f, 5f, 0f);

    }

    private GameObject UpdateFace(GameObject face, float size, bool top)
    {

        Mesh mesh = face.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices;
        if (top)
        {
            vertices = new Vector3[]{
                 new Vector3(-size/2, 0, size/2),
                 new Vector3(-size/2, 0, -size/2),
                 new Vector3(size/2, 0, size/2),
                 new Vector3(size/2, 0, -size/2),
            };
        }
        else
        {
            vertices = new Vector3[]{
                 new Vector3(-size/2, 0, 5f),
                 new Vector3(-size/2, 0, -5f),
                 new Vector3(size/2, 0, 5f),
                 new Vector3(size/2, 0, -5f),
            };
        }
        int[] oldTriangles = mesh.triangles;
        Vector2[] oldUvs = mesh.uv;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = oldTriangles;
        mesh.uv = oldUvs;
        mesh.RecalculateNormals();

        return face;
    }

    private float GetHeightFromAngle(float angle, float size)
    {
        return size / Mathf.Sin((180f - angle - 90f) * Mathf.Deg2Rad) * Mathf.Sin(angle * Mathf.Deg2Rad);
    }
}
