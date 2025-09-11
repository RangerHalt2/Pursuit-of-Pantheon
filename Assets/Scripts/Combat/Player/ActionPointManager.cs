// Created By: Ryan Lupoli
// This script handles the player's Action Points (AP) which are used to activate divine abilities
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

public class ActionPointManager : MonoBehaviour
{
    #region Variables

    [Header("Action Point Settings")]
    [Tooltip("The maximum amount of Action Points the player can have at any given time. Value must be greater than 0.")]
    [SerializeField] private int maxAP;

    [Tooltip("The current amount of Action Points the player has. Value must be greater than 0.")]
    [SerializeField] private int currentAP;

    [Header("AP Recovery Settings")]
    [Tooltip("The amount of time (in Seconds) it takes for the player to recover Action Points. Value must be greater than 0.")]
    [SerializeField] private float recoveryTime;

    [Tooltip("The amount of Action Points the player recovers when the recoveryTime has passed. Value must be greater than 0.")]
    [SerializeField] private int recoveryAmount;

    [Header("AP Display Settings")]
    [Tooltip("Reference to TMPro Object used to track current AP. Optional.")]
    [SerializeField] private TextMeshProUGUI apDisplayText;

    [Header("AP SFX Settings")]
    [Tooltip("Reference to prefab for an effect which triggers when AP is gained. Optional.")]
    public GameObject apGainEffect;
    [Tooltip("Reference to prefab for an effect which triggers when AP is reaches the Max. Optional.")]
    public GameObject apMaxEffect;
    [Tooltip("Reference to prefab for an effect which triggers when AP is spent. Optional.")]
    public GameObject apSpendEffect;
    
    [Header("Debug Settings")]
    [Tooltip("DEBUG SETTING: Grant the player unlimited AP. AP will be automatically set to the maximum and will never be spent.")]
    [SerializeField] private bool unlimitedAP;

    #endregion

    void Start()
    {
        // Check if the current AP is set higher than the maximum, or if the unlimitedAP setting is enabled
        if (currentAP > maxAP || unlimitedAP)
        {
            // Sets the player's current AP to the maximum amount
            currentAP = maxAP;
        }
        // Set AP Display to reflect initial AP
        updateAPDisplay();

        // Start AP Recovery Coroutine
        StartCoroutine(APRecovery());
    }

    void Update()
    {
        
    }

    // AP Recovery Coroutine
    private IEnumerator APRecovery()
    {
        // Wait for a period of time defined by the recovery time variable
        yield return new WaitForSeconds(recoveryTime);

        // Check to see if player is not already at their maximum AP
        if (currentAP < maxAP)
        {
            // Recover the amount of AP defined by recovery amount
            increaseAP(recoveryAmount);
        }

        // Start the APRecovery again to ensure constant uptime
        StartCoroutine(APRecovery());
    }

    // Adds a set amount of AP to the player's maximum.
    public void increaseAP(int earnerdAP)
    {
        // If the amount of AP added would push the player's current AP above the maximum, set the AP to the maximum
        if (currentAP + earnerdAP > maxAP)
        {
            currentAP = maxAP;
            Debug.Log("Player cannot gain " + earnerdAP + " AP without going over the maximum. AP has been set to max.");

            // If an AP Gain Effect is assigned
            if (apMaxEffect != null)
            {
                // Play the Sound Effect for AP reaching the maximum
                Instantiate(apMaxEffect, transform.position, transform.rotation, null);
            }

            // Update the AP Display
            updateAPDisplay();
        }
        // Other wise add the earned AP to the current AP
        else
        {
            currentAP += earnerdAP;
            Debug.Log("AP increased by " + earnerdAP + ". Current AP: " + currentAP + ".");

            // AP is at the maximum
            if (currentAP == maxAP)
            {
                // If an AP Max Effect is assigned
                if (apMaxEffect != null)
                {
                    // Play the Sound Effect for AP reaching the maximum
                    Instantiate(apMaxEffect, transform.position, transform.rotation, null);
                }
            }
            // AP is below the maximum
            else
            {
                // If an AP Gain Effect is assigned
                if (apGainEffect != null)
                {
                    // Play the Sound Effect for gaining AP
                    Instantiate(apGainEffect, transform.position, transform.rotation, null);
                }
            }
            
            // Update the AP Display
            updateAPDisplay();
        }
    }

    // Spends a set amount of the player's AP, decreasing the current amount
    public void decreaseAP(int spentAP)
    {
        // If the unlimitedAP debug setting is not enabled proceed normally
        if (!unlimitedAP)
        {
            // If the currentAP would be set to below 0, do not reduce player's AP
            if (currentAP - spentAP < 0)
            {
                // This is included as a failsafe to prevent AP accidentally being reduced below 0. Any code spending AP should check if the player can afford to spend the AP before running this method. 
                Debug.Log("ERROR: Player cannot spend " + spentAP + " AP without going to below 0 AP.");
                return;
            }
            // Otherwise subtract the spentAP from the player's current total
            else
            {
                currentAP -= spentAP;
                Debug.Log("AP decreased by " + spentAP + ". CurrentAP: " + currentAP + ".");

                // If an AP Spend Effect is assigned
                if (apSpendEffect != null)
                {
                    // Play the Sound Effect for spending AP
                    Instantiate(apSpendEffect, transform.position, transform.rotation, null);
                }

                // Update the AP Display
                updateAPDisplay();
            }
        }
        // If the unlimitedAP debug setting is enabled, do nothing
        else
        {
            Debug.Log("Unlimited AP Setting is enabled. No AP deducted.");
            return;
        }
    }

    // Updates the display for the player's current amount of AP
    // NOTE: Current implementation uses TMPro and is intended purely for the developmental purposes. A porper display using a sprite array can be added later. 
    public void updateAPDisplay()
    {
        // Used if there is a TMPro Display assinged
        if (apDisplayText != null)
        {
            apDisplayText.text = string.Format("AP: " + currentAP + " / " + maxAP);
        }

        // At a later point in time the artists should create a finalized AP display. Code managing it will be added once the assets are ready
    }

}
