using UnityEngine;

[CreateAssetMenu(fileName = "new szenario", menuName = "szenario")]
public class Szenario : ScriptableObject
{
    public GameObject chunk;
    public GameObject water;
    public int position;
}

