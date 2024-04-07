using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float hp;
    private float startHP = 100;
    public int drop = 10;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public UnityEngine.UI.Image healthBar;

    void Start()
    {
        speed = startSpeed;
        hp = startHP;
    }
    public void TakeDamage(float amount)
    {
        hp -= amount;
        healthBar.fillAmount = hp / startHP;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Slow (float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    private void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        PlayerStat.Money += drop;

        Destroy(gameObject);
    }
}
