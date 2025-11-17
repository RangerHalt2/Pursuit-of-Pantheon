using TMPro;
using UnityEngine;

public class TurnUIUpdater : MonoBehaviour
{
    TurnManager turnManager;

    [SerializeField] private TextMeshProUGUI actText;
    [SerializeField] private TextMeshProUGUI turnText;

    private void Start()
    {
        turnManager = GameObject.FindAnyObjectByType<TurnManager>();
        if(turnManager != null)
        {
            turnManager.actText = actText;
            turnManager.turnText = turnText;


            turnManager.UpdateUI();

        }
    }
}
