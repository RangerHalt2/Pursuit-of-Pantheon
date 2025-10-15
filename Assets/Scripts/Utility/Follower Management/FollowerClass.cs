using UnityEngine;

[CreateAssetMenu(menuName = "Follower/ClassStats")]
public class FollowerClass : ScriptableObject
{
    public int classID;
    public float baseMaxHP = 1f;
    public float basePower = 1f;
    public float baseMagick = 1f;
    public float baseResilience = 1f;
    public float baseFaith = 1f;
    public float baseAgility = 1f;
    public bool isStartingClass = false;
    public string unityClassName; // e.g., "Rogue"
}