using UnityEngine;

[System.Serializable]
public class PlayerProfile : MonoBehaviour
{
    [Header("Player Data")]
    public int level = 0;

    public void UpdateLevel(int followerCount)
    {
        level = Mathf.Max(1, followerCount);
        Debug.Log($"[PlayerProfile] Player level is now {level}");
    }
}
