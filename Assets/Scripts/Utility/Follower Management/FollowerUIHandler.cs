using UnityEngine;
using System.Collections.Generic;

public class FollowerUIHandler : MonoBehaviour
{
    [SerializeField] private PartyManager partyManager;

    // Maps followerID to the spawned GameObject
    private Dictionary<string, GameObject> equippedFollowerObjects = new Dictionary<string, GameObject>();

    // Call this after spawning equipped followers in the scene
    public void RegisterEquippedFollowerObject(FollowerData data, GameObject go)
    {
        if (data != null && go != null)
            equippedFollowerObjects[data.followerID] = go;
    }

    // Call this from a UI button, passing the index of the unequipped follower to equip
    public void EquipFollowerByIndex(int unequippedIndex)
    {
        if (unequippedIndex < 0 || unequippedIndex >= partyManager.unequippedFollowers.Count)
        {
            Debug.LogWarning("Invalid unequipped follower index.");
            return;
        }

        var follower = partyManager.unequippedFollowers[unequippedIndex];
        bool equipped = partyManager.EquipNextAvailable(follower);

        if (equipped)
        {
            Debug.Log($"Equipped follower: {follower.displayName}");
            // Optionally, spawn and register the GameObject here if needed
        }
        else
        {
            Debug.LogWarning("Could not equip follower (party may be full).");
        }
    }

    // Call this from a UI button, passing the index of the equipped follower to unequip
    public void UnequipFollowerByIndex(int equippedIndex)
    {
        if (equippedIndex < 0 || equippedIndex >= partyManager.equippedFollowers.Count)
        {
            Debug.LogWarning("Invalid equipped follower index.");
            return;
        }

        var follower = partyManager.equippedFollowers[equippedIndex];
        partyManager.UnequipFollower(follower);

        // Destroy the GameObject if it exists
        if (equippedFollowerObjects.TryGetValue(follower.followerID, out GameObject go) && go != null)
        {
            Destroy(go);
            equippedFollowerObjects.Remove(follower.followerID);
        }

        Debug.Log($"Unequipped follower: {follower.displayName}");
        // Optionally, refresh your UI here
    }
}
