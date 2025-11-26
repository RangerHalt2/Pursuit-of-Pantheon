using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Points")]
    public Transform[] enemySpawnPoints;

    // The data defining which enemies appear in this battle
    public List<EnemySpawnData> enemiesToSpawn = new List<EnemySpawnData>();

    public void SpawnEncounterEnemies()
    {
        // For every enemy which is to be spawned
        foreach (var enemyData in enemiesToSpawn)
        {
            // If the the spawnIndex is within the range of spawnPoints
            if (enemyData.spawnIndex < 0 || enemyData.spawnIndex >= enemySpawnPoints.Length)
                continue;

            // Spawn enemyPrefab at defines spawnpoint
            GameObject enemy = Instantiate(enemyPrefabs[enemyData.prefabIndex], enemySpawnPoints[enemyData.spawnIndex].position, enemySpawnPoints[enemyData.spawnIndex].rotation);
        }
    }
}

[System.Serializable]
public class EnemySpawnData
{
    [Tooltip("Index of prefab in EnemySpawner's prefab list.")]
    public int prefabIndex;

    [Tooltip("Index of spawn point in EnemySpawner's spawn points array.")]
    public int spawnIndex;
}
