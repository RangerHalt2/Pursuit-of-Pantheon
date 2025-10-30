// Created By: Ryan Lupoli
// A Base class other Divine Skills will be able to inherit from
using System.Collections;
using UnityEngine;

public abstract class DivineSkillBase : MonoBehaviour, IDivineSkill
{
    #region Variables
    
    public abstract string skillName { get; }

    [Tooltip("The amount of AP required to cast a certain divine skill.")]
    [SerializeField] int _apCost = 1;
    public virtual int apCost => _apCost;

    [Tooltip("The amount of time (in seconds) a skill will be on cooldown before it can be used again.")]
    [SerializeField] float _cooldown = 1f;
    public virtual float cooldown => _cooldown;

    [HideInInspector] public bool onCooldown = false;

    // Cached refernce to AP Manager
    protected ActionPointManager apManager;

    public IEnumerator cooldownCoroutine{ get; }
    #endregion

    #region Methods
    public abstract void UseSkill();

    // Checks to see if the player has enough AP to use a skill. Returns true and reduces AP if they do
    public bool CheckAP()
    {
        // If there is no action point manager assigned
        if (apManager == null)
        {
            // Check the scene for an Action Point Manager
            apManager = FindFirstObjectByType<ActionPointManager>();
            // If no AP Manager could be found
            if (apManager == null)
            {
                Debug.LogWarning(skillName + ": Could not find an Action Point Manager at runtime!");
                return false;
            }
        }

        // Check how much AP the player currently has
        int currentAP = apManager.getAP();
        // If current AP is greater than or equal to the apCost...
        if (currentAP >= apCost)
        {
            apManager.decreaseAP(apCost);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Temporarily sets a skill as on cooldown, preventing its use
    public IEnumerator CooldownCoroutine()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
    #endregion
}
