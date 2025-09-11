// Created By: Ryan Lupoli
// This script is used for storing the stats of a follower. It is intended to be referenced by other scripts
using UnityEngine;

public class FollowerStats : MonoBehaviour
{
    [Header("Stats")]
    [Tooltip("The attack stat of the follower. Used to determine how much damage their attacks deal.")]
    [SerializeField] public int Attack = 1;

    [Tooltip("The speed stat of the follower. Used to determine how quickly they can perform actions.")]
    [SerializeField] public int Speed = 1;
}
