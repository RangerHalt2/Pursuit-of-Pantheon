using UnityEngine;

public interface IClasses
{
    public int classID { get; set; }
    public float baseVigor { get; set; }
    public float basePower { get; set; }
    public float baseResilience { get; set; }
    public float baseFaith { get; set; }
    public float baseMagick { get; set; }
    public float baseAgility { get; set; }

    public string promotion { get; set; }
    public float promotionVigor { get; set; }
    public float promotionPower { get; set; }
    public float promotionResilience { get; set; }
    public float promotionFaith { get; set; }
    public float promotionMagick { get; set; }
    public float promotionAgility { get; set; }

    void SkillOne();
    void SkillTwo();
    void SkillThree();

    void DoPromotion();
}
