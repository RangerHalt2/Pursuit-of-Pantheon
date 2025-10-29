using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    //Made to work with the EXP System in order to gain exp when followers are added to the party

    [Header("Follower Data")]
    public List<EXPSystem> followers = new List<EXPSystem>();

    [Header("References")]
    public PlayerProfile playerProfile;

    public void AddFollower(string name)
    {
        followers.Add(new EXPSystem(name));
        Debug.Log($"Follower {name} added");
        UpdatePlayerLevel();
    }

    public void RemoveFollower(string name)
    {
        var follower = followers.Find(f => f.name == name);

        if (follower != null )
        {
            followers.Remove(follower);
            Debug.Log($"Follower {name} removed");
            UpdatePlayerLevel();
        }

        else
        {
            Debug.LogWarning($"Follower {name} not found");
        }
    }

    public void AddExperienceToAll(int exp)
    {
        foreach (var follower in followers)
        {
            follower.AddExperience(exp);
        }
    }

    private void UpdatePlayerLevel()
    {
        if (playerProfile != null)
        {
            playerProfile.UpdateLevel(followers.Count);
        }

        else
        {
            Debug.LogWarning("No player profile assigned to follower manager");
        }
    }
}
