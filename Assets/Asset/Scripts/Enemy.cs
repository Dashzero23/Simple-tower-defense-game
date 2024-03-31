using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int WayPointIndex = 0;

    void Start()
    {
        target = WayPoints.points[0];
    }

    void Update()
    {
        Vector3 Direction = target.position - transform.position;
        transform.Translate(Direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (WayPointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        WayPointIndex++;
        target = WayPoints.points[WayPointIndex];
    }
}
