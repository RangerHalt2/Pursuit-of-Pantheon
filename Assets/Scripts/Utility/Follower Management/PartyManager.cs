using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<FollowerData> equippedFollowers = new List<FollowerData>(6);
    public List<FollowerData> unequippedFollowers = new List<FollowerData>();

    // Equip a follower to a party position (1-6)
    public void EquipFollower(FollowerData follower, int position)
    {
        if (position < 1 || position > 6)
        {
            Debug.LogWarning("Invalid party position.");
            return;
        }

        // Remove from unequipped if present
        unequippedFollowers.Remove(follower);

        // If already equipped, update position
        if (equippedFollowers.Contains(follower))
        {
            follower.position = position;
            return;
        }

        // If slot is occupied, unequip the current follower
        FollowerData existing = equippedFollowers.Find(f => f.position == position);
        if (existing != null)
        {
            UnequipFollower(existing);
        }

        follower.position = position;
        equippedFollowers.Add(follower);
    }

    // Unequip a follower (moves to unequipped list, sets position to 0)
    public void UnequipFollower(FollowerData follower)
    {
        equippedFollowers.Remove(follower);
        follower.position = 0;
        if (!unequippedFollowers.Contains(follower))
            unequippedFollowers.Add(follower);
    }

    // Add a new follower to unequipped pool
    public void AddToUnequipped(FollowerData follower)
    {
        if (!unequippedFollowers.Contains(follower))
            unequippedFollowers.Add(follower);
        follower.position = 0;
    }

    public bool EquipNextAvailable(FollowerData follower)
    {
        // Only allow up to 5 equipped followers
        if (equippedFollowers.Count >= 5)
        {
            Debug.LogWarning("Party is full. Cannot equip more followers.");
            return false;
        }

        // Find the next available position (1-6, skipping occupied)
        HashSet<int> occupied = new HashSet<int>();
        foreach (var f in equippedFollowers)
            occupied.Add(f.position);

        int nextPos = 1;
        while (nextPos <= 6 && occupied.Contains(nextPos))
            nextPos++;

        if (nextPos > 6)
        {
            Debug.LogWarning("No available slot found.");
            return false;
        }

        // Remove from unequipped, set position, and add to equipped
        unequippedFollowers.Remove(follower);
        follower.position = nextPos;
        equippedFollowers.Add(follower);
        return true;
    }

    public bool MoveEquippedFollower(FollowerData follower, int newPosition)
    {
        if (newPosition < 1 || newPosition > 6)
        {
            Debug.LogWarning("Invalid position.");
            return false;
        }
        if (!equippedFollowers.Contains(follower))
        {
            Debug.LogWarning("Follower not equipped.");
            return false;
        }

        // Check if another follower is already at the new position
        var other = equippedFollowers.Find(f => f.position == newPosition);
        if (other != null && other != follower)
        {
            // Swap positions
            int oldPos = follower.position;
            other.position = oldPos;
        }
        follower.position = newPosition;
        return true;
    }
}