using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [Tooltip("Index of prefab in EnemySpawner's prefab list.")]
    public int prefabIndex;

    [Tooltip("Index of spawn point in EnemySpawner's spawn points array.")]
    public int spawnIndex;
}
