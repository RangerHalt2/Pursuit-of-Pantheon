using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class FollowerUIHandler : MonoBehaviour
{
    [SerializeField] private PartyManager partyManager;

    // UI elements for promotion
    [SerializeField] private Button promoteButton;
    [SerializeField] private Button promotionOption1Button;
    [SerializeField] private Button promotionOption2Button;
    [SerializeField] private GameObject promotionOptionsPanel;

    [SerializeField] private Transform equippedFollowersPanel;
    [SerializeField] private Transform unequippedFollowersPanel;
    [SerializeField] private Button followerButtonPrefab;
    [SerializeField] private Button deselectButton;

    [SerializeField] private GameObject promotionSoundEffect;

    // Maps followerID to the spawned GameObject
    private Dictionary<string, GameObject> equippedFollowerObjects = new Dictionary<string, GameObject>();

    // The currently selected equipped follower's data and class
    private FollowerData selectedFollowerData;
    private FollowerClass selectedFollowerClassData;

    // Call this after spawning equipped followers in the scene
    public void RegisterEquippedFollowerObject(FollowerData data, GameObject go)
    {
        if (data != null && go != null)
            equippedFollowerObjects[data.followerID] = go;
    }

    // Call this from a UI button, passing the index of the equipped follower to select
    public void SelectEquippedFollowerByIndex(int equippedIndex)
    {
        if (equippedIndex < 0 || equippedIndex >= partyManager.equippedFollowers.Count)
        {
            Debug.LogWarning("Invalid equipped follower index.");
            return;
        }

        selectedFollowerData = partyManager.equippedFollowers[equippedIndex];
        // Get the FollowerClass ScriptableObject for the selected follower's current class
        selectedFollowerClassData = FollowerFactory.GetClassByID(selectedFollowerData.classID);

        // Show promote button only if there are promotion options
        if (selectedFollowerClassData != null && !string.IsNullOrEmpty(selectedFollowerClassData.unityClassName))
        {
            var type = Type.GetType(selectedFollowerClassData.unityClassName);
            Debug.Log($"Type: {type}");
            if (type != null && typeof(BaseClass).IsAssignableFrom(type))
            {
                var promotionOptionsField = type.GetField("promotionOptions");
                Type[] promotionOptions = null;
                if (promotionOptionsField != null && promotionOptionsField.IsStatic)
                    promotionOptions = promotionOptionsField.GetValue(null) as Type[];
                Debug.Log($"PromotionOptions found: {promotionOptions?.Length}");
                if (promotionOptions != null && promotionOptions.Length > 0)
                {
                    promoteButton.gameObject.SetActive(true);
                    promotionOptionsPanel.SetActive(false);
                }
                else
                {
                    promoteButton.gameObject.SetActive(false);
                    promotionOptionsPanel.SetActive(false);
                }
            }
            else
            {
                promoteButton.gameObject.SetActive(false);
                promotionOptionsPanel.SetActive(false);
            }
        }
        else
        {
            promoteButton.gameObject.SetActive(false);
            promotionOptionsPanel.SetActive(false);
        }

        deselectButton.gameObject.SetActive(true);
    }

    // Call this from the "Promote Follower" button
    public void OnPromoteButtonClicked()
    {
        Debug.Log("Promote button clicked");
        if (selectedFollowerClassData == null) return;

        var type = Type.GetType(selectedFollowerClassData.unityClassName);
        if (type == null || !typeof(BaseClass).IsAssignableFrom(type)) return;

        var promotionOptionsField = type.GetField("promotionOptions");
        Type[] promotionOptions = null;
        if (promotionOptionsField != null && promotionOptionsField.IsStatic)
            promotionOptions = promotionOptionsField.GetValue(null) as Type[];

        if (promotionOptions == null || promotionOptions.Length == 0) return;

        promotionOptionsPanel.SetActive(true);

        for (int i = 0; i < promotionOptions.Length; i++)
        {
            int optionIndex = i; // capture the current value of i
            string className = AddSpacesToClassName(promotionOptions[i].Name);
            if (i == 0)
            {
                promotionOption1Button.gameObject.SetActive(true);
                promotionOption1Button.GetComponentInChildren<TextMeshProUGUI>().text = $"Promote to {className}";
                promotionOption1Button.onClick.RemoveAllListeners();
                promotionOption1Button.onClick.AddListener(() => PromoteTo(optionIndex));
            }
            else if (i == 1)
            {
                promotionOption2Button.gameObject.SetActive(true);
                promotionOption2Button.GetComponentInChildren<TextMeshProUGUI>().text = $"Promote to {className}";
                promotionOption2Button.onClick.RemoveAllListeners();
                promotionOption2Button.onClick.AddListener(() => PromoteTo(optionIndex));
            }
        }
        if (promotionOptions.Length < 2)
            promotionOption2Button.gameObject.SetActive(false);
    }

    private void PromoteTo(int optionIndex)
    {
        Debug.Log($"PromoteTo called with optionIndex: {optionIndex}");
        if (selectedFollowerClassData == null) return;

        var type = Type.GetType(selectedFollowerClassData.unityClassName);
        if (type == null || !typeof(BaseClass).IsAssignableFrom(type)) return;

        var promotionOptionsField = type.GetField("promotionOptions");
        Type[] promotionOptions = null;
        if (promotionOptionsField != null && promotionOptionsField.IsStatic)
            promotionOptions = promotionOptionsField.GetValue(null) as Type[];

        if (promotionOptions == null || optionIndex < 0 || optionIndex >= promotionOptions.Length) return;

        // Get the new class type and its FollowerClass ScriptableObject
        var newClassType = promotionOptions[optionIndex];
        var allClasses = Resources.LoadAll<FollowerClass>("ClassStats");
        var newClassSO = allClasses.FirstOrDefault(fc => fc.unityClassName == newClassType.FullName);

        if (newClassSO == null)
        {
            Debug.LogError($"No FollowerClass found for type {newClassType.FullName}");
            return;
        }

        // Update the selected follower's data
        selectedFollowerData.classID = newClassSO.classID;
        selectedFollowerData.maxHP = newClassSO.baseMaxHP;
        selectedFollowerData.power = newClassSO.basePower;
        selectedFollowerData.magick = newClassSO.baseMagick;
        selectedFollowerData.resilience = newClassSO.baseResilience;
        selectedFollowerData.faith = newClassSO.baseFaith;
        selectedFollowerData.agility = newClassSO.baseAgility;
        // ... update other stats as needed

        // Optionally, refresh UI
        promoteButton.gameObject.SetActive(false);
        promotionOptionsPanel.SetActive(false);
        deselectButton.gameObject.SetActive(false);

        if(promotionSoundEffect != null)
        {
            Instantiate(promotionSoundEffect);
        }

        RefreshFollowerPanels();
    }

    // Existing equip/unequip methods remain unchanged
    public void EquipFollowerByIndex(int unequippedIndex)
    {
        if (unequippedIndex < 0 || unequippedIndex >= partyManager.unequippedFollowers.Count)
        {
            Debug.LogWarning("Invalid unequipped follower index.");
            return;
        }

        var follower = partyManager.unequippedFollowers[unequippedIndex];
        bool equipped = partyManager.EquipNextAvailable(follower);

        if (equipped)
        {
            Debug.Log($"Equipped follower: {follower.displayName}");
            RefreshFollowerPanels();
        }
        else
        {
            Debug.LogWarning("Could not equip follower (party may be full).");
        }
    }

    // Call this from a UI button, passing the index of the equipped follower to unequip
    public void UnequipFollowerByIndex(int equippedIndex)
    {
        if (equippedIndex < 0 || equippedIndex >= partyManager.equippedFollowers.Count)
        {
            Debug.LogWarning("Invalid equipped follower index.");
            return;
        }

        var follower = partyManager.equippedFollowers[equippedIndex];
        partyManager.UnequipFollower(follower);

        // Destroy the GameObject if it exists
        if (equippedFollowerObjects.TryGetValue(follower.followerID, out GameObject go) && go != null)
        {
            Destroy(go);
            equippedFollowerObjects.Remove(follower.followerID);
        }

        Debug.Log($"Unequipped follower: {follower.displayName}");
        RefreshFollowerPanels();
    }
    public void RefreshFollowerPanels()
    {
        // Clear old buttons
        foreach (Transform child in equippedFollowersPanel) Destroy(child.gameObject);
        foreach (Transform child in unequippedFollowersPanel) Destroy(child.gameObject);

        // Equipped followers
        for (int i = 0; i < partyManager.equippedFollowers.Count; i++)
        {
            var follower = partyManager.equippedFollowers[i];
            var button = Instantiate(followerButtonPrefab, equippedFollowersPanel);
            var textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent == null)
                Debug.LogError("Button prefab is missing a TextMeshProUGUI component in its children!");
            else
            {
                var followerClass = FollowerFactory.GetClassByID(follower.classID);
                string classDisplay = followerClass != null ? AddSpacesToClassName(GetClassNameFromUnityClassName(followerClass.unityClassName)) : "Unknown";
                textComponent.text = $"{follower.displayName} ({classDisplay})";
            }
            int index = i;
            button.onClick.AddListener(() => SelectEquippedFollowerByIndex(index));
        }

        // Unequipped followers
        for (int i = 0; i < partyManager.unequippedFollowers.Count; i++)
        {
            var follower = partyManager.unequippedFollowers[i];
            var button = Instantiate(followerButtonPrefab, unequippedFollowersPanel);
            var textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent == null)
                Debug.LogError("Button prefab is missing a TextMeshProUGUI component in its children!");
            else
                textComponent.text = follower.displayName;
            int index = i;
            button.onClick.AddListener(() => EquipFollowerByIndex(index));
        }
    }
    public void DeselectFollower()
    {
        promoteButton.gameObject.SetActive(false);
        promotionOptionsPanel.SetActive(false);
        deselectButton.gameObject.SetActive(false);
        // Optionally, visually un-highlight all follower buttons here
    }

    private string AddSpacesToClassName(string className)
    {
        if (string.IsNullOrEmpty(className)) return className;
        return System.Text.RegularExpressions.Regex.Replace(className, "(\\B[A-Z])", " $1");
    }

    // Helper to get the class name from the full unityClassName (e.g., "PursuitOfPantheon.Classes.ArcaneArcher" -> "ArcaneArcher")
    private string GetClassNameFromUnityClassName(string unityClassName)
    {
        if (string.IsNullOrEmpty(unityClassName)) return "";
        int lastDot = unityClassName.LastIndexOf('.');
        return lastDot >= 0 ? unityClassName.Substring(lastDot + 1) : unityClassName;
    }
}
