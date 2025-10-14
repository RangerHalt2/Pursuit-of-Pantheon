using UnityEngine;
using System;

public class Bandit : BaseClass
{
    #region Base Stats, ID, and Promotion Options
    private void Awake()
    {
        classID = 0;
        baseVigor = 5;
        basePower = 5;
        baseResilience = 8;
        baseMagick = 10;
        baseAgility = 50;

        // Set promotion options to the types of the promoted classes
        promotionOptions = new Type[] { typeof(Thief), typeof(Rogue) };
    }
    #endregion

    public override void DoPromotion(int optionIndex)
    {
        if (optionIndex < 0 || optionIndex >= promotionOptions.Length) return;
        gameObject.AddComponent(promotionOptions[optionIndex]);
        this.enabled = false; // Using this.enabled instead of Destroy(this) to keep the component for reference and in the case of respecing the class later.
    }

    #region Class Skills
    public override void SkillOne()
    {
        
    }

    public override void SkillTwo()
    {
        
    }

    public override void SkillThree()
    {
        
    }
    #endregion
}
