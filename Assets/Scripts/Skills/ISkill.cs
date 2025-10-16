// Created By: Ryan Lupoli
// A basic interface used to design new skills
using System;
using UnityEngine;

// The types of Skills which can exist
public enum SkillType
{
    // Skills which deal damage
    Offensive,
    // Skills which restore health
    Healing,
    // Skills which provide some form of positive effect
    Buff,
    // Skilsl which provide some form of negative effect
    Debuff
}
    
public interface ISkill
{
    #region Variables
    // The name of the skill
    String skillName { get; }

    // Defines the type of the given skill
    SkillType type { get; }

    // The accuracy of the skill
    [Range (0, 100)] int accuracy { get;  }
    #endregion

    #region Methods
    // Uses a skill on a specified target
    public void UseSkill(GameObject target);

    // Checks the effect of a given skill
    public float CheckSkill(GameObject target);
    #endregion

}
