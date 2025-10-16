// Created By: Ryan Lupoli
// A Base Class other skills will be able to inherit from. Used to streamline the skill creation process
using System;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    public abstract string skillName { get; }
    public abstract SkillType type { get; }

    [Tooltip("The percent chance of a skill successfully being used.")]
    [Range(0, 100)][SerializeField] int _accuracy = 100;
    public virtual int accuracy => _accuracy;

    public abstract void UseSkill(GameObject target);
    public abstract float CheckSkill(GameObject target);

    // Performs an accuracy check for a given skill. Returns true if the skill hits, and false if it doesn't
    public bool PerformAccuracyCheck()
    {
        // Generate a random number from 1 to 100
        int roll = UnityEngine.Random.Range(1, 100);

        // If the number is greater than the skills accuracy, the skill missses
        if (roll > accuracy)
        {
            return false;
        }
        // Otherwise the skill is successful
        else
        {
            return true;
        }
    }

    // Gets the attack stat from the caster
    public float GetStatFromObject(GameObject obj, String statType)
    {
        // First, check if the assigned object has a followerStatblock
        var follower = obj.GetComponent<FollowerStatblock>();
        // If statblock is found, return the value of the specified stat
        if (follower != null)
        {
            switch (statType.ToLower())
            {
                case "maxhp":
                    return follower.maxHP;
                case "currenthp":
                    return follower.currentHP;
                case "vigor":
                    return follower.vigor;
                case "power":
                    return follower.power;
                case "magick":
                    return follower.magick;
                case "resilience":
                    return follower.resilience;
                case "agility":
                    return follower.agility;
                case "faith":
                    return follower.faith;
                default:
                    Debug.LogWarning("Stat type " + statType + " not found in FollowerStatblock.");
                    return 0;
            }
        }

        // If not a follower, check if its an enemy
        // Replace with an enemy statblock script at a later date
        var enemy = obj.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            switch (statType.ToLower())
            {
                case "attack":
                    return enemy.Attack;
                case "defense":
                    return enemy.Defense;
                default:
                    Debug.LogWarning("Stat type " + statType + " not found in EnemyStats.");
                    return 0;
            }
        }

        // If neither component is found, return 0
        return 0;
    }
}