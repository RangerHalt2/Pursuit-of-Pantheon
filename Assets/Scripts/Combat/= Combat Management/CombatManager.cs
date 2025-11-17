// Created By: Ryan Lupoli
// Meant to manage combat in game
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    #region Variables
    // Global instance which other scripts can reference
    public static CombatManager instance = null;

    [Header("Game Progress Settings")]
    [Tooltip("The page index in the UI Manager to go to when winning the game")]
    public int gameVictoryPageIndex = 0;
    [Tooltip("The effect to be created when the player wins a battle")]
    public GameObject victoryEffect;

    [Tooltip("The page index in the UI Manager to go to when losing the game")]
    public int gameLossPageIndex = 0;
    [Tooltip("The effect to be created when the player loses a battle")]
    public GameObject loseEffect;

    [Header("Team Settings")]
    [Tooltip("The teamIDs for the player's followers.")]
    [SerializeField] private int[] alliedTeamIDs;

    [Tooltip("The teamIDs for the player's followers.")]
    [SerializeField] private int[] enemyTeamIDs;

    private SceneController sceneController;

    #region CombatantTracking
    [Header("Combatant Settings")]
    // List of all combatants in the current scene
    private List<ICombatant> combatants = new List<ICombatant>();
    [Tooltip("Time (in seconds) between ActionProgress ticks.")]
    [SerializeField] private float actionTickInterval = 1f;
    [Tooltip("How much ActionProgress is required to take a turn.")]
    [SerializeField] private float actionThreshold = 100f;

    private float actionTickTimer = 0f;

    private bool registerd = false;

    private Bootstrapper boot;

    #endregion


    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        if (instance == null)
        {
            // Activate global reference
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        // Set time scale to 1 (normal speed)
        Time.timeScale = 1;
    }

    private void Start()
    {
        StartCoroutine(RegisterCombatants());
        sceneController = GameObject.FindAnyObjectByType<SceneController>();
        boot = GameObject.FindAnyObjectByType<Bootstrapper>();
    }

    // Update is called once per frame
    void Update()
    {
        TickCombat();
    }

    // Registers all combatants in the current scene
    IEnumerator RegisterCombatants()
    {
        // Clear the combatants list
        combatants.Clear();
        // Add all followers and enemies to the combatants list
        yield return new WaitForSeconds(0.2f);
        FollowerSpawner spawner = GameObject.FindAnyObjectByType<FollowerSpawner>();
        spawner.SpawnEquippedFollowers();

        var followers = FindObjectsByType<FollowerStatblock>(FindObjectsSortMode.None);
        foreach (var follower in followers)
        {
            combatants.Add(follower);
        }
        


        var enemies = FindObjectsByType<EnemyStatblock>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            combatants.Add(enemy);
        }
        registerd = true;
    }

    private void TickCombat()
    {
        if (!registerd) return;
        // Update the Tick Timer
        actionTickTimer += Time.deltaTime;
        // If the actionTickInterval has passed
        if (actionTickTimer >= actionTickInterval)
        {
            // Reset the Tick Timer
            actionTickTimer = 0f;
            // For every combatnat...
            foreach (var combatant in new List<ICombatant>(combatants))
            {
                // Add each combatant's agility to their action progress
                combatant.ActionProgress += combatant.Agility;
                // If the action progress is greater than or equal to the threshold
                if (combatant.ActionProgress >= actionThreshold)
                {
                    // Allow the combatnat to perform an action
                    combatant.PerformAction();
                }
            }
        }
        // Constantly Check comabat state
        CheckCombatState();
    }

    // Check the state of the battle to see if it should end
    public void CheckCombatState()
    {
        // Declare variables
        bool followersAlive = false;
        bool enemiesAlive = false;

        // Check all objects with a health component
        Health[] healthComponents = GameObject.FindObjectsByType<Health>(FindObjectsSortMode.None);

        // Check to see if any objects with a health component are on the enemy's side
        foreach (Health health in healthComponents)
        {
            foreach (int teamID in enemyTeamIDs)
            {
                if (health.teamID == teamID)
                {
                    enemiesAlive = true;
                }
            }
        }

        // If no enemies are alive, the battle ends in victory.
        // This code is temporary and will be refined at a later date
        if (!enemiesAlive)
        {
            //Intercept the win condition for dialogue.
            if(boot != null && boot.combatLeadsToDialogue)
            {
                sceneController.GoToScene("QuillaOpeningScene");
                return;
            }


            //UIManager.instance.GoToPage(gameVictoryPageIndex);
            sceneController.GoToScene("HubWorld");
            // Freeze Time
            //Time.timeScale = 0;
            return;
        }

        // Check to see if any objects with a health component are on the player's side
        foreach (Health health in healthComponents)
        {
            foreach (int teamID in alliedTeamIDs)
            {
                if (health.teamID == teamID)
                {
                    followersAlive = true;
                }
            }
        }

        // If no followers are alive, the game ends in a loss
        // This code is temporary and will be refined at a later date
        if (!followersAlive)
        {
            UIManager.instance.GoToPage(gameLossPageIndex);
            // Freeze Time
            Time.timeScale = 0;
        }

    }

    public void RemoveCombatant(ICombatant combatant)
    {
        if (combatants.Contains(combatant))
        {
            combatants.Remove(combatant);
            //Debug.Log("Combat Manager: Removed combatant: " + combatant.GetGameObject().name);
        }
    }

    public List<ICombatant> GetCombatants()
    {
        return combatants;
    }
}
