using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowerPrefabController : MonoBehaviour
{
    [SerializeField] public Image followerSprite;
    [SerializeField] private TextMeshProUGUI followerName;

    [SerializeField] private FollowerPartyButton partyButton;

    public FollowerData followerData;
    
    //TO DO: Needs some form of functionality and probably a common index to get the ID and compare it to assigned a sprite, probably update the core class system to inherently have that information
    public void SetFollowerSprite()
    {

    }

    // RL: Added to work with Follower Party Button
    public void SetFollower(FollowerData follower)
    {
        followerName.text = follower.displayName;
        followerData = follower;

        if (partyButton != null)
        {
            partyButton.SetFollower(follower);
        }
    }

    public void SetFollowerName(string name)
    {
        followerName.text = name;
    }
}
