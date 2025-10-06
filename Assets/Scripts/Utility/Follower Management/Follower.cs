// Created By: Ryan Lupoli
// This is a basic script used to identify followers and update their statblock
// The ApplyToStatblock and LoadFromStatblock mehtods are required as part of the process to store party data across scenes
using System.ComponentModel;
using UnityEngine;

public class Follower : MonoBehaviour
{

    [SerializeField] private Health followerHealth;
    public FollowerStatblock followerStats;

    // Generates a new stat block based on the one currently attatched to the follower
    public void ApplyToStatblock(FollowerStatblock block)
    {

        block.followerID = followerStats.followerID;
        block.displayName = followerStats.displayName;
        block.position = followerStats.position;

        block.classID = followerStats.classID;

        block.maxHP = followerStats.maxHP;
        block.currentHP = followerStats.currentHP;

        block.strength = followerStats.strength;
        block.magic = followerStats.magic;

        block.defense = followerStats.defense;
        block.resistance = followerStats.resistance;

        block.speed = followerStats.speed;

        block.bonusMaxHP = followerStats.bonusMaxHP;

        block.bonusStrength = followerStats.bonusStrength;
        block.bonusMagic = followerStats.bonusMagic;

        block.bonusDefense = followerStats.bonusDefense;
        block.bonusResistance = followerStats.bonusResistance;

        block.bonusSpeed = followerStats.bonusSpeed;
    }

    // Overwrites a follower's stats with those of the given statblock
    public void LoadFromStatblock(FollowerStatblock stats)
    {
        followerStats.followerID = stats.followerID;
        followerStats.displayName = stats.displayName;
        followerStats.position = stats.position;

        followerStats.classID = stats.classID;

        followerStats.maxHP = stats.maxHP;
        followerStats.currentHP = stats.currentHP;

        followerStats.strength = stats.strength;
        followerStats.magic = stats.magic;

        followerStats.defense = stats.defense;
        followerStats.resistance = stats.resistance;

        followerStats.speed = stats.speed;

        followerStats.bonusMaxHP = stats.bonusMaxHP;

        followerStats.bonusStrength = stats.bonusStrength;
        followerStats.bonusMagic = stats.bonusMagic;

        followerStats.bonusDefense = stats.bonusDefense;
        followerStats.bonusResistance = stats.bonusResistance;

        followerStats.bonusSpeed = stats.bonusSpeed;
    }
}
