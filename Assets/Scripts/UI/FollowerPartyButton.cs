using UnityEngine;
using UnityEngine.UI;

public class FollowerPartyButton : MonoBehaviour
{
    private PartyManager partyManager;
    private FollowerData follower;

    [Header("Buttons")]
    public bool showButtons;
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;

    void Awake()
    {
        // Find the party manager in runtime
        partyManager = FindFirstObjectByType<PartyManager>();
        if (partyManager != null)
        {
            Debug.Log("FollowerPartyButton: PartyManager not Found!");
        }
    }

    public void SetFollower(FollowerData followerData)
    {
        // Assign the follower
        follower = followerData;
        // Refresh buttons
        RefreshButtons();
    }

    // Called when pressing the add button
    public void AddToParty()
    {
        // Attempt to add the follower to the current party
        bool success = partyManager.EquipNextAvailable(follower);
        // If adding the follower failed
        if (!success)
        {
            Debug.Log("FollowerPartyButton: Could not add follower to party.");
        }

        // Refresh the buttons
        RefreshButtons();
    }

    // Called when pressing the Remove button
    public void RemoveFromParty()
    {
        // Removes the follower from the party manager
        partyManager.UnequipFollower(follower);
        // Refresh the buttons
        RefreshButtons();
    }

    // Enable/Disable Buttons based on the follower's equipped state
    public void RefreshButtons()
    {
        // If showButtons is false, or if the follower is null
        if (!showButtons || follower == null)
        {
            // Disable both of the buttons
            addButton.gameObject.SetActive(false);
            removeButton.gameObject.SetActive(false);
        }

        // Set is Equipped to true if the follower's position is greater than 0 (They are in the active party)
        // Set is Equipped to false if the follower's position is 0, they are unequipped
        bool isEquipped = follower.position > 0;

        // The add button is enabled if the follower is unequipped, and disabled if they are
        addButton.gameObject.SetActive(!isEquipped);
        // The remove button is enabled if the follower is equipped, and disabled if they aren't
        removeButton.gameObject.SetActive(isEquipped);
    }
}
