using UnityEngine;

public class Thief : BaseClass
{
    #region Base Stats and ID
    private void Awake()
    {
        classID = 1;
        basePower = 10;
        baseResilience = 8;
        baseMagick = 10;
        baseFaith = 7;
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
