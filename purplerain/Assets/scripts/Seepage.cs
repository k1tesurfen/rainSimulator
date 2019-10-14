﻿using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Seepage : MonoBehaviour, ISzenario
{

    public GameManager gm;
    public GameObject water;
    bool inSpotlight = true;
    MeshFilter meshFilter;

    [Range(0, 10)]
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Spotlight");
        meshFilter = GetComponent<MeshFilter>();
        gm.chunk.GenerateChunk(meshFilter.mesh, gm.chunkSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (inSpotlight)
        {
            gm.chunk.UpdateChunk(meshFilter.mesh, gm.chunkSize, angle);
            Simulate(gm.time.GetTime());
            water.transform.position = new Vector3(water.transform.position.x, water.transform.position.y + 0.01f * Time.deltaTime, water.transform.position.z);
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
