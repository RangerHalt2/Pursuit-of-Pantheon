using UnityEngine;
using System;

public class ActiveFollowerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject followerPrefab;

    public GameObject GenerateFollower(string followerName, FollowerClass followerClass)
    {
        GameObject followerGO = Instantiate(followerPrefab);

        // Set up Follower component
        Follower follower = followerGO.GetComponent<Follower>();
        FollowerStatblock statblock = followerGO.GetComponent<FollowerStatblock>();

        // Assign unique ID and display name
        statblock.followerID = Guid.NewGuid().ToString();
        statblock.displayName = followerName;

        // Link statblock to follower
        follower.followerStats = statblock;

        // Add the class-specific component
        if (!string.IsNullOrEmpty(followerClass.unityClassName))
        {
            var type = Type.GetType(followerClass.unityClassName);
            if (type == null)
            {
                // Try with namespace if needed
                type = Type.GetType("PursuitOfPantheon.Classes." + followerClass.unityClassName);
            }
            if (type != null && typeof(BaseClass).IsAssignableFrom(type))
            {
                var classComponent = (BaseClass)followerGO.AddComponent(type);
                classComponent.classData = followerClass;
                classComponent.enabled = true; // Ensure it's enabled
            }
            else
            {
                Debug.LogWarning($"Could not find or assign BaseClass type for {followerClass.unityClassName}");
            }
        }

        return followerGO;
    }
}
