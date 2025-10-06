// Created by Ryan Lupoli
// The following is a basic statblock for a follower designed to store data in the bootstrapper. Will be replaced by a better version which William creates.
using UnityEngine;

[System.Serializable]
public class FollowerStatblock : MonoBehaviour
{
    [Tooltip("The follower's internal ID")]
    public string followerID;
    [Tooltip("the follower's name as is displayed in game.")]
    public string displayName;

    [Tooltip("The spawn point location the follower will use.")]
    public int position;

    [Header("Class Info")]
    [Tooltip("The classID of the follower")]
    public int classID = 0;

    [Header("Base Stat Info")]
    [Tooltip("The maximum HP of the follower.")]
    public float maxHP = 1f;
    [Tooltip("The current HP of the follower.")]
    public float currentHP = 1f;

    [Tooltip("The physical attack stat of the follower.")]
    public float strength = 1f;
    [Tooltip("The magical attack stat of the follower.")]
    public float magic = 1f;

    [Tooltip("The physical defense stat of the follower.")]
    public float defense = 1f;
    [Tooltip("The magical defense stat of the follower.")]
    public float resistance = 1f;

    [Tooltip("The speed stat of the follower.")]
    public float speed = 1f;

    [Header("Bonus Stat Info")]
    [Tooltip("Additonal maximum HP the follower gained from bonuses.")]
    public float bonusMaxHP = 0f;

    [Tooltip("Additonal physical attack the follower gained from bonuses.")]
    public float bonusStrength = 0f;
    [Tooltip("Additonal magical attack the follower gained from bonuses.")]
    public float bonusMagic = 0f;

    [Tooltip("Additonal physical defense the follower gained from bonuses.")]
    public float bonusDefense = 0f;
    [Tooltip("Additonal magical defense the follower gained from bonuses.")]
    public float bonusResistance = 0f;

    [Tooltip("Additonal speed the follower gained from bonuses.")]
    public float bonusSpeed = 0f;
}
