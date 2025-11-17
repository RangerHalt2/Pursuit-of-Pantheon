using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingBuilding : MonoBehaviour
{
    [Header("How much Training is worth")]
    [SerializeField] private float trainingIncrement;
    [SerializeField] private float agilityIncrement;

    [Header("Core Training Follower UI's Needed:")]
    [SerializeField] private Canvas TrainingFollowers;
    [SerializeField] private Canvas TrainingFollowersChildren;
    [SerializeField] private GameObject followerButtonPrefab;

    [Header("Selected Followers Canvas UI Elements")]
    [SerializeField] private TextMeshProUGUI statsBlockText;
    [SerializeField] private TextMeshProUGUI selectedName;
    [SerializeField] private Canvas selectedFollowerCanvas;

    private TurnManager turnManager;

    private void Start()
    {
        turnManager = GameObject.FindAnyObjectByType<TurnManager>();
    }

    private FollowerData selectedFollower;

    public void HideFollowerCanvas()
    {
        if (TrainingFollowers == null)
        {
            Debug.LogWarning("The Designated TrainingFollowers UI is null, no code ran.");
            return;
        }

        TrainingFollowers.gameObject.SetActive(false);
        selectedFollowerCanvas.gameObject.SetActive(false);
    }

    public void ShowAllFollowersUI()
    {
        if (TrainingFollowers == null)
        {
            Debug.LogWarning("The Designated TrainingFollowers UI is null, no code ran.");
            return;
        }

        TrainingFollowers.gameObject.SetActive(true);
        //Clear the old ones
        foreach(Transform children in TrainingFollowersChildren.transform)
        {
            Destroy(children.gameObject);
        }

        FollowerData[] allFollowers = GetAllFollowersFromManager();

        foreach (FollowerData follower in allFollowers)
        {
            GameObject followerButton = Instantiate(followerButtonPrefab, TrainingFollowersChildren.transform);
            followerButton.GetComponent<FollowerPrefabController>().SetFollowerName(follower.displayName);
            Button btn = followerButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                    {
                        selectedFollower = follower;
                        selectedName.text = selectedFollower.displayName;
                        statsBlockText.text = ("Vigor:      " + selectedFollower.vigor + "\n"
                                              +"Power:      " + selectedFollower.power + "\n"
                                              +"Magick:     " + selectedFollower.magick + "\n"
                                              +"Resilience: " + selectedFollower.resilience + "\n"
                                              +"Faith:      " + selectedFollower.faith + "\n"
                                              +"Agility:    " + selectedFollower.agility + "\n"
                        );
                        selectedFollowerCanvas.gameObject.SetActive(true);
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
        foreach(FollowerData follower in gameManager.unequippedFollowers)
        {
            ret[index++] = follower;
        }
        return ret;
    }

    public void TrainStat(string stat)
    {
        if(selectedFollower == null)
        {
            Debug.LogWarning("No Selected Follower at this time, no code ran.");
            return;
        }
        switch (stat)
        { //LB: Can an ENUM work here? Probably.
            case "vigor":
                selectedFollower.vigor += trainingIncrement;
                Debug.Log("Vigor Increased");
                break;
            case "power":
                selectedFollower.power += trainingIncrement;
                Debug.Log("Power Increased");
                break;
            case "magick":
                selectedFollower.magick += trainingIncrement;
                Debug.Log("Magick Increased");
                break;
            case "resilience":
                selectedFollower.resilience += trainingIncrement;
                Debug.Log("Resilience Increased");
                break;
            case "faith":
                selectedFollower.faith += trainingIncrement;
                Debug.Log("Faith Increased");
                break;
            case "agility":
                selectedFollower.agility += agilityIncrement;
                Debug.Log("Agility Increased");
                break;
            default:
                Debug.LogWarning("Stat string passed was not a proper stat. No effect has occured");
                break;
        }//End of Switch

        HideFollowerCanvas();
        turnManager.SpendTurn();

        return;
    }//end of TrainStat

}
