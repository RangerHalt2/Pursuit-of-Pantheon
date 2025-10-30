using System;
using Unity.VisualScripting;
using UnityEngine;

public class BasicAttack : SkillBase
{
    public override String skillName => "Basic Attack";
    public override SkillType type => SkillType.Offensive;
    

    [Header("Skill Configuration")]
    [Tooltip("The modifier applied to an attack.")]
    [Range(0, 2)] public float attackModifier = 1f;
    
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

        // Check to see if the skill hit the enemy
        if(!PerformAccuracyCheck())
        {
            return;
        }

        // Get the modified Attack stat of the caster
        skillAtk = (GetStatFromObject(this.gameObject, "Power") + GetStatFromObject(this.gameObject, "BonusPower"))  * attackModifier;

        // Find the defense of the assigned target
        float targetDef = GetStatFromObject(target, "Resilience");

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

    // Checks how effective the skill would be. For an offensive Skill this means that it will return the amount of damage it would deal
    public override float CheckSkill(GameObject target)
    {
        // Find the modified Attack stat of the caster
        skillAtk = (GetStatFromObject(this.gameObject, "Power") + GetStatFromObject(this.gameObject, "BonusPower")) * attackModifier;

        // Find the target's defense
        float targetDef = GetStatFromObject(target, "Resilience");

        // Calculate the skills damage per hit
        float damage = skillAtk - targetDef;

        // Ensure that the attack will do at least 1 damage
        if (damage < 1f)
        {
            damage = 1f;
        }

        return damage;
    }
}