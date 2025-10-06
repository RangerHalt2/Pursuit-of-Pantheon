// Created By: Ryan Lupoli
// This script manages the spawning of followers in to a given scene
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FollowerSpawner : MonoBehaviour
{
    #region Variables
    [Header("Prefab Settings")]
    [Tooltip("The prefab used for spawning followers.")]
    public GameObject followerPrefab;

    [Header("Follower Position Settings")]
    [Tooltip("The spawn point of a follower assigned to postition 1.")]
    [SerializeField] private GameObject spawnPoint1;
    [Tooltip("The spawn point of a follower assigned to postition 2.")]
    [SerializeField] private GameObject spawnPoint2;
    [Tooltip("The spawn point of a follower assigned to postition 3.")]
    [SerializeField] private GameObject spawnPoint3;
    [Tooltip("The spawn point of a follower assigned to postition 4.")]
    [SerializeField] private GameObject spawnPoint4;
    [Tooltip("The spawn point of a follower assigned to postition 5.")]
    [SerializeField] private GameObject spawnPoint5;
    [Tooltip("The spawn point of a follower assigned to postition 6.")]
    [SerializeField] private GameObject spawnPoint6;
    #endregion

    // Spawns all stored followers in to the current scene
    public void SpwanAllFollowers()
    {
        // Get the party data
        Transform partyDataTransform = Bootstrapper.Instance.partyData.transform;
        // For each child in party data
        foreach (Transform child in partyDataTransform)
        {
            // Access the followerStatblock component of each follower
            FollowerStatblock statBlock = child.GetComponent<FollowerStatblock>();

            if (statBlock == null)
            {
                continue;
            }

            // Determine spawn position
            Vector3 spawnPosition = GetSpawnPosition(statBlock.position);

            // Instantuiate a followerPrefab at specified position
            GameObject followerGO = Instantiate(followerPrefab, spawnPosition, Quaternion.identity);
            // Get follower component of followerGO
            Follower followerComponent = followerGO.GetComponent<Follower>();
            // Load Statblock data to follower
            followerComponent.LoadFromStatblock(statBlock);
        }
    }

    // Spawns a specific follower into the current scene based on their followerID
    public void SpawnFollowerByID(string followerID)
    {
        // Search children of partyData for a stat block with a matching followerID
        Transform partyDataTransform = Bootstrapper.Instance.partyData.transform;

        FollowerStatblock statBlock = null;

        // For every child in partyData
        foreach (Transform child in partyDataTransform)
        {
            var block = child.GetComponent<FollowerStatblock>();
            // If a statblock was found, and the follower ID is the same as the one provided
            if (block != null && block.followerID == followerID)
            {
                // Assign this statBlock
                statBlock = block;
                break;
            }
        }

        // If no follower was found
        if (statBlock == null)
        {
            Debug.LogWarning("Could not find a follower with the ID \"" + followerID + "\" in partyData.");
        }

        // Determine spawn position
        Vector3 spawnPosition = GetSpawnPosition(statBlock.position);

        // Instantuiate a followerPrefab at specified position
        GameObject followerGO = Instantiate(followerPrefab, spawnPosition, Quaternion.identity);
        // Get follower component of followerGO
        Follower followerComponent = followerGO.GetComponent<Follower>();
        // Load Statblock data to follower
        followerComponent.LoadFromStatblock(statBlock);
    }

    // Determine the spawn point for a follower.
    private Vector3 GetSpawnPosition(int partyPosition)
    {
        // Return the position of a defined spawn point based on the value provided
        switch (partyPosition)
        {
            case 1:
                return spawnPoint1.transform.position;
            case 2:
                return spawnPoint2.transform.position;
            case 3:
                return spawnPoint3.transform.position;
            case 4:
                return spawnPoint4.transform.position;
            case 5:
                return spawnPoint5.transform.position;
            case 6:
                return spawnPoint6.transform.position;
            default:
                Debug.LogWarning("$Invalid Party position: {partyPosition}. Defaulting to spawnPoint 1");
                return spawnPoint1.transform.position;
        }
    }
}
