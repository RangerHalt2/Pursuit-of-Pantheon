// Created By: Ryan Lupoli
// This is a script meant to track a game object's health
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    #region Variables
    [Header("Team Settings")]
    [Tooltip("Determines the \"Team\" the object is on. Objects on the same team cannot damage each other.")]
    public int teamID;

    [Header("Health Settings")]
    [Tooltip("The maximum amount of health an object can. Value must be greater than 0.")]
    [SerializeField] public float maxHealth = 1f;
    [Tooltip("The amount of health the object currently has. If the current health is 0, the object is dead.")]
    [SerializeField] public float currentHealth = 1f;

    [Header("Display Settings")]
    [Tooltip("Reference to healthbar prefab. Optional.")]
    [SerializeField] private HealthBar healthBar;
    [Tooltip("Reference to TMPro Object used to track current AP. Optional.")]
    [SerializeField] private TextMeshProUGUI healthDisplayText;

    [Header("Effect Settings")]
    [Tooltip("Reference to prefab for an effect which triggers when the object recieves damage. Optional.")]
    public GameObject hitEffect;
    [Tooltip("Reference to prefab for an effect which triggers when the object is destroyed. Optional.")]
    public GameObject deathEffect;

    [Header("Statblock References")]
    [Tooltip("Reference to follower Statblock")]
    public FollowerStatblock followerStats;
    [Tooltip("Reference to enemy Statblock")]
    public EnemyStatblock enemyStats;

    private IEnumerator coroutine;

    private bool stasisActive = false;

    //Added by Alyssa for SetHealth()
    public UnityEvent<float> OnHealthChanged;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If object has been assigned a statblock, read the stats
        if (followerStats != null)
        {
            maxHealth = followerStats.maxHP;
            currentHealth = followerStats.currentHP;
            teamID = followerStats.teamID;
        }
        else if(enemyStats != null)
        {
            maxHealth = enemyStats.maxHP;
            currentHealth = enemyStats.currentHP;
            teamID = enemyStats.teamID;
        }
        
        // Automatically kill object if it has 0 or less health
        if (currentHealth <= 0)
        {
            //Debug.Log(gameObject.name + "'s Initial Health was equal to or less than 0. They have been automatically destroyed.");
            Die();
        }

        // If there is a healthbar assigned, set the max health
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        updateDisplay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Applies a certain amount of damage to an object
    public void TakeDamage(float damageAmount)
    {
        if (stasisActive) return;
        // Subtract the damage amount from the health of the object
        currentHealth -= damageAmount;
        //Debug.Log(gameObject.name + " took " + damageAmount + " damage. Current Health: " + currentHealth + "/" + maxHealth + ".");
        updateDisplay();

        // If the object has 0 or less current health
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // If a hit effect has been assigned
            if (hitEffect != null)
            {
                // Play the hit effect for the object
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
        }
    }

    // Applies a certain amount of healing to an object
    public void ReceiveHealing(float healingAmount)
    {
        if (stasisActive) return;
        // Add the healing amount to the object's current health
        currentHealth += healingAmount;
        //Debug.Log(gameObject.name + " received " + healingAmount + " healing. Current Health: " + currentHealth + "/" + maxHealth + ".");
        updateDisplay();

        // If the object's current health is now greater than the max...
        if (currentHealth > maxHealth)
        {
            // Set current health to max health
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        // If a death effect has been assigned
        if (deathEffect != null)
        {
            // Play the death effect for the object
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        // Remove the combatant from the list
        CombatManager.instance.RemoveCombatant(GetComponent<ICombatant>());
        
        if(teamID == 1)
        {
            ScoreManager.Instance.enemyKillScore += 15;
        }

        //Debug.Log(gameObject.name + " has died.");
        //Delay destruction by a frame
        StartCoroutine(DelayedDestroy());
    }
    
    private IEnumerator DelayedDestroy()
    {
        // Wait one frame
        yield return null;
        // Destroy the game object
        Destroy(gameObject);
    }

    public void updateDisplay()
    {
        // Used if there is a TMPro Display assinged
        if (healthDisplayText != null)
        {
            healthDisplayText.text = string.Format(currentHealth + " / " + maxHealth);
        }

        // If there is a healthBar assigned
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    public void ToggleStasis()
    {
        if (stasisActive)
        {
            stasisActive = false;
        }
        else
        {
            stasisActive = true;
        }
    }

    //Added by Alyssa for WrittenbytheVictors Quilla skill
    public void SetHealth(float newHealth)
    {
        float clamped = Mathf.Clamp(newHealth, 0f, maxHealth);

        if (Mathf.Approximately(clamped, currentHealth) == false)
        {
            currentHealth = clamped;
            OnHealthChanged?.Invoke(currentHealth);
        }

        if (currentHealth < 0f)
        {
            Die();
        }
    }
}
