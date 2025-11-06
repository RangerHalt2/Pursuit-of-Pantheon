using UnityEngine;

[System.Serializable]
public class EXPSystem
{
    //A “follower” tracker system that stores the exp they get from battles or events.
    //This should have a remove/add function so we can take away follower count 
    //from the player when events occur or they have a combatant die.

    public int amount;
    public readonly PlayerProfile profile;

    public EXPSystem(PlayerProfile profile, int startingAmount = 0)
    {
        this.profile = profile;
        amount = startingAmount;
        profile.level = amount;
    }

    public void SyncLevel()
    {
        profile.UpdateLevel(amount);
    }
}

//Will work with the follower system (FollowerManager.cs) in order to track how many
//followers there are and report the experience
