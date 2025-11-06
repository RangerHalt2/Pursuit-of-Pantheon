using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    //Made to work with the EXP System in order to gain exp when followers are added to the party

    [Header("Follower Data")]
    public EXPSystem expSystem;

    [Header("References")]
    public PlayerProfile playerProfile;

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
}
