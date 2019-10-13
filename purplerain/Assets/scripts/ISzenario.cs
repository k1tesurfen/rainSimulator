using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISzenario
{
    void Simulate(float time);
    void InSpotlight(bool state);
}
