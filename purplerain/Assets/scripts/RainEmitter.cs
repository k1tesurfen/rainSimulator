using System.Collections.Generic;
using UnityEngine;

public class RainEmitter : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public int rainAmount;
    Collider rainCloud;
    Vector3 max;
    Vector3 min;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }

        rainCloud = GetComponent<Collider>();
        max = rainCloud.bounds.max;
        min = rainCloud.bounds.min;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < rainAmount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
            SpawnFromPool("drops", randomPosition);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.Quit();
        }
    }

    public void SpawnFromPool(string tag, Vector3 pos)
    {
        GameObject drop = poolDictionary[tag].Dequeue();
        drop.SetActive(true);
        drop.transform.position = pos;
        drop.transform.forward = Camera.main.transform.forward;
        poolDictionary[tag].Enqueue(drop);
    }
}
