using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    public Vector3 positionOffset;

    private Renderer rend;

    private GameObject turret;

    BuildManager buildManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.Instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        if (turret != null) 
        {
            Debug.Log("Can't build");
            return;
        }

        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        rend.material.color = hoverColor;
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
