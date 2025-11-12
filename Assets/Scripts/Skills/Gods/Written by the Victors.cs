using UnityEngine;

public class WrittenbytheVictors : DivineSkillBase
{
    public override string skillName => "Written by the Victors";
    [SerializeField] float rewindSeconds = 3f;
    [SerializeField] float extraHealing = 0f;

    private void Start()
    {
        //Create battle recorder
    }

    public override void UseSkill()
    {
        if (onCooldown || !CheckAP()) return;

        // if battlerecorder is not null
        //rewindseconds
        //if extrahealing is more than 0
        //find followers
        //foreach var follower in followers
        //extraHealing

        //Start Cooldown coroutine
    }
}
