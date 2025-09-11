using UnityEngine;

public class BasicFollowerAI : MonoBehaviour
{
    #region Variables
    [Header("Action Settings")]
    [Tooltip("The amount of time (in seconds) this follower takes to perform an action.")]
    [SerializeField] private float actionInterval;

    private float elapsedTime;

    // Reference to the Follower Stats Script
    private FollowerStats followerStats;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Check if Follower Stats have been assigned
        followerStats = GetComponent<FollowerStats>();

        if (followerStats != null)
        {
            
        }
        else
        {
            Debug.LogWarning("FollowerStats Script not found on this GameObject. Follower AI will not function.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= actionInterval)
        {
            elapsedTime = 0;
            Attack();
        }
    }

    // The follower performs a basic attack against an enemy.
    void Attack()
    {
        Debug.Log("Attack Method Called.");

        // Create an array of potential targets
        GameObject[] potentialTargets;

        // Search for objects tagged as Enemy
        potentialTargets = GameObject.FindGameObjectsWithTag("Enemy");

        // Pick a random target from the list
        int index = Random.Range(0, potentialTargets.Length);

        // Check if target is in range of the array
        if (index >= 0 & index < potentialTargets.Length)
        {
            GameObject target = potentialTargets[index];

            // Check if the target is within the bounds of the array
            if (target != null)
            {
                // Check if the target has a health script
                Health targetHealth = target.GetComponent<Health>();
                if (targetHealth != null)
                {
                    Debug.Log(name + " attacked " + target.name + ".");
                    targetHealth.TakeDamage(followerStats.Attack);
                }
                else
                {
                    Debug.LogWarning(target.name + " does not have a Health Script.");
                }
            }
            else
            {
                Debug.LogWarning(this.name + " tried to hit a target at an index which is out of range.");
            }
        }
        else
        {
            Debug.LogWarning("Index out of range: " + index);
        }
    }
}
