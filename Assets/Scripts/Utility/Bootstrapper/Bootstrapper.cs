// Created By: Ryan Lupoli
// This script is designed to manage the bootstrapper ensuring one instance of it is constantly active
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    // Instance of the Bootstrap Script
    public static Bootstrapper Instance { get; private set; } = null;

    [Header("Bootstrapped Data")]
    [Tooltip("Player's Active Party")]
    public PartyData partyData = new PartyData();

    void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If another instance exists, destroy it
            Debug.LogError("Found another instance of BootstrappedData on " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        // Prevent Data from being unloaded
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
}

