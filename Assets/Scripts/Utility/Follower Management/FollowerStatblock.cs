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

    [Tooltip("The vigor stat of the follower. Affects maximum HP.")]
    public float vigor = 1f;

    [Tooltip("The physical attack stat of the follower.")]
    public float power = 1f;
    [Tooltip("The magical attack stat of the follower.")]
    public float magick = 1f;

    [Tooltip("The physical defense stat of the follower.")]
    public float resilience = 1f;

    [Tooltip("The speed stat of the follower.")]
    public float agility = 1f;

    public float faith = 1f;

    [Header("Bonus Stat Info")]
    [Tooltip("Additonal maximum HP the follower gained from bonuses.")]
    public float bonusMaxHP = 0f;

    [Tooltip("Additonal physical attack the follower gained from bonuses.")]
    public float bonusPower = 0f;
    [Tooltip("Additonal magical attack the follower gained from bonuses.")]
    public float bonusMagick = 0f;

    [Tooltip("Additonal physical defense the follower gained from bonuses.")]
    public float bonusResilience = 0f;


    [Tooltip("Additonal speed the follower gained from bonuses.")]
    public float bonusAgility = 0f;

    public float bonusVigor = 0f;

    public float bonusFaith = 0f;
}
