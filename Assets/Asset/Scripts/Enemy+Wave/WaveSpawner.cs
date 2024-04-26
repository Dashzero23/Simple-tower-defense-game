using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    private Transform spawnPoint;

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


        // Set the countdown timer for the next wave
        countdown = wave.timeBetweenWave;

        // Increment wave index to move to the next wave
        waveIndex++;

        // Calculate the total number of enemies in the current wave
        foreach (int value in wave.count)
        {
            EnemiesAlive += value;
        }

        if (wave.multiSpawn)
        {
            // Multi-spawn: start spawning from all sets at once
            for (int i = 0; i < wave.enemy.Length; i++)
            {
                StartCoroutine(SpawnEnemySet(wave, i));
            }
        }
        else
        {
            // Sequential spawn: spawn each set of enemies one by one
            for (int i = 0; i < wave.enemy.Length; i++)
            {
                yield return StartCoroutine(SpawnEnemySet(wave, i));
            }
        }
    }

    IEnumerator SpawnEnemySet(Wave wave, int setIndex)
    {
        if (setIndex < wave.delay.Length)
        {
            yield return new WaitForSeconds(wave.delay[setIndex]);
        }

        Enemy enemy = wave.enemy[setIndex].GetComponent<Enemy>();
        float rate = enemy.stat.rate;
        Transform[] curPath = WayPoints.SetWaypointsSet(wave.wayPointSet[setIndex]);
        spawnPoint = curPath[0];

        for (int j = 0; j < wave.count[setIndex]; j++)
        {
            SpawnEnemy(wave.enemy[setIndex], curPath);
            yield return new WaitForSeconds(rate);
        }
    }


    void SpawnEnemy(GameObject enemy, Transform[] wayPoint)
    {
        GameObject curEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemyMovement enemyMovement = curEnemy.GetComponent<EnemyMovement>();
        enemyMovement.path = wayPoint;
    }
}
