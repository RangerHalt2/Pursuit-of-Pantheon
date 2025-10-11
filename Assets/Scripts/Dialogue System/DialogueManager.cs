using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using JetBrains.Annotations;

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

    private void Start()
    {
        characterLookup = new Dictionary<string, CharacterData>();

        foreach (var character in characters)
        {
            characterLookup[character.speakerID] = character;
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

        if (characterLookup.TryGetValue(line.speakerID, out var character))
        {
            nameText.text = character.displayName;
            avatarImage.sprite = character.avatar;
        }
        else
        {
            nameText.text = line.speakerID;
            //avatarImage.sprite = defaultAvatar;
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

                int nextIndex = line.choices[i].nextLineIndex;
                choicePanels[i].onClick.AddListener(() =>
                {
                    currentLineIndex = nextIndex;
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
        Debug.Log("Continue button pressed. Typing: " + isTyping);

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

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }

    public Dialogue LoadDialogueFromJSON(TextAsset jsonFile)
    {
        return JsonUtility.FromJson<Dialogue>(jsonFile.text);
    }
}