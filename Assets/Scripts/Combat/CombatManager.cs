// Created By: Ryan Lupoli
// Meant to manage combat in game
using TMPro.EditorUtilities;
using Unity.VisualScripting;
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

    // Update is called once per frame
    void Update()
    {

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
            UIManager.instance.GoToPage(gameVictoryPageIndex);
            // Freeze Time
            Time.timeScale = 0;
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
}
