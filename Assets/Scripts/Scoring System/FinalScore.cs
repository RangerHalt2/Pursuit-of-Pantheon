using System.Collections;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    [Header("Score Text Fields")]
    [SerializeField] private TextMeshProUGUI totalScore;
    [SerializeField] private TextMeshProUGUI enemyScore;
    [SerializeField] private TextMeshProUGUI recruitScore;
    [SerializeField] private TextMeshProUGUI trainingScore;
    [SerializeField] private TextMeshProUGUI promoteScore;

    private ScoreManager scoreManager;

    private void Start()
    {
        StartCoroutine(DelayedScore());
    }

    private IEnumerator DelayedScore()
    {
        yield return new WaitForSeconds(0.2f);

        scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        scoreManager.CalculateTotalScore();
        if (scoreManager != null)
        {
            totalScore.text = "Total Score: " + scoreManager.totalScore;
            enemyScore.text = "Enemy Kill Score: " + scoreManager.enemyKillScore;
            recruitScore.text = "Recruitement Score " + scoreManager.recruitScore;
            trainingScore.text = "Training Score " + scoreManager.statTrainingScore;
            promoteScore.text = "Promotion Score " + scoreManager.promotionScore;
        }
        else
            Debug.Log("Score Manager is null");
    }

}
