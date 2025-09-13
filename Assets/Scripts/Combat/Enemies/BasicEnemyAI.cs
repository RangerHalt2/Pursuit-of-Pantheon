// Created By: Ryan Lupoli
// This is a basic AI script meant to manage followers. Intended for the prototype build
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    #region Variables
    [Header("Action Settings")]
    [Tooltip("The amount of time (in seconds) this follower takes to perform an action.")]
    [SerializeField] private float actionInterval;

    private float elapsedTime;

    // Determines how many times the object can attempt to choose an action. Exists as a temporary measure to prevent game lockups. This will be replaced by a more elegant solution later
    private int choiceLimit = 5;

    // Whether the follower successfully performed an action
    private bool actionPerformed = false;

    [Header("Team Settings")]
    [Tooltip("Team ID's for allies. Will be targeted by the enemy's supportive actions.")]
    [SerializeField] private int[] alliedTeamIds;
    [Tooltip("Team ID's for enemies. Will be targeted by the enemy's basic attacks.")]
    [SerializeField] private int[] enemyTeamIds;


    // Reference to the Enemy Stats Script
    private EnemyStats enemyStats;
    private Health health;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Check if Follower Stats have been assigned
        enemyStats = GetComponent<EnemyStats>();

        if (enemyStats != null)
        {

        }
        else
        {
            Debug.LogWarning("EnemyStats Script not found on this GameObject. Follower AI will not function.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for the action interval
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= actionInterval)
        {
            Debug.Log("Action Interval Passed.");
            // Until an action is performed...
            while (!actionPerformed && choiceLimit < 5)
            {
                Debug.Log("Action has not been performed.");
                // Select potential action at random
                int action = UnityEngine.Random.Range(0, 2);
                // After action is performed, set the relevant boolean to true
                switch (action)
                {
                    // Attack a follower
                    case 0:
                        Attack();
                        break;
                    // Do nothing
                    case 1:
                        break;
                    default:
                        choiceLimit++;
                        break;
                }
            }
            // Reset Elapsed Time
            elapsedTime = 0;
            choiceLimit = 0;
            actionPerformed = false;

            CombatManager.instance.CheckCombatState();
        }
    }

    // The follower performs a basic attack against an enemy.
    void Attack()
    {
        Debug.Log("Attack Method Called.");

        // Select a random target
        GameObject target = FindRandomTargetByID(enemyTeamIds);

        if (target != null)
        {
            // Check if target has a health script
            Health targetHealth = target.GetComponent<Health>();

            Debug.Log(name + " attacked " + target.name + ".");
            targetHealth.TakeDamage(enemyStats.Attack);
            actionPerformed = true;
        }
        else
        {
            choiceLimit++;
        }
    }

    // The follower performs a basic heal on an ally or themself
    void Heal()
    {
        Debug.Log("Heal Method Called.");

        // Select Random Allied Target
        GameObject target = FindRandomTargetByID(alliedTeamIds);

        if (target != null)
        {
            Health targetHealth = target.GetComponent<Health>();
            // Check if the target's current health is already at or above their max
            if (targetHealth.currentHealth >= targetHealth.maxHealth)
            {
                choiceLimit++;
                return;
            }

            Debug.Log(name + " healed " + target.name + ".");
            targetHealth.ReceiveHealing(enemyStats.Magic);
            actionPerformed = true;
        }
        else
        {
            choiceLimit++;
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
            // Debug.LogWarning("No GameObjects with a provided teamID. Make sure there are targets with a Health Script, and that they have the correct TeamID.");
            return null;
        }

        // Choose a random target
        int index = UnityEngine.Random.Range(0, potentialTargets.Count);
        return potentialTargets[index];
    }
}
