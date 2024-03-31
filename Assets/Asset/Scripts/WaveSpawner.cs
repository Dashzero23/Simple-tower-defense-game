using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float TimeBetweenWave = 5f;
    private float countdown = 2f;
    public TextMeshProUGUI waveCountdownText; 

    private int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = TimeBetweenWave;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i  = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
