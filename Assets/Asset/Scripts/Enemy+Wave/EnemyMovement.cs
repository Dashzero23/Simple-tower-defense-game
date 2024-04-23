using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public Transform[] path;
    private Transform target;
    private int WayPointIndex = 0;

    private Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = path[0];
    }

    void Update()
    {
        Vector3 Direction = target.position - transform.position;
        transform.Translate(Direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.stat.startSpeed;
    }

    void GetNextWayPoint()
    {
        if (WayPointIndex >= path.Length - 1)
        {
            EndPath();
            return;
        }

        WayPointIndex++;
        target = path[WayPointIndex];
    }

    void EndPath()
    {
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
        PlayerStat.Lives-= enemy.stat.live;
    }
}
