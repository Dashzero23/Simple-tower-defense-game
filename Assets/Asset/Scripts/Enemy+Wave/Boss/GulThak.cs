using System.Collections.Generic;
using UnityEngine;

public class GulThak : MonoBehaviour
{
    private GameObject nodesGameObject;
    private Node[] nodes;
    public GameObject destroyEffect;

    void Awake()
    {
        nodesGameObject = GameObject.Find("Nodes");

        if (nodesGameObject != null)
        {
            nodes = new Node[nodesGameObject.transform.childCount];

            for (int i = 0; i < nodesGameObject.transform.childCount; i++)
            {
                Transform childTransform = nodesGameObject.transform.GetChild(i);
                nodes[i] = childTransform.GetComponent<Node>();

                if (nodes[i] == null)
                {
                    Debug.LogWarning($"Child at index {i} does not have a Node component.");
                }
            }
        }

        else
        {
            Debug.LogWarning("Nodes GameObject not found in the scene.");
        }
    }

    void Start()
    {
        InvokeRepeating("DestroyTurret", 0f, 10f);
    }

    void DestroyTurret()
    {
        List<Node> nodesWithTurrets = new List<Node>();

        foreach (Node node in nodes)
        {
            if (node.upgradeLevel != -1)
            {
                nodesWithTurrets.Add(node);
            }
        }

        if (nodesWithTurrets.Count > 0)
        {
            int randomIndex = Random.Range(0, nodesWithTurrets.Count);
            Node selectedNode = nodesWithTurrets[randomIndex];

            selectedNode.DestroyTurret();

            Vector3 effectPosition = selectedNode.GetBuildPosition() + new Vector3(0f, 2f, 0f);
            GameObject effect = Instantiate(destroyEffect, effectPosition, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
