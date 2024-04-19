using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    private Transform spawnPoint;
    private Transform[] curPath;

    //public float TimeBetweenWave = 5f;
    private float countdown = 2f;
    public TextMeshProUGUI waveCountdownText;

    public GameManager gameManager;
    private int waveIndex = 0;

    void Start()
    {
        countdown = 10f;
    }
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            return;
        }

        if (countdown <= 0 && waveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave());
            //countdown = TimeBetweenWave;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("Time: {0:00.00}", countdown);

        if (waveIndex == waves.Length && EnemiesAlive == 0)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
    }

    IEnumerator SpawnWave()
    {
        PlayerStat.Rounds++;

        Wave wave = waves[waveIndex];

        countdown = wave.timeBetweenWave;

        waveIndex++;

        foreach (int value in wave.count)
        {
            EnemiesAlive += value;
        }

        for (int i = 0; i < wave.enemy.Length; i++)
        {
            Enemy enemy = wave.enemy[i].GetComponent<Enemy>();
            curPath = WayPoints.SetWaypointsSet(wave.wayPointSet[i]);
            spawnPoint = curPath[0];

            for (int j = 0; j < wave.count[i]; j++)
            {
                SpawnEnemy(wave.enemy[i]);
                yield return new WaitForSeconds(enemy.stat.rate);
            }
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject curEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemyMovement enemyMovement = curEnemy.GetComponent<EnemyMovement>();
        enemyMovement.path = curPath;
    }
}
