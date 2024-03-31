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

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild (GameObject turret)
    {
        turretToBuild = turret;
    }
}
