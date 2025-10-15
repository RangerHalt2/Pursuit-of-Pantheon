// Created By: Ryan Lupoli
// This script is in charge of managing the turns within the game and loading the various different events
using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    #region Variables
    [Header("Turn Settings")]
    [Tooltip("The number of turns in each \"Act\" of the story. Each entry in the array functions as a different act, and the value assigned determines the amount of turns. ")]
    public int[] turns;
    // The current turn the player is on within the current act
    public int currentTurn = 1;
    // The current act the player is in
    public int currentAct = 1;

    [Header("UI References")]
    public TextMeshProUGUI actText;
    public TextMeshProUGUI turnText;

    [Header("Event Settings")]
    public int test;

    SceneController sceneController;
    #endregion

    void Awake()
    {
        // Ensure there is only ever one instance of the turn manager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // If no instance exists, create an instance
        Instance = this;

        // Locate sceneController
        sceneController = GetComponentInParent<SceneController>();
        if (sceneController == null)
        {
            //Debug.LogError("TurnManager: SceneController not found on parent Bootstrapper!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Turn Management
    // Increase the value of currentTurn, calling MoveToNextAct if necessary
    public void SpendTurn()
    {
        // If the currentAct is less than or equal to the amount of acts in the game...
        if (currentAct <= turns.Length)
        {
            // If currentTurn is less than or equal to the amount of turns in the act...
            if (currentTurn <= turns[currentAct - 1])
            {
                // Increment the current turn
                currentTurn++;
            }
            // If the currentTurn is greater than the amount of turns in the current act...
            if (currentTurn > turns[currentAct - 1])
            {
                // Move Player to the next act
                MoveToNextAct();
            }
            // If the currentAct is less than or equal to the amount of acts in the game...
            if (currentAct <= turns.Length)
            {
                Debug.Log("Turn Manager: Turn passed. Currently on Turn " + currentTurn + " of " + turns[currentAct - 1] + " in Act " + currentAct);
                // Update the UI
                UpdateUI();
            }
        }
        else
        {
            Debug.LogWarning("Turn Manager: The current Act (" + currentAct + ") is greater than the amount of acts in the campaign (" + turns.Length + ")!");
        }
    }

    // Increase the currentAct, assuming the player is not currently in the final act
    public void MoveToNextAct()
    {
        // If the method is called and the current Act is greater than the amount of acts in the game
        if (currentAct > turns.Length)
        {
            // Do Nothing
            Debug.LogWarning("Turn Manager: The currentAct is greater than the amount of acts in the game! The game should be over!");
            return;
        }

        // Increment current Act and rest the current turn
        currentAct++;
        currentTurn = 1;

        // If the current act is greater than the amount of acts in the game
        if (currentAct > turns.Length)
        {
            Debug.Log("Turn Manager: The player has completed the final act!");

            // Call code for ending the game here

        }
        else
        {
            // Extra code can be placed here based on what we want to happen at the end of each act
            Debug.Log("Turn Manager: Act " + (currentAct - 1) + " of " + (turns.Length) + " has ended. Moving to act " + currentAct + "!");
            // Update the UI
            UpdateUI();
        }
    }
    #endregion

    #region Event Management
    // Load the a specific event by their name
    public void LoadEvent(string eventName)
    {
        // Each case represents a new event
        switch (eventName)
        {
            // Promotion event
            case "Promote":
                // Spend a turn
                SpendTurn();
                
                // Load the promotion event scene
                sceneController.GoToScene("PromotionEventScene");
                Debug.Log("Turn Manager: Player has spent a turn to promote a unit.");
                break;
            // Default, Called if eventName is invalid
            default:
                Debug.LogError("Turn Manager: LoadEvent was asked to load " + eventName + ", but that event does not exist!");
                break;
        }
    }
    #endregion

    #region UI Management
    // Assigns the actText and turnText UI elements
    public void AssignUI(TextMeshProUGUI actTextRef, TextMeshProUGUI turnTextRef)
    {
        actText = actTextRef;
        turnText = turnTextRef;

        UpdateUI();
    }

    // Updates the UI to reflect the current Act and Turn
    private void UpdateUI()
    {
        // Displays the current act
        if (actText != null)
        {
            actText.text = ("Act: " + currentAct + "");
        }
        // Displays the number of turns remaining
        if (turnText != null)
        {
            if ((turns[currentAct - 1] - currentTurn) != 0)
            {
                turnText.text = ((turns[currentAct - 1] - currentTurn) + " turns remaining!");
            }
            else
            {
                turnText.text = ("Final Turn!");
            }
        }
    }
    #endregion
}
