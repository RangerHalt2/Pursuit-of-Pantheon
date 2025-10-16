// Created By: Ryan Lupoli
// A basic multi-attack skill which allows for a character to perform multiple weaker attacks in a given turn
using System;
using Unity.VisualScripting;
using UnityEngine;

public class MultiAttack : SkillBase
{
    public override String skillName => "Multi Attack";
    public override SkillType type => SkillType.Offensive;
    

    [Header("Skill Configuration")]
    [Tooltip("Number of times Multi Attack hits the enemy.")]
    public int hits = 2;
    [Tooltip("The modifier applied to an attack.")]
    [Range(0, 1)] public float attackModifier = 0.5f;
    
    // The attack stat used by the skill
    private float skillAtk;
    

    // Function of the MultiAttack Skill
    public override void UseSkill(GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth == null)
        {
            Debug.LogWarning("Multi Attack skill called by " + gameObject.name + " with intent to hit " + target.name + ", but " + target.name + " does not have a health script!");
        }

        // Attack the target multiple times based on the amount of
        for (int attackCount = 0; attackCount < hits; attackCount++)
        {
            // Check to see if the skill hit the enemy
            if(!PerformAccuracyCheck())
            {
                continue;
            }

            // Check that the target still has a health script (If it died from an earlier hit the object is destroyed and thus no longer has a health script)
            if (targetHealth == null)
            {
                return;
            }

            // Get the modified Attack stat of the caster
            skillAtk = GetStatFromObject(this.gameObject, "Power") * attackModifier;

            // Find the defense of the assigned target
            float targetDef = GetStatFromObject(target, "Defense");

            // Calculate Skill Damage
            float damage = skillAtk - targetDef;

            // Ensure that the attack will do at least 1 damage
            if (damage < 1f)
            {
                damage = 1f;
            }

            // Deal damage to the target
            targetHealth.TakeDamage(damage);
        }
    }

    // Checks how effective the skill would be. For an offensive Skill this means that it will return the amount of damage it would deal
    public override float CheckSkill(GameObject target)
    {
        // Find the modified Attack stat of the caster
        skillAtk = GetStatFromObject(this.gameObject, "Power") * attackModifier;

        // Find the target's defense
        float targetDef = GetStatFromObject(target, "Defense");

        // Calculate the skills damage per hit
        float damage = skillAtk - targetDef;

        // Ensure that the attack will do at least 1 damage
        if (damage < 1f)
        {
            damage = 1f;
        }

        return damage * hits;
    }
}
