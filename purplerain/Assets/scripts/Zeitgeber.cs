using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeitgeber : MonoBehaviour
{
    public enum TimeScale
    {
        second,
        minute,
        hour
    }
    public int timeScale = 60;
    public int simulationDuration = 100;

    //return the timestamp of the simulation in seconds
    public float GetTime()
    {
        return (Time.time % simulationDuration) * timeScale;
    }
    
    public void SetTimeScale(TimeScale ts)
    {
        switch (ts)
        {
            case TimeScale.second:
                timeScale = 1;
                break;
            case TimeScale.minute:
                timeScale = 60;
                break;
            case TimeScale.hour:
                timeScale = 3600;
                break;
            default:
                break;
        }
    }

}
