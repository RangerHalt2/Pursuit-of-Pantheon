using System.Collections.Generic;
using UnityEngine;

public static class NameList
{
    public static readonly List<string> names = new List<string>()
    {
        "Alyssa",
        "Alan",
        "Logan",
        "Bryan",
        "Eden",
        "William",
        "Wai",
        "Athena",
        "Ryan",
        "Ethan",
        "James",
        "John",
        "Matthew",
        "Daniel",
        "Joshua",
        "Emily",
        "Ailime", //Lowkey this is a sneak from Logan, it's a shitty play on Emilia.
        "Mia",
        "Reine",
        "Yoshino",
        "Tohka",
        "Taylor",
        "Jordan"
    };


    public static string GetRandomName()
    {
        string ret = "";

        ret = names[Random.Range(0, names.Count)];

        return ret;
    }
}


public class RecruitmentManager : MonoBehaviour
{
    [SerializeField] private ActiveFollowerGenerator activeFollowerGenerator;
    [SerializeField] private PartyManager partyManager;

    public void RecruitNewFollower()
    {
        // Get a random base class
        FollowerClass randomClass = FollowerFactory.GetRandomClass();

        // Generate a new follower GameObject
        GameObject newFollowerGO = activeFollowerGenerator.GenerateFollower(NameList.GetRandomName(), randomClass);
        FollowerStatblock newStatblock = newFollowerGO.GetComponent<FollowerStatblock>();

        // Convert to FollowerData and add to unequipped pool
        FollowerData newFollowerData = FollowerData.FromStatblock(newStatblock);
        partyManager.AddToUnequipped(newFollowerData);
        if (partyManager.equippedFollowers.Count < 1) partyManager.EquipNextAvailable(newFollowerData);

        // Destroy the temporary GameObject since we only want to store data
        Destroy(newFollowerGO);
        FindAnyObjectByType<FollowerUIHandler>().RefreshFollowerPanels();
    }
}
