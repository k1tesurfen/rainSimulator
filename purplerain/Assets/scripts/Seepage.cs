using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Seepage : MonoBehaviour, ISzenario
{

    public GameManager gm;
    public GameObject water;
    public bool inSpotlight;
    MeshFilter meshFilter;

    public float size;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        gm.chunk.GenerateChunk(meshFilter.mesh, size);
    }

    // Update is called once per frame
    void Update()
    {
        if (inSpotlight)
        {
            gm.chunk.UpdateChunk(meshFilter.mesh, size, angle);
            Simulate(gm.time.GetTime());
        }
    }

    public void SetAnlge(float angle)
    {
        this.angle = angle;
    }

    public void InSpotlight(bool state)
    {
        inSpotlight = state;
        if (state)
        {
            gameObject.layer = LayerMask.NameToLayer("Spotlight");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void Simulate(float time)
    {

    }

}
