// Created By: Ryan Lupoli
// This iscript manages the AI of the player's followers
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework.Constraints;
using UnityEngine;


public class FollowerAI : MonoBehaviour
{

    #region Variables
    [Header("Action Settings")]
    [Tooltip("The amount of time (in seconds) this follower takes to perform an action.")]
    [SerializeField] private float actionInterval;

    private float elapsedTime;

    // Determines how many times a follower can attempt to choose an action. Exists as a temporary measure to prevent game lockups. This will be replaced by a more elegant solution later
    private int choiceLimit = 0;

    // Whether the follower successfully performed an action
    private bool actionPerformed = false;

    [Header("Team Settings")]
    [Tooltip("Team ID's for allies. Will be targeted by a follower's supportive actions.")]
    [SerializeField] private int[] alliedTeamIds;
    [Tooltip("Team ID's for enemies. Will be targeted by a follower's basic attacks.")]
    [SerializeField] private int[] enemyTeamIds;


    // Reference to the Follower Stats Script
    private FollowerStats followerStats;
    private FollowerSkills followerSkills;
    // Reference to the health script
    private Health health;

    [Header("Skill Settings")]
    [Tooltip("The list of skills the player has access to. Enter Data as Skill IDs.")]
    [SerializeField] private List<int> skills;
    
    [Header("Tactics Settings")]
    [Tooltip("Determines how a follower acts in combat.")]
    public Tactics currentTactic;

    // What tactics the followers can adopt in combat
    public enum Tactics
    {
        FullAssault,
        SupportTeam,
        Idle
    }


    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Check if Follower Stats and follower Skills have been assigned
        followerStats = GetComponent<FollowerStats>();

        if (followerStats == null)
        {
            Debug.LogWarning("FollowerStats Script not found on this GameObject. Follower AI will not function.");
        }
        if (followerSkills == null)
        {
            Debug.LogWarning("FollowerSkills Script not found on this GameObject. Follower AI will not function.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for the action interval
        elapsedTime += Time.deltaTime;

        // Follower can act
        if (elapsedTime >= actionInterval)
        {
            Debug.Log("Action Interval Passed.");
            // Until an action is performed...
            while (!actionPerformed && choiceLimit < 5)
            {
                switch (currentTactic)
                {
                    case Tactics.FullAssault:
                        Attack();
                        break;
                    case Tactics.SupportTeam:
                        Heal();
                        break;
                    case Tactics.Idle:
                        break;
                    default:
                        choiceLimit++;
                        break;
                }
                /*Debug.Log("Action has not been performed.");
                // Select potential action at random
                int action = UnityEngine.Random.Range(0, 2);
                // After action is performed, set the relevant boolean to true
                switch (action)
                {
                    // Attack an enemy
                    case 0:
                        Attack();
                        break;
                    // Heal an ally
                    case 1:
                        Heal();
                        break;
                    default:
                        choiceLimit++;
                        break; */
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
            targetHealth.TakeDamage(followerStats.Attack);
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
            targetHealth.ReceiveHealing(followerStats.Magic);
            actionPerformed = true;
        }
        else
        {
            choiceLimit++;
        }
    }

    # region Targeting Methods
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
    # endregion
}