using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    
    [Header("Use Bullet (default)")]
    public float fireRate = 1f;
    private float fireCD = 0f;
    
    public int burstRate = 0;
    public int burstCount = 1;

    private float burstCD = 0;
    private int curBurst = 0;
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool userLaser = false;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float damgeOverTime = 10;
    public float slowPercent = 0.5f;

    [Header("Unity Setup Field")]
    public float turnSpeed = 10f;
    public Transform partToRotate;
    public string enemyTag = "enemy";
    public Transform[] firePoints;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (userLaser)
            {
                foreach (Transform firePoint in firePoints)
                {
                    LineRenderer lineRenderer = firePoint.GetComponent<LineRenderer>();
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                    }
                }
                impactEffect.Stop();
                impactLight.enabled = false;
            }

            return;
        }

        // Target lock
        LockOnTarget();

        if (userLaser)
        {
            Laser();
        }

        else
        {
            if (curBurst < burstCount && curBurst < burstCount)
            {
                if (fireCD <= 0f)
                {
                    Shoot();
                    curBurst++;
                    fireCD = fireRate;
                }

                fireCD -= Time.deltaTime;
                burstCD = burstRate;
            }

            else
            {
                if (burstCD <= 0f)
                {
                    curBurst = 0;
                }
                burstCD -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(target);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damgeOverTime * Time.deltaTime, false);
        targetEnemy.Slow(slowPercent);

        foreach (Transform firePoint in firePoints)
        {
            LineRenderer lineRenderer = firePoint.GetComponent<LineRenderer>();

            if (lineRenderer != null)
            {
                if (!lineRenderer.enabled)
                {
                    lineRenderer.enabled = true;
                    impactEffect.Play();
                    impactLight.enabled = true;
                }

                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, target.position);

                Vector3 dir = firePoint.position - target.position;
                impactEffect.transform.rotation = Quaternion.LookRotation(dir);
                impactEffect.transform.position = target.position + (dir.normalized * 1f);
            }
        }
    }
}
