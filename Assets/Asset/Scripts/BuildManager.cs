using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;


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
    }
    public void SelectTurretToBuild (Blueprint turret)
    {
        turretToBuild = turret;
    }
}
