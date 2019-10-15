using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSzenario : MonoBehaviour, ISzenario
{
    public GameManager gm;
    MeshFilter meshFilter;
    bool inSpotlight = false;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        meshFilter = GetComponent<MeshFilter>();
        gm.chunk.GenerateChunk(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (inSpotlight)
        {
        //    gm.chunk.UpdateChunk(meshFilter.mesh, gm.chunkSize, angle);
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
