using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.Instance;
    }
    public void PurchaseStandardTurret()
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseMissileTurret()
    {
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }
}
