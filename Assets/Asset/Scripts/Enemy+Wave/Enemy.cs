using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStat stat;
    [HideInInspector]
    public float speed;
    public float hp;

    [Header("Unity Stuff")]
    public UnityEngine.UI.Image healthBar;

    private bool isDead = false;
    void Start()
    {
        speed = stat.startSpeed;
        hp = stat.startHP;
    }
    public void TakeDamage(float amount, bool physical)
    {
        if(physical)
        {
            hp -= amount * (1 - stat.physArmor);
        }
        else
        {
            hp -= amount * (1 - stat.lasArmor);
        }
        
        healthBar.fillAmount = hp / stat.startHP;

        if (hp <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow (float amount)
    {
        speed = stat.startSpeed * (1f - amount);
    }

    private void Die()
    {
        isDead = true;

        GameObject effect = Instantiate(stat.deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        PlayerStat.Money += stat.drop;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
