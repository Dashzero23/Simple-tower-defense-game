using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemy; // Enemy type
    public int[] count; // # of enemy
    public int[] wayPointSet; // Path
    public int timeBetweenWave;
    public bool multiSpawn;
}
