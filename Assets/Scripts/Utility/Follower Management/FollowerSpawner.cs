// Created By: Ryan Lupoli
// This script manages the spawning of followers in to a given scene
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class FollowerSpawner : MonoBehaviour
{
    #region Variables
    [Header("Prefab Settings")]
    [Tooltip("The prefab used for spawning followers.")]
    public GameObject followerPrefab;

    [Header("Follower Position Settings")]
    [SerializeField] private Transform[] spawnPoints = new Transform[6];

    [SerializeField] private PartyManager partyManager;
    [SerializeField] private FollowerUIHandler uiHandler;
    #endregion

    // Call this to populate the scene with equipped followers
    public void SpawnEquippedFollowers()
    {
        for (int i = 0; i < partyManager.equippedFollowers.Count; i++)
        {
            FollowerData data = partyManager.equippedFollowers[i];
            if (data.position < 1 || data.position > spawnPoints.Length)
                continue;

            GameObject followerGO = data.ToFollowerGameObject(followerPrefab);
            followerGO.transform.position = spawnPoints[data.position - 1].position;
            uiHandler.RegisterEquippedFollowerObject(data, followerGO);
        }
    }
}
