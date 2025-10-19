// Created by Ryan Lupoli
// This interface is used by the combat manager to identify and keep track of combatants in a combat scene
using UnityEngine;

public interface ICombatant
{
    #region Variables
    // The Speed stat. Used to determine how quickly something is able to act
    float Agility { get; }
    // Tracks how close something is to being able to act
    float ActionProgress { get; set; }
    // Tracks which team the combatant is on
    int TeamID { get; }
    #endregion

    // Called when a combatant is allowed to take an action in combat
    void PerformAction();
    // Allows for quick access to the combatant game object
    GameObject GetGameObject();
}
