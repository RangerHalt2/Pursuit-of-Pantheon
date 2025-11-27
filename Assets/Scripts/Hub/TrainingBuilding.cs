using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class TrainingBuilding : MonoBehaviour
{
    [Header("How much Training is worth")]
    [SerializeField] private float trainingIncrement;
    [SerializeField] private float agilityIncrement;

    private TurnManager turnManager;
    private ShowFollowersInHub hubManager;

    private FollowerData selectedFollower;

    private void Start()
    {
        turnManager = GameObject.FindAnyObjectByType<TurnManager>();
        hubManager = GameObject.FindAnyObjectByType<ShowFollowersInHub>();
    }


    public void TrainStat(string stat)
    {
        selectedFollower = hubManager.selectedFollower;
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

        hubManager.HideFollowerCanvas();
        ScoreManager.Instance.statTrainingScore++;
        turnManager.SpendTurn();

        return;
    }//end of TrainStat

}
