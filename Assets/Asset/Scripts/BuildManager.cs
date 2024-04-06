using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
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
    private Node selectedNode;
    public NodeUI nodeUI;

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

    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTurretToBuild (Blueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
}
