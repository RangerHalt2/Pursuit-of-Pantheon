// Created By: Ryan Lupoli
// A basic interface designed to help create Divine Skills
using System;
using System.Collections;
using UnityEngine;

public interface IDivineSkill
{
    #region Variables
    // The name of the divine Skill
    String skillName { get; }
    // The apCost of the given skill
    int apCost { get; }
    // The amount of time (in seconds) a skill will be on coolown
    float cooldown { get; }
    #endregion

    #region Methods
    // Performs the function of the given skill
    public void UseSkill();
    #endregion
}
