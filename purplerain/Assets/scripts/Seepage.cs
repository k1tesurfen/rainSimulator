using UnityEngine;

public class Seepage : MonoBehaviour, ISzenario
{

    public GameManager gm;
    public GameObject water;

    private GameObject[] chunk;

    bool inSpotlight = true;

    [Range(0, 10)]
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        chunk = gm.chunk.GenerateChunk(gameObject);
        foreach(GameObject face in chunk)
        {
            face.layer = LayerMask.NameToLayer("Spotlight");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inSpotlight)
        {
            Simulate(gm.time.GetTime());
            water.transform.position = new Vector3(water.transform.position.x, water.transform.position.y + 0.01f * Time.deltaTime, water.transform.position.z);
        }
        gm.chunk.UpdateChunk(chunk);
        transform.position = new Vector3(-gm.chunkSize / 2, 0f, -gm.chunkSize / 2);
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
            foreach(GameObject face in chunk)
            {
                face.layer = LayerMask.NameToLayer("Spotlight");
            }
        }
        else
        {
            foreach(GameObject face in chunk)
            {
                face.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    public void Simulate(float time)
    {

    }

}
