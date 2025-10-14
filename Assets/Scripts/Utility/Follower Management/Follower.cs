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

        block.power = followerStats.power;
        block.magick = followerStats.magick;

        block.resilience = followerStats.resilience;
        block.faith = followerStats.faith;

        block.agility = followerStats.agility;

        block.bonusMaxHP = followerStats.bonusMaxHP;

        block.bonusPower = followerStats.bonusPower;
        block.bonusMagick = followerStats.bonusMagick;

        block.bonusResilience = followerStats.bonusResilience;
        block.bonusFaith = followerStats.bonusFaith;

        block.bonusAgility = followerStats.bonusAgility;
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

        followerStats.power = stats.power;
        followerStats.magick = stats.magick;

        followerStats.resilience = stats.resilience;
        followerStats.faith = stats.faith;

        followerStats.agility = stats.agility;

        followerStats.bonusMaxHP = stats.bonusMaxHP;

        followerStats.bonusPower = stats.bonusPower;
        followerStats.bonusMagick = stats.bonusMagick;

        followerStats.bonusResilience = stats.bonusResilience;
        followerStats.bonusFaith = stats.bonusFaith;

        followerStats.bonusAgility = stats.bonusAgility;
    }
}
