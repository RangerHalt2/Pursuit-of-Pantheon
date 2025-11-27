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
        scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            totalScore.text = "Total Score: "+scoreManager.totalScore;
            enemyScore.text = "Enemy Kill Score: " + scoreManager.enemyKillScore;
            recruitScore.text = "Recruitement Score " + scoreManager.recruitScore;
            trainingScore.text = "Training Score " + scoreManager.statTrainingScore;
            promoteScore.text = "Promotion Score " + scoreManager.promotionScore;
        }
    }

}
