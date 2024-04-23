using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public Blueprint standardTurret;
    public Blueprint missileTurret;
    public Blueprint laserBeamer;

    void Start()
    {
        buildManager = BuildManager.Instance;
    }
    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuild(missileTurret);
    }
    public void SelectLaserBeamer()
    {
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
