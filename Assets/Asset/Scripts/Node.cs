using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    public int upgradeLevel;

    private Renderer rend;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public Blueprint turretBlueprint;
    [HideInInspector]
    //public bool isUpgraded = false;

    BuildManager buildManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret (Blueprint blueprint)
    {
        if (PlayerStat.Money < blueprint.cost)
        {
            Debug.Log("Not enough money");
            return;
        }
        PlayerStat.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

        turret = _turret;
        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    public void UpgradeTurret()
    {
        if (PlayerStat.Money < turretBlueprint.upgradeCost[upgradeLevel])
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }
        PlayerStat.Money -= turretBlueprint.upgradeCost[upgradeLevel];
        
        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab[upgradeLevel], GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        upgradeLevel++;

        //isUpgraded = true;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void SpecialUpgradeTurret(int index)
    {
        if (PlayerStat.Money < turretBlueprint.upgradeCost[upgradeLevel])
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }
        PlayerStat.Money -= turretBlueprint.upgradeCost[upgradeLevel];

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.specialPrefab[index], GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        upgradeLevel++;

        //isUpgraded = true;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void SellTurret()
    {
        PlayerStat.Money += GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
        upgradeLevel = 0;
        //isUpgraded = false;
    }
    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.canBuild)
        {
            return;
        }
        if (buildManager.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public int GetSellAmount()
    {
        if (upgradeLevel == 0)
        {
            return turretBlueprint.cost / 2;
        }

        return turretBlueprint.upgradeCost[upgradeLevel - 1] / 2;
    }
}
