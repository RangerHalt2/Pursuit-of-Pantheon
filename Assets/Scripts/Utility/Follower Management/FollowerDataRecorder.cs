// Created By: Ryan Lupoli
// This script records the data of followers in a scene and sends it to the bootstrapper.
using UnityEngine;
using System.Linq;

public class FollowerDataRecorder : MonoBehaviour
{
    // Records the statblocks of all followers in the current scene
    public void RecordAllFollowers()
    {
        var followers = Object.FindObjectsByType<Follower>(FindObjectsSortMode.None);

        // Clear previous saved data 
        Transform partyDataTransform = Bootstrapper.Instance.partyData.transform;
        foreach (Transform child in partyDataTransform)
        {
            Destroy(child.gameObject);
        }

        // For every follower found
        foreach (var follower in followers)
        {
            // Create an empty GameObject for the stat block
            GameObject statGO = new GameObject($"FollowerStat_{follower.followerStats.followerID}");
            // Make this GameObject a child of the Party Data
            statGO.transform.SetParent(partyDataTransform);

            // Add the FollowerStatBlock component and populate it
            FollowerStatblock statBlock = statGO.AddComponent<FollowerStatblock>();
            follower.ApplyToStatblock(statBlock);
        }
    }
}
