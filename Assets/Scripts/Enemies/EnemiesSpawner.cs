using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public bool isSpawned = true;
    public float spawnRate = 1f;
    public float spawnDelay = 0.5f;

    [Header("Enemies")]
    [SerializeField] private GameObject[] enemiesType;

    private float startSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        startSpawnTime = Time.time;
    }

    private void spawnEnemy()
    {
        if (enemiesType.Length <= 0)
            return;

        float randomValue = Random.Range(0.0f, 1.0f);
        float cumulativeRate = 0;

        for (int i = 0; i < enemiesType.Length; i++)
        {
            float enemySpawnRate = 0;

            if (enemiesType[i].GetComponent<Enemy>())
                enemySpawnRate = enemiesType[i].GetComponent<Enemy>().spawnRate;

            cumulativeRate += enemySpawnRate;

            if (randomValue <= cumulativeRate)
            {
                Instantiate(enemiesType[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }

    private void resetSpawner()
    {
        startSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startSpawnTime;

        if (isSpawned && elapsedTime > spawnDelay && Random.Range(0.0f, 1.0f) <= spawnRate)
        {
            spawnEnemy();
            resetSpawner();
        }
    }
}
