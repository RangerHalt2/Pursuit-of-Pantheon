using UnityEngine;
using System;

public class ActiveFollowerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject followerPrefab; // Assign in Inspector

    public GameObject GenerateFollower(string followerName, FollowerClass followerClass)
    {
        GameObject followerGO = Instantiate(followerPrefab);

        // Set up Follower component
        Follower follower = followerGO.GetComponent<Follower>();
        FollowerStatblock statblock = followerGO.GetComponent<FollowerStatblock>();
        Health health = followerGO.GetComponent<Health>();

        // Assign unique ID
        statblock.followerID = Guid.NewGuid().ToString();
        statblock.displayName = followerName;
        statblock.classID = followerClass.classID;

        // Assign base stats from class
        statblock.maxHP = followerClass.baseMaxHP;
        statblock.currentHP = followerClass.baseMaxHP;
        statblock.strength = followerClass.baseStrength;
        statblock.magic = followerClass.baseMagic;
        statblock.defense = followerClass.baseDefense;
        statblock.resistance = followerClass.baseResistance;
        statblock.speed = followerClass.baseSpeed;

        // Set up health component
        if (health != null)
        {
            health.maxHealth = statblock.maxHP;
            health.currentHealth = statblock.currentHP;
        }

        // Link statblock to follower
        follower.followerStats = statblock;

        return followerGO;
    }
}
