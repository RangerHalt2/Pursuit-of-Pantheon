using UnityEngine;

public class Bandit : MonoBehaviour, IClasses
{
    #region Base Stats and ID
    private int classID = 0;
    private float baseVigor = 5;
    private float basePower = 5;
    private float baseResilience = 8;
    private float baseMagick = 10;
    private float baseAgility = 50;

    private string promotion = "Thief";
    private float promotionVigor = 7;
    private float promotionPower = 10;
    private float promotionResilience = 8;
    private float promotionMagick = 10;
    private float promotionAgility = 65;
    
    #endregion

    private void DoPromotion()
    {
        
    }

    
    #region Class Skills
    void IClasses.SkillOne()
    {
        return;
    }

    void IClasses.SkillTwo() {
        return;
    }

    void IClasses.SkillThree()
    {
        return;
    }
    #endregion


}
