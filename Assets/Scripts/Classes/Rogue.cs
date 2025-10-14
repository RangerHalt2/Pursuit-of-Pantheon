using UnityEngine;

public class Rogue : BaseClass
{
    #region Base Stats and ID
    private void Awake()
    {
        classID = 1;
        baseVigor = 7;
        basePower = 10;
        baseResilience = 8;
        baseMagick = 10;
        baseAgility = 65;

        promotionOptions = null;
    }
    #endregion


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
