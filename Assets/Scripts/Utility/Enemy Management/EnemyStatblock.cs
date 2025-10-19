using System;
using System.Linq;
using UnityEngine;

public class EnemyStatblock : MonoBehaviour, ICombatant
{
    [Tooltip("The follower's internal ID")]
    public string enemyID;
    [Tooltip("the enemy's name as is displayed in game.")]
    public string displayName;
    [Tooltip("The teamID of the enemy")]
    public int teamID = 1;

    [Tooltip("The spawn point location the enemy will use.")]
    public int position;

    [Header("Base Stat Info")]
    [Tooltip("The maximum HP of the enemy.")]
    public float maxHP = 1f;
    [Tooltip("The current HP of the enemy.")]
    public float currentHP = 1f;

    [Tooltip("The vigor stat of the enemy. Affects maximum HP.")]
    public float vigor = 1f;

    [Tooltip("The physical attack stat of the enemy.")]
    public float power = 1f;
    [Tooltip("The magical attack stat of the enemy.")]
    public float magick = 1f;

    [Tooltip("The physical defense stat of the enemy.")]
    public float resilience = 1f;

    [Tooltip("The magical defense stat of the enemy.")]
    public float faith = 1f;

    [Tooltip("The speed stat of the enemy.")]
    public float agility = 1f;

    [Header("Skill Info")]
    [Tooltip("The list of skill components attached to this enemy.")]
    [SerializeField] private MonoBehaviour[] skillComponents;
    // Filtered list of objects in skill components.
    private ISkill[] skills;

    public float actionProgress;

    #region Interface Management
    public float Agility => agility;
    public float ActionProgress { get => actionProgress; set => actionProgress = value; }
    public int TeamID => teamID;
    #endregion

    private void Awake()
    {
        // Filter all entries in the skillComponents array to only those that use the skill interface, then assign them to the skills array
        skills = System.Array.FindAll(skillComponents, comp => comp is ISkill).Cast<ISkill>().ToArray();
    }

    private void Start()
    {
        // Used for testing that skills are properly loaded and recognized, leave commented out
        /*
        for (int i = 0; i <skills.Length; i++)
        {
            ISkill skill = skills[i];
            Debug.Log($"Skill {i + 1}: {skill.skillName} loaded.");
        }
        */
    }

    public void PerformAction()
    {
        GetComponent<EnemyAI>().TakeTurn();
        actionProgress = 0;
    }

    public GameObject GetGameObject() => gameObject;

    public ISkill[] GetSkills() => skills;
}
