// Created By: Ryan Lupoli
// This script is used for storing the stats of a follower. It is intended to be referenced by other scripts
using UnityEngine;

public class FollowerStats : MonoBehaviour
{
    [Header("Stats")]
    [Tooltip("The attack stat of the follower. Used to determine how much damage their attacks deal.")]
    [SerializeField] public float Attack = 1;

    [Tooltip("The magic stat of the follower. Used to determine how much effective certain skills are.")]
    [SerializeField] public float Magic = 1;

    [Tooltip("The speed stat of the follower. Used to determine how quickly they can perform actions.")]
    [SerializeField] public float Speed = 1;
}
