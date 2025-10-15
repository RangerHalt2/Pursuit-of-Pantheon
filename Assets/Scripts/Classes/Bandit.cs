using UnityEngine;
using System;

namespace PursuitOfPantheon.Classes
{
    public class Bandit : BaseClass
    {
        #region Base Stats, ID, and Promotion Options
        private void Awake()
        {
            classID = 0;
            basePower = 5;
            baseResilience = 8;
            baseMagick = 10;
            baseFaith = 6;
            baseAgility = 50;

            // Set promotion options to the types of the promoted classes
            promotionOptions = null;
        }
        #endregion

        #region Class Skills
        public override void SkillOne() { }
        public override void SkillTwo() { }
        public override void SkillThree() { }
        #endregion
    }
}

