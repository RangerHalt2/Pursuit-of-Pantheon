using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public static class ClassNames
{
    public static readonly Dictionary<int, string> IdToName = new Dictionary<int, string>()
    {
        { 0, "Rogue" },
        { 1, "Swashbuckler" },
        { 2, "Arcane Archer" },
        { 3, "Brawler" },
        { 4, "Monk" },
        { 5, "Pugilist" },
        { 6, "Barbarian" },
        { 7, "Olympian" },
        { 8, "Ogre" },
        { 9, "Knight" },
        { 10, "Dark Knight" },
        { 11, "Cavalier" },
        { 12, "Mage" },
        { 13, "Witch" },
        { 14, "Archivist" },
        { 15, "Priest" },
        { 16, "Bard" },
        { 17, "Paladin" }
    };

    public static string GetNameById(int id)
    {
        if (IdToName.TryGetValue(id, out var name))
            return name;
        return "Unknown";
    }
}

public class ShowFollowersInHub : MonoBehaviour
{

    [Header("Core Training Follower UI's Needed:")]
    [SerializeField] private Canvas TrainingFollowers;
    [SerializeField] private Canvas TrainingFollowersChildren;
    [SerializeField] private GameObject followerButtonPrefab;

    [Header("Selected Followers Canvas UI Elements")]
    [SerializeField] private TextMeshProUGUI statsBlockText;
    [SerializeField] private TextMeshProUGUI selectedName;
    [SerializeField] private TextMeshProUGUI selectedClass;
    [SerializeField] private Canvas selectedFollowerCanvas;

    [Header("Selected Promotion Canvas UI Elements")]
    [SerializeField] private Canvas promotionCanvas;

    [Header("Selected Party Canvas UI Elements")]
    [SerializeField] private Canvas selectedPartyCanvas;
    [SerializeField] private Canvas PartyCanvasChildren;

    [Header("Raycast Blockers")]
    [SerializeField] private Image mainHubBLCKER;

    public FollowerData selectedFollower { get; private set;}

    public void HideFollowerCanvas()
    {
        if (TrainingFollowers == null)
        {
            Debug.LogWarning("The Designated TrainingFollowers UI is null, no code ran.");
            return;
        }

        TrainingFollowers.gameObject.SetActive(false);
        HideSelectedFollowerCanvas();
        HidePartyCanvas();
    }

    public void HideSelectedFollowerCanvas()
    {
        if (selectedFollowerCanvas == null)
        {
            Debug.LogWarning("The Designated SelectedFollowers UI is null, no code ran.");
            return;
        }

        selectedFollowerCanvas.gameObject.SetActive(false);
    }

    public void HidePartyCanvas()
    {
        if(selectedPartyCanvas == null)
        {
            Debug.LogWarning("The Designated SelectedParty UI is null, no code ran.");
            return;
        }
        selectedPartyCanvas.gameObject.SetActive(false);
        if (mainHubBLCKER != null) mainHubBLCKER.gameObject.SetActive(false);
    }

    public void ShowPartyStuff()
    {
        if (selectedPartyCanvas == null || PartyCanvasChildren == null)
        {
            Debug.LogWarning("Selected Party Canvas is null or the PartyCanvas Child is null, no code executed");
            return;
        }
        selectedPartyCanvas.gameObject.SetActive(true);

        ShowAllEquippedFollowers();
        ShowAllUnEquippedFollowers();
        if(mainHubBLCKER != null) mainHubBLCKER.gameObject.SetActive(true);
    }

    public void ShowAllFollowersUI(string building)
    {
        if (TrainingFollowers == null)
        {
            Debug.LogWarning("The Designated TrainingFollowers UI is null, no code ran.");
            return;
        }

        TrainingFollowers.gameObject.SetActive(true);
        if (mainHubBLCKER != null) mainHubBLCKER.gameObject.SetActive(true);
        //Clear the old ones
        foreach (Transform children in TrainingFollowersChildren.transform)
        {
            Destroy(children.gameObject);
        }

        FollowerData[] allFollowers = GetAllFollowersFromManager();

        foreach (FollowerData follower in allFollowers)
        {
            GameObject followerButton = Instantiate(followerButtonPrefab, TrainingFollowersChildren.transform);
            // RL: changed to work with Follower Party Button
            followerButton.GetComponent<FollowerPrefabController>().SetFollower(follower);
            followerButton.GetComponentInChildren<FollowerPartyButton>().showButtons = false;
            followerButton.GetComponentInChildren<FollowerPartyButton>().HideButtons();

            Button btn = followerButton.GetComponentInChildren<FollowerPartyButton>().generalClick;
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    selectedFollower = follower;
                    selectedName.text = selectedFollower.displayName;
                    selectedClass.text = ("Class: " + ClassNames.GetNameById(selectedFollower.classID));
                    statsBlockText.text = ("Vigor:      " + selectedFollower.vigor + "\n"
                                          + "Power:      " + selectedFollower.power + "\n"
                                          + "Magick:     " + selectedFollower.magick + "\n"
                                          + "Resilience: " + selectedFollower.resilience + "\n"
                                          + "Faith:      " + selectedFollower.faith + "\n"
                                          + "Agility:    " + selectedFollower.agility + "\n"
                    );
                    switch (building)
                    {
                        case "training":
                            selectedFollowerCanvas.gameObject.SetActive(true);
                            break;
                        case "promotion":
                            promotionCanvas.gameObject.SetActive(true);
                            break;
                    }
                    
                }
                ); //End of Listener Function
            } //End of Null Check
        } //End of foreach loop
    } //End of ShowAllFollowersUI

    private FollowerData[] GetAllFollowersFromManager()
    {
        PartyManager gameManager = GameObject.FindAnyObjectByType<PartyManager>();
        int totalLength = gameManager.equippedFollowers.Count + gameManager.unequippedFollowers.Count;
        FollowerData[] ret = new FollowerData[totalLength];
        int index = 0;
        foreach (FollowerData follower in gameManager.equippedFollowers)
        {
            ret[index++] = follower;
        }
        foreach (FollowerData follower in gameManager.unequippedFollowers)
        {
            ret[index++] = follower;
        }
        return ret;
    }

    public void ShowAllEquippedFollowers()
    {
        if (TrainingFollowers == null)
        {
            Debug.LogWarning("The Designated TrainingFollowers UI is null, no code ran.");
            return;
        }

        TrainingFollowers.gameObject.SetActive(true);
        //Clear the old ones
        foreach (Transform children in PartyCanvasChildren.transform)
        {
            Destroy(children.gameObject);
        }

        FollowerData[] allFollowers = GetEquippedFollowers();

        foreach (FollowerData follower in allFollowers)
        {
            GameObject followerButton = Instantiate(followerButtonPrefab, PartyCanvasChildren.transform);
            // RL: changed to work with Follower Party Button
            followerButton.GetComponent<FollowerPrefabController>().SetFollower(follower);
            followerButton.GetComponentInChildren<FollowerPartyButton>().showButtons = true;
            followerButton.GetComponentInChildren<FollowerPartyButton>().ExternalUpdate();

            Button btn = followerButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    selectedFollower = follower;
                    selectedName.text = selectedFollower.displayName;
                    selectedClass.text = ("Class: " + ClassNames.GetNameById(selectedFollower.classID));
                    statsBlockText.text = ("Vigor:      " + selectedFollower.vigor + "\n"
                                          + "Power:      " + selectedFollower.power + "\n"
                                          + "Magick:     " + selectedFollower.magick + "\n"
                                          + "Resilience: " + selectedFollower.resilience + "\n"
                                          + "Faith:      " + selectedFollower.faith + "\n"
                                          + "Agility:    " + selectedFollower.agility + "\n"
                    );
                }
                ); //End of Listener Function
            } //End of Null Check
        } //End of foreach loop
    }


    private FollowerData[] GetEquippedFollowers()
    {
        PartyManager gameManager = GameObject.FindAnyObjectByType<PartyManager>();
        int totalLength = gameManager.equippedFollowers.Count;
        FollowerData[] ret = new FollowerData[totalLength];
        int index = 0;
        foreach (FollowerData follower in gameManager.equippedFollowers)
        {
            ret[index++] = follower;
        }
        return ret;
    }

    private FollowerData[] GetUnEquippedFollowers()
    {
        PartyManager gameManager = GameObject.FindAnyObjectByType<PartyManager>();
        int totalLength = gameManager.unequippedFollowers.Count;
        FollowerData[] ret = new FollowerData[totalLength];
        int index = 0;
        foreach (FollowerData follower in gameManager.unequippedFollowers)
        {
            ret[index++] = follower;
        }
        return ret;
    }

    public void ShowAllUnEquippedFollowers()
    {
        if (TrainingFollowers == null)
        {
            Debug.LogWarning("The Designated TrainingFollowers UI is null, no code ran.");
            return;
        }

        TrainingFollowers.gameObject.SetActive(true);
        //Clear the old ones
        foreach (Transform children in TrainingFollowersChildren.transform)
        {
            Destroy(children.gameObject);
        }

        FollowerData[] allFollowers = GetUnEquippedFollowers();

        foreach (FollowerData follower in allFollowers)
        {
            GameObject followerButton = Instantiate(followerButtonPrefab, TrainingFollowersChildren.transform);
            // RL: changed to work with Follower Party Button
            followerButton.GetComponent<FollowerPrefabController>().SetFollower(follower);
            followerButton.GetComponentInChildren<FollowerPartyButton>().showButtons = true;
            followerButton.GetComponentInChildren<FollowerPartyButton>().ExternalUpdate();

            Button btn = followerButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    selectedFollower = follower;
                    selectedName.text = selectedFollower.displayName;
                    selectedClass.text = ("Class: " + ClassNames.GetNameById(selectedFollower.classID));
                    statsBlockText.text = ("Vigor:      " + selectedFollower.vigor + "\n"
                                          + "Power:      " + selectedFollower.power + "\n"
                                          + "Magick:     " + selectedFollower.magick + "\n"
                                          + "Resilience: " + selectedFollower.resilience + "\n"
                                          + "Faith:      " + selectedFollower.faith + "\n"
                                          + "Agility:    " + selectedFollower.agility + "\n"
                    );
                }
                ); //End of Listener Function
            } //End of Null Check
        } //End of foreach loop
    }
}
