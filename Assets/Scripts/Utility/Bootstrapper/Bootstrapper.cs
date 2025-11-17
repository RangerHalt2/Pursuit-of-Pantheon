// Created By: Ryan Lupoli
// This script is designed to manage the bootstrapper ensuring one instance of it is constantly active
using System;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    // Instance of the Bootstrap Script
    public static Bootstrapper Instance { get; private set; } = null;

    [HideInInspector]
    public Gods selectedGod;

    [Header("Bootstrapped Data")]
    [Tooltip("Player's Active Party")]
    public PartyData partyData = new PartyData();

    //LB: Region added by Logan, intermediate between combat outcome and the combat manager.
    #region Combat Manager Dialogue Resources and Functions
    [HideInInspector]
    public bool combatLeadsToDialogue = false;
    [HideInInspector]
    public TextAsset dialogueAfterBattle;

    public void HandleCombatEnd()
    {
        if (combatLeadsToDialogue)
        {
            if(dialogueAfterBattle == null)
            {
                Debug.LogWarning("No provided dialogue with this, some form of error occured. Preventing execution.");
                return;
            }

            SceneController controller = GetComponent<SceneController>();
            if (controller != null)
            {
                controller.GoToScene("QuillaOpeningScene");
            }
        }
    }
    
    public void PurgeDialogues()
    {
        dialogueAfterBattle = null;
        combatLeadsToDialogue = false;
    }

    #endregion

    void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If another instance exists, destroy it
            Debug.LogError("Bootstrapper: Found another instance of BootstrappedData on " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        // Prevent Data from being unloaded
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    public void SelectGod(int selectionNum)
    {
        switch (selectionNum)
        {
            case 0:
                selectedGod = Gods.Anubis;
                break;
            case 1:
                selectedGod = Gods.Claranox;
                break;
            case 2:
                selectedGod = Gods.Quilla;
                break;
            default:
                Debug.LogError("No known god by this number");
                break;
        }
    }

    public enum Gods
    {
        Anubis,
        Claranox,
        Quilla
    }
}

