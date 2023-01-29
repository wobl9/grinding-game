using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemiesSpawnConfig config;
    private float gameTime;

    void Start()
    {
        if(config.allowSpawn)
        {
            InvokeRepeating("SpawnEnemy", config.spawnDelay, config.spawnRate);
            gameTime = Time.time;

        }
 
    }

    void SpawnEnemy()
    {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * config.spawnRadius;
        int randomIndex = Random.Range(0, config.enemyPrefabs.Length);
        Instantiate(config.enemyPrefabs[randomIndex], randomPos, Quaternion.identity);
    }
}