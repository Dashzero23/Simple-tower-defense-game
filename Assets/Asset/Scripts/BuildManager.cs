using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject buildEffect;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than 1 BuildManager");
            return;
        }

        Instance = this;
    }

    private Blueprint turretToBuild;

    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStat.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStat.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money");
            return;
        }
        PlayerStat.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    public void SelectTurretToBuild (Blueprint turret)
    {
        turretToBuild = turret;
    }
}
