using UnityEngine;

public class TurnButtonProxy : MonoBehaviour
{
    public void SpendTurn()
    {
        // If there is an instance of the turn manager
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.SpendTurn();
        }
        else
        {
            Debug.LogWarning("TurnButtonProxy: TurnManager instance not found!");
        }
    }

    public void LoadEvent(string eventName)
    {
        // If there is an instance of the turn manager
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.LoadEvent(eventName);
        }
        else
        {
            Debug.LogWarning("TurnButtonProxy: TurnManager instance not found!");
        }
    }
}
