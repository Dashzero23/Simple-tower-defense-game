using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemy; // Enemy type
    public int[] count; // # of enemy
    public float[] rate; // Spawn rate
    public int wayPointSet; // Path
    public int timeBetweenWave;
}
