using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public static ItemHandler Instance;

    public int itemCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(int amount = 1)
    {
        itemCount += amount;
        Debug.Log("Item count is now: " + itemCount);
    }

    public bool RemoveItem(int amount = 1)
    {
        if (itemCount >= amount)
        {
            itemCount -= amount;
            return true;
        }
        return false;
    }

    public bool HasItem()
    {
        return itemCount > 0;
    }
}
