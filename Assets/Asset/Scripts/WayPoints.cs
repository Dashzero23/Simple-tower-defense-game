using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    private static Transform[][] pointsMap;
    public static Transform[] points;

    public void Awake()
    {
        Transform[][] _pointsMap = new Transform[transform.childCount][];

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform _transform = transform.GetChild(i);
            Transform[] p = new Transform[_transform.childCount];
            _pointsMap[i] = p;
            for (int j = 0; j < _transform.childCount; j++)
            {
                //Debug.Log(_transform.name + " - " + _transform.GetChild(j).name);
                p[j] = _transform.GetChild(j);
            }
        }
        pointsMap = _pointsMap;
        points = pointsMap[0];
    }

    public static void SetWaypointsSet(int index)
    {
        // Select a random waypoint set.
        if (index == -1)
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, pointsMap.Length);
            points = pointsMap[rInt];
            return;
        }
        // minor mistake of the user in the configuration of the wave :-)
        if (index >= pointsMap.Length)
        {
            Debug.Log("index \"" + index + "\" is out of range in pointsMap (max=" + (pointsMap.Length - 1) + ").");
            index = 0;
        }
        points = pointsMap[index];
    }

    public static Transform GetStartPoint()
    {
        // return the first waypoint.
        return points[0];
    }
}