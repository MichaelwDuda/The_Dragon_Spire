using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int enemyCount = 3;
        public float spawnDelay = 1f;
        public float timeBeforeNextWave = 3f;
    }

    public Wave[] waves;
    public Transform spawnPoint;
    public Transform[] waypoints;
    public HomeTowerHealth homeTower;
    public bool startOnPlay = true;

    private bool isSpawning;

    void Start()
    {
        if (startOnPlay)
        {
            StartWaves();
        }
    }

    public void StartWaves()
    {
        if (isSpawning)
            return;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        isSpawning = true;

        if (waves == null || waves.Length == 0)
        {
            Debug.LogWarning("EnemySpawner has no waves assigned.", this);
            isSpawning = false;
            yield break;
        }

        for (int waveIndex = 0; waveIndex < waves.Length; waveIndex++)
        {
            Wave wave = waves[waveIndex];

            if (wave == null || wave.enemyPrefab == null)
            {
                Debug.LogWarning($"Wave {waveIndex + 1} is missing an enemy prefab.", this);
                continue;
            }

            int count = Mathf.Max(0, wave.enemyCount);

            for (int enemyIndex = 0; enemyIndex < count; enemyIndex++)
            {
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(Mathf.Max(0f, wave.spawnDelay));
            }

            if (waveIndex < waves.Length - 1)
            {
                yield return new WaitForSeconds(Mathf.Max(0f, wave.timeBeforeNextWave));
            }
        }

        isSpawning = false;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform point = spawnPoint != null ? spawnPoint : transform;
        GameObject enemy = Instantiate(enemyPrefab, point.position, point.rotation);

        Enemy_move movement = enemy.GetComponent<Enemy_move>();
        if (movement != null)
        {
            movement.Configure(waypoints, homeTower);
        }
    }
}
