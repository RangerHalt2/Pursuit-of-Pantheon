using UnityEngine;

public class Thief : MonoBehaviour, IClasses
{
    #region Base Stats and ID
    private int classID = 1;

    private IClasses promotion;
    #endregion


    void IClasses.SkillOne()
    {
        return;
    }

    void IClasses.SkillTwo()
    {
        return;
    }

    void IClasses.SkillThree()
    {
        return;
    }
}
