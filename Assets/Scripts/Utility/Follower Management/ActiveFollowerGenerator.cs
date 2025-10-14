using UnityEngine;
using System;

public class ActiveFollowerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject followerPrefab;

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
        statblock.power = followerClass.baseStrength;
        statblock.magick = followerClass.baseMagic;
        statblock.resilience = followerClass.baseDefense;
        statblock.faith = followerClass.baseResistance;
        statblock.agility = followerClass.baseSpeed;

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
