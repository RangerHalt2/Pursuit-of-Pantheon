using UnityEngine;

[CreateAssetMenu(menuName = "Follower/FollowerClass")]
public class FollowerClass : ScriptableObject
{
    public string className;
    public int classID;
    public float baseMaxHP = 1f;
    public float baseStrength = 1f;
    public float baseMagic = 1f;
    public float baseDefense = 1f;
    public float baseResistance = 1f;
    public float baseSpeed = 1f;
}