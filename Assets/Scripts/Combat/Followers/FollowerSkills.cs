// Created By: Ryan Lupoli
// This script is intended to be used for storing the skills followers have access to

using System.Diagnostics;
using UnityEngine;

public class FollowerSkills : MonoBehaviour
{

    FollowerStats followerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Reference Follower Stats Script
        followerStats = GetComponent<FollowerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calls for a skill based on the provided skill ID.
    void UseSkill(int skillID)
    {
        switch (skillID)
        {
            // Skill: Basic Attack
            case 0:
                break;
            // Invalid TeamId called
            default:
                UnityEngine.Debug.LogWarning("InvalidTeamID!");
                break;
        }
    }

    #region Skills
    // Skill 1: Basic Attack
    // Player deals damage equal to their attack stat
    public float BasicAttack()
    {
        float damage = followerStats.Attack;
        return damage;
    }

    #endregion
}
