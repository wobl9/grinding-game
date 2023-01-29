using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Config", menuName = "Spawn config")]
public class EnemiesSpawnConfig : ScriptableObject
{
    public GameObject[] enemyPrefabs;
    public float spawnRadius = 10f;
    public float spawnRate = 2f;
    public float spawnDelay = 1f;
    public bool allowSpawn = true;
}