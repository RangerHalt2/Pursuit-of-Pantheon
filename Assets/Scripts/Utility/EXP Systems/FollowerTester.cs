using UnityEngine;

public class FollowerTester : MonoBehaviour
{
    public FollowerManager followerManager;

    private void Start()
    {
        followerManager.AddFollower("Archer");
        followerManager.AddFollower("Warrior");
        followerManager.AddFollower("Mage");

        followerManager.AddExperienceToAll(100);

        followerManager.RemoveFollower("Warrior");
    }
}
