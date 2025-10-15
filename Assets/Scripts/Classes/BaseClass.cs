using UnityEngine;
using System;

public abstract class BaseClass : MonoBehaviour
{
    public FollowerClass classData;
    public int classID;
    public float basePower, baseResilience, baseMagick, baseFaith, baseAgility;
    public Type[] promotionOptions;

    public virtual void DoPromotion(int optionIndex)
    {
        if (promotionOptions == null || optionIndex < 0 || optionIndex >= promotionOptions.Length) return;
        gameObject.AddComponent(promotionOptions[optionIndex]);
        Destroy(this);
    }

    public abstract void SkillOne();
    public abstract void SkillTwo();
    public abstract void SkillThree();
}