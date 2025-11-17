// Created By: Ryan Lupoli
// Manages the player's Score
// Score is obtained by doing actions the player is naturally going to do over the course of a run
using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    #region Variables
    // The total amount of points accrued by the player over the course of the run
    public int totalScore;

    // Score types
    // The total amount of points earned from killing enemies
    public int enemyKillScore;
    // The total amount of points earned from recruiting followers
    public int recruitScore;
    // The total amount of points earned from training followers
    public int statTrainingScore;
    // The total amount of points earned from promoting followers
    public int promotionScore;

    [Header("Points Per Score Category")]
    [Tooltip("The amount of points earned for every enemy defeated.")]
    [SerializeField] private int pointsPerKill = 25;
    [Tooltip("The amount of points earned for every follower recruited.")]
    [SerializeField] private int pointsPerRecruit = 15;
    [Tooltip("The amount of points earned every time a follower recieves a stat bonus.")]
    [SerializeField] private int pointsPerStatTrained = 1;
    [Tooltip("The amount of points earned time a follower was promoted.")]
    [SerializeField] private int pointsPerPromotion = 50;

    [Header("UI References")]
    private TextMeshProUGUI totalScoreText;
    private TextMeshProUGUI enemyKillScoreText;
    private TextMeshProUGUI recruitScoreText;
    private TextMeshProUGUI statTrainingScoreText;
    private TextMeshProUGUI promotionScoreText;
    #endregion

    private void Awake()
    {
        // Ensure there is only ever one instance of the score manager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // If no instance exists, create an instance
        Instance = this;
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find UI elements in the scene by name
        // Make sure that the TMPro assets in the scene have the EXACT same names
        totalScoreText = GameObject.Find("TotalScoreText")?.GetComponent<TextMeshProUGUI>();
        enemyKillScoreText = GameObject.Find("EnemyKillScoreText")?.GetComponent<TextMeshProUGUI>();
        recruitScoreText = GameObject.Find("RecruitScoreText")?.GetComponent<TextMeshProUGUI>();
        statTrainingScoreText = GameObject.Find("StatTrainingScoreText")?.GetComponent<TextMeshProUGUI>();
        promotionScoreText = GameObject.Find("PromotionScoreText")?.GetComponent<TextMeshProUGUI>();

        UpdateScoreUI();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Increments the player's score by a predetermined amount specified in the ScoreManager
    public void IncrementScore(String scoreType)
    {
        // Incremente the appropriate Score for the provided scoreType
        switch (scoreType)
        {
            case "Kill":
                enemyKillScore += pointsPerKill;
                break;
            case "Recruit":
                recruitScore += pointsPerRecruit;
                break;
            case "Training":
                statTrainingScore += pointsPerStatTrained;
                break;
            case "Promotion":
                promotionScore += pointsPerPromotion;
                break;
            // If score Type is invalid, increment no score
            default:
                Debug.LogWarning("Score Manager: Increment Score was called with invalid Score Type '" + scoreType + "'! Score was not properly incremented!");
                break;
        }
        CalculateTotalScore();
        UpdateScoreUI();
    }

    private void CalculateTotalScore()
    {
        // Calculate the sum of all score types
        totalScore = enemyKillScore + recruitScore + statTrainingScore + promotionScore;
    }

    // Resets the player's score back to 0
    // Inteneded to be called whenever a run is started
    private void ResetScore()
    {
        // Reset all score values to 0
        totalScore = 0;
        enemyKillScore = 0;
        recruitScore = 0;
        statTrainingScore = 0;
        promotionScore = 0;
    }

    // Updates the displayed scores on the UI    
    private void UpdateScoreUI()
    {
        if (totalScoreText != null) totalScoreText.text = totalScore.ToString();
        if (enemyKillScoreText != null) enemyKillScoreText.text = enemyKillScore.ToString();
        if (recruitScoreText != null) recruitScoreText.text = recruitScore.ToString();
        if (statTrainingScoreText != null) statTrainingScoreText.text = statTrainingScore.ToString();
        if (promotionScoreText != null) promotionScoreText.text = promotionScore.ToString();
    }
}
