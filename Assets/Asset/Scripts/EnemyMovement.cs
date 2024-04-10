using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int WayPointIndex = 0;

    private Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = WayPoints.points[0];
    }

    void Update()
    {
        Vector3 Direction = target.position - transform.position;
        transform.Translate(Direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWayPoint()
    {
        if (WayPointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        WayPointIndex++;
        target = WayPoints.points[WayPointIndex];
    }

    void EndPath()
    {
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
        PlayerStat.Lives--;
    }
}
