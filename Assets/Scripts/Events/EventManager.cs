// Created By: Ryan Lupoli
// This script is in charge of managing events the player can encounter
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Variables
    [Header("Random Event Settings")]
    // The chance of a random event occuring
    private int randomEventChance;
    [Tooltip("The base chance of a random event starting.")]
    [Range(0, 100)][SerializeField] private int baseRandomEventChance;
    [Tooltip("How much the chance of a random event occuring increases every time one does not occur.")]
    [Range(0, 100)][SerializeField] private int randomEventChanceIncrease;
    [Tooltip("How many turns must pass after one random event before another one can occur.")]
    [SerializeField] public int randomEventCooldown = 3;

    TurnManager turnManager;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnManager = GetComponent<TurnManager>();
        if(turnManager == null)
        {
            Debug.LogWarning("EventManager: Cannot find TurnManager!");
        }

        // Set the random event chance to the base
        randomEventChance = baseRandomEventChance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Rolls for a chance to load a random event
    public bool RollForRandomEvent()
    {
        // Roll for a random event
        int roll = UnityEngine.Random.Range(1, 100);

        // If the roll is greater than the random event chance, no random event occurs
        if (roll > randomEventChance)
        {
            // Increase the random event chance by randomEventChanceIncrease
            randomEventChance += randomEventChanceIncrease;
            return false;
        }
        // Otherwise, a random event happens
        else
        {
            // Reset random event chance to its base amount
            randomEventChance = baseRandomEventChance;
            return true;
        }
    }

    // Plays a random event
    public void PlayRandomEvent()
    {
        Debug.Log("EventManager: Random Event!");
    }
}
