// Created By: Ryan Lupoli
// A Divine Skill intended for Quilla
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting.FullSerializer.Internal;

public class TempusFugit : DivineSkillBase
{
    public override string skillName => "Tempus Fugit";

    [Header("Skill Configuration")]
    [Tooltip("Determines how many followers are effected by the skill.")]
    public int effectedAllies = 2;
    [Tooltip("Determines how much of a boost to attack speed allies will recieve.")]
    public float bonusAgilityModifier = .5f;
    [Tooltip("Determines how long (in seconds) the effects of Tempus Fugit will effect followers")]
    public float duration = 5f;


    public override void UseSkill()
    {
        if (!onCooldown)
        {

            if (CheckAP())
            {
                // Find all followers in the current scene
                GameObject[] allFollowers = GameObject.FindGameObjectsWithTag("Follower");

                if (allFollowers.Length == 0)
                {
                    Debug.LogWarning("No followers found in the scene!");
                    return;
                }

                // Select followers at random
                List<GameObject> shuffled = allFollowers.OrderBy(f => Random.value).ToList();
                List<GameObject> selectedFollowers = shuffled.Take(Mathf.Min(effectedAllies, allFollowers.Length)).ToList();

                // Apply the buff to the selected followers
                foreach (GameObject follower in selectedFollowers)
                {
                    // Find the follower's statblock
                    FollowerStatblock stats = follower.GetComponent<FollowerStatblock>();
                    if (stats != null)
                    {
                        // Start coroutine to apply a the buff
                        StartCoroutine(ApplyAttackSpeedBoost(stats));
                    }
                    else
                    {
                        Debug.LogWarning(follower.name + " is missing a FollowerStatblock component!");
                    }
                }
                CooldownCoroutine();
            }
            else
            {
                Debug.Log("Not enough AP to use Tempus Fugit.");
            }
        }
        else
        {
            Debug.Log("Tempus Fugit is on cooldown.");
        }
    }
    
    // Applys bonus agility to the follower by adding their base agility multiplied by the modifier to their bonus attack speed for the duration of the skill
    private IEnumerator ApplyAttackSpeedBoost(FollowerStatblock follower)
    {
        // Calculate the bonus agility that the follower will gain
        float tempusFugitBoost = follower.agility * bonusAgilityModifier;

        // Apply the bonus
        follower.bonusAgility += tempusFugitBoost;
        //Debug.Log("Tempest Fugit: " + follower.name + " temporarily gained " + tempusFugitBoost + " points of bonus Agility.");

        yield return new WaitForSeconds(duration);

        // Remove the bonus
        follower.bonusAgility -= tempusFugitBoost;
       //Debug.Log("Tempest Fugit: Boost Faded.");
    }
}
