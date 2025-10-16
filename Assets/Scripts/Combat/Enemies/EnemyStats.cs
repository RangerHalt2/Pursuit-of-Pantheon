// Created By: Ryan Lupoli
// This script is used for storing the stats of an Enemy. It is intended to be referenced by other scripts
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    [Tooltip("The attack stat of the enemy. Used to determine how much damage their attacks deal.")]
    [SerializeField] public float Attack = 1;

    [Tooltip("The magic stat of the enemy. Used to determine how much effective certain skills are.")]
    [SerializeField] public float Magic = 1;
    [SerializeField] public float Defense = 1;

    [Tooltip("The speed stat of the enemy. Used to determine how quickly they can perform actions.")]
    [SerializeField] public float Speed = 1;
}

