// Created By: Ryan Lupoli
// This script is in charge of managing the turns within the game and loading the various different events
using System;
using UnityEngine;
using TMPro;

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
    // How many turns must pass before the next random int can occur
    private int reCooldown = 1;


    [Header("UI References")]
    public TextMeshProUGUI actText;
    public TextMeshProUGUI turnText;

    SceneController sceneController;
    EventManager eventManager;

    private ScheduledEventList scheduledEventList;
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
            Debug.LogWarning("TurnManager: SceneController not found on parent Bootstrapper!");
        }

        eventManager = GetComponent<EventManager>();
        if (eventManager == null)
        {
            Debug.LogWarning("TurnManager: Cannot find EventManager!");
        }

        // Locate JSON File for scheduled events and convert events into a list
        TextAsset jsonFile = Resources.Load<TextAsset>("Events/ScheduledEvents");
        if (jsonFile != null)
        {
            scheduledEventList = JsonUtility.FromJson<ScheduledEventList>(jsonFile.text);
            Debug.Log("TurnManager: Scheduled events successfully loaded!");
        }
        else
        {
            Debug.LogWarning("TurnManager: Could not find ScheduledEvents.json in Resources!");
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
        // If the random event cooldown is greater than 0
        if(reCooldown > 0)
        {
            // Decrease it by one
            reCooldown--;
        }

        // If the currentAct is less than or equal to the amount of acts in the game...
        if (currentAct <= turns.Length)
        {
            // If currentTurn is less than or equal to the amount of turns in the act...
            if (currentTurn <= turns[currentAct - 1])
            {
                // Increment the current turn
                currentTurn++;

                bool wasScheduled = CheckForScheduledEvent();
                if (wasScheduled) return; //Don't execute any below code if the event was scheduled.

                // Roll for a random event and check if it is on cooldown
                if (reCooldown <= 0)
                {
                    if (eventManager.RollForRandomEvent())
                    {
                        sceneController.GoToScene("QuillaOpeningScene");
                        //Set the cooldown timer to the randomEventCooldown specified in the EventManager
                        reCooldown = eventManager.randomEventCooldown;
                    }
                }
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

        // Increment current Act and restet the current turn
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
    private bool CheckForScheduledEvent()
    {
        //returns if a scheduled event is being forcefully loaded.
        bool ret = false;

        // Ensure that the Scheduled Evetnts list was found
        if (scheduledEventList == null || scheduledEventList.scheduledEvents == null)
        {
            Debug.LogWarning("Turn Manager: Scheduled Events list was not found or not set up properly. Cannot check for/load any Scheduled events!");
            return ret;
        }

        // Parse Scheduled events to see if one is meant to occur on the current turn
        foreach (ScheduledEvent e in scheduledEventList.scheduledEvents)
        {
            // If an event is meant to happen on this turn in this act
            if (e.act == currentAct && e.turn == currentTurn)
            {
                Debug.Log("Turn Manager: Scheduled event " + e.eventName + " triggered on Act: " + currentAct + ", Turn: " + currentTurn);

                // Prevent random events from occuring this turn
                reCooldown = eventManager.randomEventCooldown;

                // Check if scheduled event is for dialogue
                if (!string.IsNullOrEmpty(e.dialogueFileName))
                {
                    Debug.Log("Turn Manager: Scheduled dialogue event " + e.eventName + " triggered with dialogue " + e.dialogueFileName);
                    StartDialogueEvent(e.dialogueFileName, e.loadDialogueScene);
                    ret = true;
                }
                else
                {
                    LoadEvent(e.eventName, true);
                }
                return ret;
            }
        }
        return ret;
    }

    // Load the a specific event by their name
    public void LoadEvent(string eventName, bool spendTurn)
    {
        // If the spendTurn is ture, the event consumes a turn
        // This is meant to allow Scheduled events to consume a turn, but for manual ones to not
        if (spendTurn)
        {
            SpendTurn();
        }

        // Each case represents a new event
        switch (eventName)
        {
            case "Test":
                Debug.Log("Turn Manager: Test Event Successfully triggered!");
                break;
            // Promotion event
            case "Dialogue":
                break;
            case "Promote":
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
    
    private void StartDialogueEvent(string dialogueFileName, bool loadDialogueScene)
    {
        if (loadDialogueScene)
        {
            Debug.Log("TurnManager: Saving dialogue " + dialogueFileName + " to PlayerPrefs key 'NextDialogueToLoad'");
            PlayerPrefs.SetString("NextDialogueToLoad", dialogueFileName);
            PlayerPrefs.Save();
            // Load the dialogue scene
            sceneController.GoToScene("QuillaOpeningScene");
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
    public void UpdateUI()
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
