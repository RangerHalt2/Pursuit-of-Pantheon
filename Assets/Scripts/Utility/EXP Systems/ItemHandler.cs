using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    //This should store variables and change their counts.
    //Public functions to get/set should be the core of this script.

    public static ItemHandler Instance;

    private Dictionary<string, int> upgradeCounts = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadUpgrades(); //load saved data
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GiveUpgrade(string upgradeID)
    {
        if (!upgradeCounts.ContainsKey(upgradeID))
        {
            upgradeCounts[upgradeID] = 0;
        }

        upgradeCounts[upgradeID]++;
        PlayerPrefs.SetInt(upgradeID, upgradeCounts[upgradeID]);
        PlayerPrefs.Save();
    }

    public int GetUpgradeCount(string upgradeID)
    {
        return upgradeCounts.ContainsKey(upgradeID) ? upgradeCounts[upgradeID] : 0;
    }

    private void LoadUpgrades()
    {
        string[] knownUpgrades = { "FollowerUpgrade" };

        foreach (string id in knownUpgrades)
        {
            int count = PlayerPrefs.GetInt(id, 0);
            upgradeCounts[id] = count;
        }
    }
}

/*for the upgrades being given this needs to be called in the scripts
 UpgradeManager.Instance.GiveUpgrade("FollowerUpgrade");
The name of the upgrade can always be changed it is just a placeholder*/
