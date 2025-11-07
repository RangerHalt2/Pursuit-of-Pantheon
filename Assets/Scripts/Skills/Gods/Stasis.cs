using System.Collections;
using UnityEngine;

public class Stasis : DivineSkillBase
{
    #region Variables
    public override string skillName => "Stasis";

    [Header("Skill Configuration")]
    [Tooltip("Determines how long (in seconds) the effects of Stasis will effect the selected follower")]
    [SerializeField] private float duration = 5f;

    #endregion

    public override void UseSkill()
    {
        if (!onCooldown)
        {
            if (CheckAP())
            {
                // Select a target
                GameObject selectedTarget = null;

                // Apply the stasis effect
                Health targetHealth = selectedTarget.GetComponent<Health>();
                if (targetHealth != null)
                {
                    StartCoroutine(ActivateStasis(targetHealth));
                }
            }
            else
            {
                Debug.Log("Not enough AP to use Stasis.");
            }
        }
        else
        {
            Debug.Log("Stasis is on cooldown.");
        }
    }

    private IEnumerator ActivateStasis(Health health)
    {
        health.ToggleStasis();
        yield return new WaitForSeconds(duration);
        health.ToggleStasis();
    }
}
