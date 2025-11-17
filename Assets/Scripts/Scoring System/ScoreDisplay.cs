// Created By: Ryan Lupoli
// This script manages displaying the score values from the Score Manager
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private ScoreManager scoreManager;

    [Header("Score Displays")]
    [SerializeField] private TextMeshProUGUI totalScoreDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            scoreManager = ScoreManager.Instance;
            Debug.Log("ScoreDisplay: Successfully located Score Manager.");
        }
        else
        {
            Debug.LogWarning("ScoreDisplay:Score Manager could not be found! Make sure an instance of it exists.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void UpdateDisplay()
    {
        if (totalScoreDisplay != null)
        {
            totalScoreDisplay.text = string.Format(scoreManager.totalScore + "");
        }
    }
}
