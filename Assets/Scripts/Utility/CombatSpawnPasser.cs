using UnityEngine;

public class CombatSpawnPasser : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;


    private void Start()
    {
        int index = 0;
        FollowerSpawner spawner = GameObject.FindAnyObjectByType<FollowerSpawner>();
        if (spawner != null)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                spawner.spawnPoints[index] = spawnPoint;
                index++;
            }
        }
    }
}
