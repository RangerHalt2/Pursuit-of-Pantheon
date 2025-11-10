using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowerPrefabController : MonoBehaviour
{
    [SerializeField] private Image followerSprite;
    [SerializeField] private TextMeshProUGUI followerName;

    
    //TO DO: Needs some form of functionality and probably a common index to get the ID and compare it to assigned a sprite, probably update the core class system to inherently have that information
    public void SetFollowerSprite()
    {

    }

    public void SetFollowerName(string name)
    {
        followerName.text = name;
    }
}
