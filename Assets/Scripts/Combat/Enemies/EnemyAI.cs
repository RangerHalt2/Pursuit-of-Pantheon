// Created By: Ryan Lupoli
// This script manages the AI of the enemies
// DISCLAIMER: As of right now this script is mostly copied from the follower AI. If certain comments, tooltips, etc erroneously make reference to a follower, that is why
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Options for follower tactics. More options can be easily added as is necessary
    public enum Tactic
    {
        // Maximizes the potential damage dealt to enemies
        MaximizeDamage,
        // Prioritize killing blows
        FinishTargets
    }

    #region Variables
    [Header("Team Settings")]
    [Tooltip("Team ID's for allies. Will be targeted by the enemies's healing and buffing skills.")]
    [SerializeField] private int[] alliedTeamIds;
    [Tooltip("Team ID's for enemies. Will be targeted by a enemies's offensive and debuffing skills.")]
    [SerializeField] private int[] enemyTeamIds;

    [Tooltip("The tacics for the selected enemy. Determins which skills they use and how they use them")]
    [SerializeField] private Tactic selectedTactic;

    private EnemyStatblock enemyStatblock;
    #endregion

    void Awake()
    {
        // Check for EnemyStatblock have been assigned
        enemyStatblock = GetComponent<EnemyStatblock>();

        if (enemyStatblock == null)
        {
            Debug.LogWarning("EnemyAI: EnemyStatblock Script not found on " + gameObject.name + ". Enemy AI will not function.");
        }
    }

    public void TakeTurn()
    {
        //Debug.Log(gameObject.name + " acted!");
        switch (selectedTactic)
        {
            // Full Assault Tactic
            case Tactic.MaximizeDamage:
                MaximizeDamageLogic();
                break;
            case Tactic.FinishTargets:
                FinishTargetsLogic();
                break;
        }

    }

    #region Tactic Logic
    // Target the enemy that allows them to deal the highest potential damage to a target. They will ignore any and all supportive skills they have access to.
    public void MaximizeDamageLogic()
    {
        // Ensure enemy has a statblock
        if (enemyStatblock == null)
        {
            Debug.LogWarning("EnemyStatblock not found on " + gameObject.name);
            return;
        }

        // Ensure that statblock has at least one skill assigned
        ISkill[] skills = enemyStatblock.GetSkills();
        if (skills == null || skills.Length == 0)
        {
            Debug.LogWarning(gameObject.name + " has no skills.");
            return;
        }

        // Get all registered combatants from CombatManager
        List<ICombatant> allCombatants = CombatManager.instance?.GetCombatants();
        if (allCombatants == null)
        {
            Debug.LogWarning("CombatManager not found or combatants list is null.");
            return;
        }

        // Initialize variables to track the best target
        GameObject bestTarget = null;
        ISkill bsetSkill = null;
        float bestEffectiveDamage = 0f;

        // Check ever combatant in the scene
        foreach (ICombatant target in allCombatants)
        {
            // Skip any combatant that does not have an enemy teamID
            if (!enemyTeamIds.Contains(target.TeamID) || !IsTargetValid(target))
            {
                continue;
            }

            // Assign the traget Game Object and health componenet
            GameObject targetObject = target.GetGameObject();
            Health targetHealth = targetObject.GetComponent<Health>();

            // If for any reason there is no health component or statblock, or the current HP is less than or equal to 0, skip the target
            if (targetHealth == null || targetHealth.currentHealth <= 0)
            {
                continue;
            }

            // Check every skill the follower has
            foreach (ISkill skill in skills)
            {
                // If the skill is offensive...
                if (skill is SkillBase skillBase && skillBase.type == SkillType.Offensive)
                {
                    // Calculate the estimate damage dealt to the target
                    float estimatedDamage = skillBase.CheckSkill(targetObject);
                    float effectiveDamage = Mathf.Min(estimatedDamage, targetHealth.currentHealth);

                    // If the effective damage is higher than the recorded best effective damagge
                    if (effectiveDamage > bestEffectiveDamage)
                    {
                        // Update the selected target
                        bestEffectiveDamage = effectiveDamage;
                        bestTarget = targetObject;
                        bsetSkill = skill;
                    }
                }
            }
        }
        // If a target was found...
        // Double check the taregt is still alive
        if (bestTarget == null || bestTarget.GetComponent<Health>() == null || bestTarget.GetComponent<Health>().currentHealth <= 0)
        {
            Debug.LogWarning("Best target became invalid before skill could be used.");
            return;
        }

        if (bsetSkill != null && bestTarget != null && bestTarget.GetComponent<Health>()?.currentHealth > 0)
        {
            Debug.Log(gameObject.name + " uses " + ((SkillBase)bsetSkill).skillName + " on " + bestTarget.name + ". Effective Damage: " + bestEffectiveDamage);
            bsetSkill.UseSkill(bestTarget);
        }
        else
        {
            Debug.Log(gameObject.name + "Found no valid offensive actions.");
        }
    }

    /* 
    This tacitc prioritizes securing kills agains enemies. Actions will be perforemd in the following order of priority
    Priority 1: Try to kill an enemy (Will randomly attack an enemy that can be killed by one of its skills)
    Priority 2: Heal self if health is below 25%
    Priority 3: Randomly use a Buff, Debuff, or Attacking Skill
    */
    public void FinishTargetsLogic()
    {
        if (enemyStatblock == null)
        {
            Debug.LogWarning("EnemyStatblock not found on " + gameObject.name);
            return;
        }

        ISkill[] skills = enemyStatblock.GetSkills();
        if (skills == null || skills.Length == 0)
        {
            Debug.LogWarning(gameObject.name + " has no skills.");
            return;
        }

        List<ICombatant> allCombatants = CombatManager.instance?.GetCombatants();
        if (allCombatants == null)
        {
            Debug.LogWarning("CombatManager not found or combatants list is null.");
            return;
        }

        GameObject self = gameObject;
        Health selfHealth = self.GetComponent<Health>();

        if (selfHealth == null)
        {
            Debug.LogWarning("No Health component found on " + gameObject.name);
            return;
        }

        // Priority 1: Try to finish off an enemy target
        #region Finish Targets Priority 1
        List<GameObject> killableTargets = new List<GameObject>();
        List<ISkill> killSkills = new List<ISkill>();

        // Check all combatants in the scene
        foreach (ICombatant target in allCombatants)
        {
            // If the target is on the same team, or otherwise invalid, skip it
            if (!enemyTeamIds.Contains(target.TeamID) || !IsTargetValid(target))
            {
                continue;
            }

            // Assign the game Object and health componenet
            GameObject targetObj = target.GetGameObject();
            Health targetHealth = targetObj.GetComponent<Health>();

            // If for any reason there is no health component or statblock, or the current HP is less than or equal to 0, skip the target
            if (targetHealth == null || targetHealth.currentHealth <= 0)
            {
                continue;
            }

            // For every skill that can be used
            foreach (ISkill skill in skills)
            {
                // If the skill is offensive...
                if (skill is SkillBase skillBase && skillBase.type == SkillType.Offensive)
                {
                    // Calculate the estimated damage of the skill
                    float estimatedDamage = skillBase.CheckSkill(targetObj);
                    // If the skill would kill the target, add it to the list of killable targets
                    if (estimatedDamage >= targetHealth.currentHealth)
                    {
                        killableTargets.Add(targetObj);
                        killSkills.Add(skill);
                    }
                }
            }
        }
        // If there is at least one killable target
        if (killableTargets.Count > 0)
        {
            // Randomly select a targte from the list
            int index = UnityEngine.Random.Range(0, killableTargets.Count);
            GameObject target = killableTargets[index];
            ISkill skill = killSkills[index];

            // Use the skill to kill the target
            //Debug.Log(gameObject.name + " finishes " + target.name + " with " + ((SkillBase)skill).skillName);
            skill.UseSkill(target);
            return;
        }
        #endregion

        // Priority 2: Heal Self if low on health
        #region Finish Targets Priority 2
        // Get current health as a percent
        float healthPercent = (float)selfHealth.currentHealth / selfHealth.maxHealth;
        // If current health is below 25 percent
        if (healthPercent < 0.25f)
        {
            // For every skill which can be used
            foreach (ISkill skill in skills)
            {
                // If the skill is a healing skill
                if (skill is SkillBase skillBase && skillBase.type == SkillType.Healing)
                {
                    // Heal self
                    Debug.Log(gameObject.name + " heals themselves with " + skillBase.skillName);
                    skill.UseSkill(self);
                    return;
                }
            }
        }
        #endregion

        // Priority 3: Use any non healing skill at random.
        // Buffs will be used on allies, debuffs or offensive skill will be used on enemies.
        // If a Buff or debuff is seleceted, but not buffs or debuffs are found, then it will default to an offensive skill
        #region Finish Target Priority 3
        // Pick a random number from between 1 and 3
        int randomChoice = UnityEngine.Random.Range(0, 3);
        // Create a list of all enemies in the scene
        List<GameObject> validEnemies = allCombatants.Where(c => enemyTeamIds.Contains(c.TeamID) && IsTargetValid(c)).Select(c => c.GetGameObject()).ToList();

        switch (randomChoice)
        {
            // Buff random ally
            case 0:
                // Create a list of all allies in the scene
                List<GameObject> allies = allCombatants.Where(c => alliedTeamIds.Contains(c.TeamID) && IsTargetValid(c)).Select(c => c.GetGameObject()).ToList();
                // Find the first buff skill in the follower has
                ISkill buffSkill = skills.FirstOrDefault(s => s is SkillBase sb && sb.type == SkillType.Buff);
                // If a buff skill is found and there is at least 1 ally
                if (buffSkill != null && allies.Count > 0)
                {
                    // Select an ally at random
                    GameObject randomAlly = allies[UnityEngine.Random.Range(0, allies.Count)];

                    //Debug.Log(gameObject.name + " buffs " + randomAlly.name + " with " + ((SkillBase)buffSkill).skillName);

                    // Use the buffing skill on the ally
                    buffSkill.UseSkill(randomAlly);
                    return;
                }
                // If no buff skill was found, break out of the Switch statement to default to an offensive skill
                break;
            // Debuff random enemy
            case 1:
                // Find the first default skill the follower has
                ISkill debuffSkill = skills.FirstOrDefault(s => s is SkillBase sb && sb.type == SkillType.Debuff);
                // If buff skill is found and there is at least 1 enemy
                if (debuffSkill != null && validEnemies.Count > 0)
                {
                    // Select an enemy at random
                    GameObject randomEnemy = validEnemies[UnityEngine.Random.Range(0, validEnemies.Count)];

                    //Debug.Log(gameObject.name + " debuffs " + randomEnemy.name + " with " + ((SkillBase)debuffSkill).skillName);

                    // Use the debuffing skill on the the enemy
                    debuffSkill.UseSkill(randomEnemy);
                    return;
                }
                // If no debuff skill was found, break out of the Switch statement to default to an offensive skill
                break;
            // Attack random enemy
            case 2:
                // Break out of the switch statement
                break;
        }
        // If no buff/debuff was found, or if the random choice was offensive, use an offensive skill on a random enemy

        // Create a list of all offensive skills
        ISkill offensiveSkill = skills.FirstOrDefault(s => s is SkillBase sb && sb.type == SkillType.Offensive);

        // If an offensive skill is found, and there is at least 1 enemy
        if (offensiveSkill != null && validEnemies.Count > 0)
        {
            // Select an enemy at random
            GameObject randomEnemy = validEnemies[UnityEngine.Random.Range(0, validEnemies.Count)];

            //Debug.Log(gameObject.name + " attacks " + randomEnemy.name + " with " + ((SkillBase)offensiveSkill).skillName);

            // Use the offensive skill on the enemy
            offensiveSkill.UseSkill(randomEnemy);
            return;
        }
        #endregion
        Debug.Log(gameObject.name + " found no valid skill to use this turn.");
    }

    
    #endregion
    
    private bool IsTargetValid(ICombatant combatant)
    {
        if (combatant == null)
            return false;

        try
        {
            var obj = combatant.GetGameObject();
            if (obj == null)
                return false;

            var health = obj.GetComponent<Health>();
            return health != null && health.currentHealth > 0;
        }
        catch (MissingReferenceException)
        {
            return false;
        }
    }
}
