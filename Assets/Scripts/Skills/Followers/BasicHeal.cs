using System;
using Unity.VisualScripting;
using UnityEngine;

public class BasicHeal : SkillBase
{
    public override String skillName => "Basic Heal";
    public override SkillType type => SkillType.Healing;
    

    [Header("Skill Configuration")]
    [Tooltip("The modifier applied to a heal.")]
    [Range(0, 2)] public float healingModifier = 1f;
    
    // The attack stat used by the skill
    private float skillMag;
    

    // Function of the MultiAttack Skill
    public override void UseSkill(GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth == null)
        {
            Debug.LogWarning("Multi Attack skill called by " + gameObject.name + " with intent to hit " + target.name + ", but " + target.name + " does not have a health script!");
        }

        // Check to see if the skill hit the target
        if(!PerformAccuracyCheck())
        {
            return;
        }

        // Find the modified Maigck stat of the caster
        skillMag = (GetStatFromObject(this.gameObject, "Magick") + GetStatFromObject(this.gameObject, "BonusMagick")) * healingModifier;

        // Find the target's current health
        float targetHP = GetStatFromObject(target, "CurrentHP");

        float healing = skillMag;

        // Ensure that the attack will do at least 1 damage
        if (targetHP >=  GetStatFromObject(target, "MaxHP"))
        {
            healing = 0f;
        }

        // Apply the healing to the target
        targetHealth.ReceiveHealing(healing);
    }

    // Checks how effective the skill would be. For a healing skill this is total amount of health restored. The function will return 0 if the target is already at their maxHP
    public override float CheckSkill(GameObject target)
    {
        // Find the modified Maigck stat of the caster
        skillMag = (GetStatFromObject(this.gameObject, "Magick") + GetStatFromObject(this.gameObject, "BonusMagick")) * healingModifier;

        // Find the target's current health
        float targetHealth = GetStatFromObject(target, "CurrentHP");

        // Calculate the total healing
        float healing = skillMag;

        // Ensure that the attack will do at least 1 damage
        if (targetHealth >=  GetStatFromObject(target, "MaxHP"))
        {
            healing = 0f;
        }

        return healing;
    }
}