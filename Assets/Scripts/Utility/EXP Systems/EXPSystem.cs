using UnityEngine;

[System.Serializable]
public class EXPSystem
{
    //A “follower” tracker system that stores the exp they get from battles or events.
    //This should have a remove/add function so we can take away follower count 
    //from the player when events occur or they have a combatant die.

    public string name;
    public int experience;

    public EXPSystem(string name, int startingExp = 0)
    {
        this.name = name;
        this.experience = startingExp;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
    }

    public void RemoveExperience(int amount)
    {
        experience = Mathf.Max(0, experience - amount);
    }
}

//Will work with the follower system (FollowerManager.cs) in order to track how many
//followers there are and report the experience
