using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int hp = 100;
    public int money = 10;
    private Transform target;
    public GameObject deathEffect;
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
            EndPath();
            return;
        }

        WayPointIndex++;
        target = WayPoints.points[WayPointIndex];
    }

    void EndPath()
    {
        Destroy(gameObject);
        PlayerStat.Lives--;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        PlayerStat.Money += money;
        
        Destroy(gameObject);
    }
}
