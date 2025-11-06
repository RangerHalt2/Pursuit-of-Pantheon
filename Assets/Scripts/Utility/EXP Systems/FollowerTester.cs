using UnityEngine;

public class FollowerTester : MonoBehaviour
{
    public FollowerManager followerManager;

    private void Start()
    {
        followerManager.AddFollower(5);
        followerManager.AddFollower(2);
        followerManager.AddFollower(1);

        followerManager.RemoveFollower(5);
    }
}
