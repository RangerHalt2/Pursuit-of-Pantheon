using UnityEngine;
using System;

namespace PursuitOfPantheon.Classes
{
    public class Rogue : BaseClass
    {
        public new static readonly Type[] promotionOptions = { typeof(Swashbuckler), typeof(ArcaneArcher) };
        #region Base Stats and ID
        private void Awake()
        {
            if (classData == null)
                classData = Resources.Load<FollowerClass>("ClassStats/RogueStats");

            ApplyStats();
        }
        #endregion

        private void ApplyStats()
        {
            var statblock = GetComponent<FollowerStatblock>();
            if (statblock != null && classData != null)
            {
                statblock.classID = classData.classID;
                statblock.maxHP = classData.baseMaxHP;
                statblock.currentHP = classData.baseMaxHP;
                statblock.vigor = classData.baseVigor;
                statblock.power = classData.basePower;
                statblock.magick = classData.baseMagick;
                statblock.resilience = classData.baseResilience;
                statblock.faith = classData.baseFaith;
                statblock.agility = classData.baseAgility;
            }
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
}
