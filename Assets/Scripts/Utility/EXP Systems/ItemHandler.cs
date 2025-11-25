using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public static ItemHandler Instance;

    public enum ItemType
    {
        UpgradeToken,
        RewindToken
    }

    public int upgradeTokenCount = 0;
    public int rewindTokenCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(ItemType item, int amount = 1)
    {
        switch (item)
        {
            case ItemType.UpgradeToken:
                upgradeTokenCount += amount;
                break;

            case ItemType.RewindToken:
                rewindTokenCount += amount; 
                break;
        }

        Debug.Log($"Added {amount} {item}. Total now: {GetItemCount(item)}");
    }

    public bool RemoveItem(ItemType item, int amount = 1)
    {
        if (GetItemCount(item) < amount)
        {
            return false;
        }

        switch (item)
        {
            case ItemType.UpgradeToken:
                upgradeTokenCount -= amount;
                break;

            case ItemType.RewindToken:
                rewindTokenCount -= amount;
                break;
        }

        return true;
    }

    public int GetItemCount(ItemType item)
    {
        switch (item)
        {
            case ItemType.UpgradeToken:
                return upgradeTokenCount;

            case ItemType.RewindToken:
                return rewindTokenCount;

            default:
                return 0;
        }
    }
}
