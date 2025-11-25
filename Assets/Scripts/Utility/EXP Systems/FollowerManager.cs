using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    //Made to work with the EXP System in order to gain exp when followers are added to the party

    [Header("Follower Data")]
    public EXPSystem expSystem;

    [Header("References")]
    public PlayerProfile playerProfile;

    //Added by Alyssa
    [Header("Active Folowers")]
    public List<FollowerStatblock> equippedFollowers = new List<FollowerStatblock>();

    private void Awake()
    {
        if (playerProfile == null)
        {
            Debug.LogError("PlayerProfile is not assigned");
            return;
        }

        expSystem = new EXPSystem(playerProfile, 0);
    }

    public void AddFollower(int amount)
    {
        expSystem.amount += amount;
        expSystem.SyncLevel();
        Debug.Log($"{amount} followers added");
    }

    public void RemoveFollower(int amount)
    {
        expSystem.amount = Mathf.Max(0, expSystem.amount - amount);
        expSystem.SyncLevel();
        Debug.Log($"{amount} followers removed");
    }

    //Added by Alyssa
    public void AddEquippedFollower(FollowerStatblock follower)
    {
        if (!equippedFollowers.Contains(follower))
        {
            equippedFollowers.Add(follower);
        }
    }

    public void RemoveEquippedFollower(FollowerStatblock follower)
    {
        equippedFollowers.Remove(follower);
    }

    public int GetHighestSkill(string skillName)
    {
        int highest = 0;

        foreach (var follower in equippedFollowers)
        {
            if (follower == null) continue;

            int value = follower.GetSkill(skillName);
            if (value > highest)
            {
                highest = value;
            }
        }

        return highest;
    }
}
