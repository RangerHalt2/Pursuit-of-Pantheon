using UnityEngine;

public class RecruitmentManager : MonoBehaviour
{
    [SerializeField] private ActiveFollowerGenerator activeFollowerGenerator;
    [SerializeField] private PartyManager partyManager;
    [SerializeField] private FollowerClass warriorClassSO;

    public void RecruitNewFollower()
    {
        // Generate a new follower GameObject
        GameObject newFollowerGO = activeFollowerGenerator.GenerateFollower("Bob", warriorClassSO);
        FollowerStatblock newStatblock = newFollowerGO.GetComponent<FollowerStatblock>();

        // Convert to FollowerData and add to unequipped pool
        FollowerData newFollowerData = FollowerData.FromStatblock(newStatblock);
        partyManager.AddToUnequipped(newFollowerData);

        // Destroy the temporary GameObject since we only want to store data
        Destroy(newFollowerGO);
    }
}
