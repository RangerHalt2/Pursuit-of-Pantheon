// Created By: Ryan Lupoli
// This is a basic AI script meant to manage followers. Intended for the prototype build
using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BasicFollowerAI : MonoBehaviour
{
    #region Variables
    [Header("Action Settings")]
    [Tooltip("The amount of time (in seconds) this follower takes to perform an action.")]
    [SerializeField] private float actionInterval;

    private float elapsedTime;

    [Header("Team Settings")]
    [Tooltip("Team ID's for allies. Will be targeted by a follower's supportive actions.")]
    [SerializeField] private int[] alliedTeamIds;
    [Tooltip("Team ID's for enemies. Will be targeted by a follower's basic attacks.")]
    [SerializeField] private int[] enemyTeamIds;
    

    // Reference to the Follower Stats Script
    private FollowerStats followerStats;
    private Health health;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Check if Follower Stats have been assigned
        followerStats = GetComponent<FollowerStats>();

        if (followerStats != null)
        {

        }
        else
        {
            Debug.LogWarning("FollowerStats Script not found on this GameObject. Follower AI will not function.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= actionInterval)
        {
            elapsedTime = 0;
            Attack();
        }
    }

    // The follower performs a basic attack against an enemy.
    void Attack()
    {
        Debug.Log("Attack Method Called.");

        // Select a random target
        GameObject target = FindRandomTargetByID(enemyTeamIds);

        // Check if target has a health script
        Health targetHealth = target.GetComponent<Health>();
        if (target != null)
        {
            Debug.Log(name + " attacked " + target.name + ".");
            targetHealth.TakeDamage(followerStats.Attack);
        }
        else
        {
            Debug.LogWarning(target.name + " does not have a Health Script.");
        }
    }

    // Selects a random target with a specified tag from within the scene.
    public GameObject FindRandomTargetByTag(String tagName)
    {
        // Create an array of potential targets
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(tagName);

        // If no objects with specified tag are found, return null
        if (potentialTargets.Length == 0)
        {
            Debug.LogWarning("No GameObjects with found tag: " + tagName);
            return null;
        }

        // Select a random object in the array
        int index = UnityEngine.Random.Range(0, potentialTargets.Length);

        // Return the selected target
        // Debug.Log(potentialTargets[index].name + " was selected.");
        return potentialTargets[index];
    }

    // Selects a random target with a specified teamID from within the scene.
    public GameObject FindRandomTargetByID(int[] targetTeamID)
    {
        // Create an array of all objects with a health component
        Health[] healthComponents = GameObject.FindObjectsByType<Health>(FindObjectsSortMode.None);

        // Create a list of all game objects
        List<GameObject> potentialTargets = new List<GameObject>();

        // Check each healthComponent Identified
        foreach (Health health in healthComponents)
        {
            if (health != null && targetTeamID != null)
            {
                // For every teamID targeted...
                foreach (int teamID in targetTeamID)
                {
                    // If the teamID is one of the targeted IDs...
                    if (health.teamID == teamID)
                    {
                        // Add the game object to the potential targets
                        potentialTargets.Add(health.gameObject);
                        break;
                    }
                }
            }
        }

        // If no valid targets were found, return null
        if (potentialTargets.Count == 0)
        {
            Debug.LogWarning("No GameObjects with a provided teamID. Make sure there are targets with a Health Script, and that they have the correct TeamID.");
            return null;
        }

        // Choose a random target
        int index = UnityEngine.Random.Range(0, potentialTargets.Count);
        return potentialTargets[index];
    }    
}
