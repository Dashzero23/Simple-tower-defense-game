using UnityEngine;

[System.Serializable]
public class Blueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject[] upgradedPrefab;
    public GameObject[] specialPrefab;
    public Sprite[] specialSprites;
    public int[] upgradeCost;
}
