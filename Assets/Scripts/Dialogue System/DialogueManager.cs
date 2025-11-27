using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;

    public Image avatarImage;
    public CharacterData[] characters;
    private Dictionary<string, CharacterData> characterLookup;

    public Button[] choicePanels;
    public TextMeshProUGUI[] choiceTexts;

    private Dialogue currentDialogue;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    public FollowerManager followerManager;
    public Health health;
    private Dictionary<string, Action<string>> effects;
    public string effectParameter;

    private void Awake()
    {
        characterLookup = new Dictionary<string, CharacterData>();

        foreach (var character in characters)
        {
            characterLookup[character.speakerID] = character;
        }

        HandleChoiceAdjustments();

        if (effects == null)
        {
            effects = new Dictionary<string, Action<string>>(); //safeguard
        }
    }

    public void TestButton()
    {
        Debug.Log("Test button clicked");
    }

    public void StartDialogue (Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        DisplayLine();
    }

    public void DisplayLine ()
    {
        if (currentLineIndex >= currentDialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentDialogue.lines[currentLineIndex];
        nameText.text = line.speakerName;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSentence(line.sentence));

        foreach (var panel in choicePanels)
        {
            panel.gameObject.SetActive(false);
            panel.onClick.RemoveAllListeners();
        }

        bool hasChoices = line.choices != null && line.choices.Length > 0;
        continueButton.gameObject.SetActive(!hasChoices);

        if (hasChoices)
        {
            StartCoroutine(ShowChoicesDelayed(line));
        }

        if (!string.IsNullOrEmpty(line.speakerID) && characterLookup.TryGetValue(line.speakerID, out var character))
        {
            nameText.text = character.displayName;
            avatarImage.sprite = character.avatar;
        }
        else
        {
            nameText.text = line.speakerName ?? "Unknown";
            // avatarImage.sprite = defaultAvatar;
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f); //Speed of typing
        }
        isTyping = false;
    }

    IEnumerator ShowChoicesDelayed(DialogueLine line)
    {
        while (isTyping)
        {
            yield return null;
        }

        for (int i = 0; i < choicePanels.Length; i++)
        {
            if (i < line.choices.Length)
            {
                choicePanels[i].gameObject.SetActive(true);
                choiceTexts[i].text = line.choices[i].choiceText;

                choicePanels[i].onClick.RemoveAllListeners();


                DialogueChoice currentChoice = line.choices[i];
                int nextIndex = line.choices[i].nextLineIndex;

                choicePanels[i].onClick.AddListener(() =>
                {
                    OnChoicesSelected(currentChoice);
                    //currentLineIndex = nextIndex;
                    DisplayLine();
                });
            }

            else
            {
                choicePanels[i].gameObject.SetActive(false);
            }
        }
    }

    public void DisplayNextSentence()
    {
        //Debug.Log("Continue button pressed. Typing: " + isTyping);

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue.lines[currentLineIndex].sentence;
            isTyping = false;
            return;
        }

        DialogueLine line = currentDialogue.lines[currentLineIndex];

        if (line.choices == null || line.choices.Length == 0)
        {
            if (line.nextLineIndex != -1)
            {
                currentLineIndex = line.nextLineIndex;
                DisplayLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void HandleChoiceAdjustments()
    {
        effects = new Dictionary<string, Action<string>>
        {
            { "DamageParty", (param) =>
                {
                    if (float.TryParse(param, out float dmg))
                        health.TakeDamage(dmg);
                    else
                        health.TakeDamage(5f);
                }
            },

            { "IncreaseFollowers", (param) =>
                {
                    if (int.TryParse(param, out int amount))
                        followerManager.AddFollower(amount);
                }
            },

            { "DecreaseFollowers", (param) =>
                {
                    if (int.TryParse (param, out int amount))
                        followerManager.RemoveFollower(amount);
                }
            },

            { "GainItem", (param) =>
                {
                    string[] args = param.Split(',');

                    ItemHandler.ItemType itemType = (ItemHandler.ItemType)System.Enum.Parse(typeof(ItemHandler.ItemType), args[0]);

                    int amount = 1;
                    if (args.Length > 1)
                    {
                        int.TryParse(args[1], out amount);
                    }

                    ItemHandler.Instance.AddItem(itemType, amount);
                }
            },

            { "AddHealingDebuff", (param) =>
                {
                    //debuffManager.AddDebuff(param);
                }
            },

            { "SkipCombat", (param) =>
                {
                    if (CombatManager.instance != null)
                    {
                        CombatManager.instance.SkipCombat();
                    }
                }
            },

            { "RandomOutcome", (param) =>
                {
                    bool success = UnityEngine.Random.value > 0.5f;
                    GameFlags.Set("RandomOutcome", success);
                    Debug.Log("Random outcome: " + success);
                } 
            },

            { "SkillCheck", (param) =>
                {
                    string[] parts = param.Split(',');
                    string skillName = parts[0];
                    int difficulty = int.Parse(parts[1]);

                    int skillValue = followerManager.GetHighestSkill(skillName);

                    bool success = skillValue >= difficulty;
                    GameFlags.Set("LastSkillCheck", success);
                }
            }
        };

        //how to implement within JSON
        //{
        //"choiceText": "Be mean",
        //"nextLineIndex: 12,
        //"effectID": "DamageParty"
    }

    void OnChoicesSelected(DialogueChoice choice)
    {
        //trigger an effect
        if (!string.IsNullOrEmpty(choice.effectID) && effects.TryGetValue(choice.effectID, out var effect))
        {
            effect.Invoke(choice.effectParameter);
        }

        if (choice.nextLineIndex >= 0)
        {
            currentLineIndex = choice.nextLineIndex;
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        string leadsTo = currentDialogue.lines[currentLineIndex].leadsTo;

        //LB: Adding a feature that adds a follower at the end of the dialogue if it's meant to
        bool addFollower = currentDialogue.lines[currentLineIndex].gainFollowers;
        if (addFollower)
        {
            RecruitmentManager rm = GameObject.FindAnyObjectByType<RecruitmentManager>(); //This will always be loaded
            rm.RecruitNewFollower();
            ScoreManager.Instance.recruitScore += 50;
        }

        HandleDialogueOutcome(leadsTo);
        return;
    }

    public async void HandleDialogueOutcome(string leadsToName)
    {
        if (string.IsNullOrEmpty(leadsToName))
        {
            Debug.Log("No leadsTo field, ending dialogue.");
            return;
        }

        // Load the asset asynchronously by its Address
        AsyncOperationHandle<DialogueOutcome> handle =
            Addressables.LoadAssetAsync<DialogueOutcome>(leadsToName);

        DialogueOutcome outcome = await handle.Task;

        if (outcome == null)
        {
            Debug.LogWarning($"Could not find DialogueOutcome with name {leadsToName}");
            return;
        }

        outcome.Execute();
    }

    public Dialogue LoadDialogueFromJSON(TextAsset jsonFile)
    {
        return JsonUtility.FromJson<Dialogue>(jsonFile.text);
    }
}