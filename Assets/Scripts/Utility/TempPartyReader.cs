using System;
using TMPro;
using UnityEngine;

public class TempPartyReader : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI equippedFollowers;
    [SerializeField] private TextMeshProUGUI unEquippedFollowers;

    private PartyManager partyManager;


    private void Start()
    {
        partyManager = GetComponent<PartyManager>();
    }

    private void Update()
    {
        PopulateUnEquippedFollowers();
        PopulateEquippedFollowers();
    }

    private void PopulateEquippedFollowers()
    {
        string message = "Equpped Followers:\n";

        int i = 0;

        foreach(FollowerData FD in partyManager.equippedFollowers)
        {
            message += ("#" + i + ": " + FD.displayName + "\n");
            i++;
        }
        equippedFollowers.text = message;
    }

    private void PopulateUnEquippedFollowers()
    {
        string message = "UnEquipped Followers:\n";

        int i = 0;

        foreach(FollowerData FD in partyManager.unequippedFollowers)
        {
            message += ("#" + i + ": "+ FD.displayName + "\n");
            i++;
        }
        unEquippedFollowers.text = message;
    }

}
