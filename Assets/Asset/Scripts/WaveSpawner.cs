using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    private Transform spawnPoint;

    public float TimeBetweenWave = 5f;
    private float countdown = 2f;
    public TextMeshProUGUI waveCountdownText;

    public GameManager gameManager;
    private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = TimeBetweenWave;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("Time: {0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStat.Rounds++;

        Wave wave = waves[waveIndex];

        WayPoints.SetWaypointsSet(wave.wayPointSet);
        spawnPoint = WayPoints.GetStartPoint();

        foreach (int value in wave.count)
        {
            EnemiesAlive += value;
        }

        for (int i = 0; i < wave.enemy.Length; i++)
        {
            for (int j = 0; j < wave.count[i]; j++)
            {
                SpawnEnemy(wave.enemy[i]);
                yield return new WaitForSeconds(1f / wave.rate[i]);
            }
        }

        waveIndex++;

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
