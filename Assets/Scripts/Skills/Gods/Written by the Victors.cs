using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class WrittenbytheVictors : DivineSkillBase
{
    public override string skillName => "Written by the Victors";

    [Header("Rewind Settings")]
    [Tooltip("How far back in time to rewind (seconds")]
    [SerializeField] private float rewindDuration = 2f;

    [Tooltip("How many seconds of history we store at a time")]
    [SerializeField] private float recordDuration = 5f;

    [Tooltip("How often to record snapshots during gameplay (smaller is smoother but takes up more memory")]
    [SerializeField] private float snapshotInterval = 0.1f;

    private List<Snapshot> snapshots = new List<Snapshot>();
    private float recordTimer;
    private bool isRewinding = false;

    private Health healthComponent;

    private struct Snapshot
    {
        public float health;
    }

    private void Awake()
    {
        healthComponent = GetComponent<Health>();
        StartCoroutine(RecordState());
    }

    private IEnumerator RecordState()
    {
        while (true)
        {
            if (!isRewinding)
            {
                recordTimer += Time.deltaTime;

                if (recordTimer >= snapshotInterval)
                {
                    recordTimer = 0f;
                    snapshots.Insert(0, new Snapshot
                    {
                        health = healthComponent.currentHealth
                    });

                    int maxSnapshots = Mathf.CeilToInt(recordDuration /  snapshotInterval);

                    if (snapshots.Count > maxSnapshots)
                    {
                        snapshots.RemoveAt(snapshots.Count - 1);
                    }
                }
            }

            yield return null;
        }
    }

    public override void UseSkill()
    {
        //Check is we can use
        if (onCooldown || !CheckAP())
        {
            return;
        }

        StartCoroutine(ActivateRewind());
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator ActivateRewind()
    {
        isRewinding = true;

        float rewindEndTime = Time.time + rewindDuration;

        while (Time.time < rewindEndTime && snapshots.Count > 0)
        {
            var snapshot = snapshots[0];
            healthComponent.SetHealth(snapshot.health);
            yield return new WaitForSeconds(snapshotInterval);
        }

        isRewinding = false;
    }
}
