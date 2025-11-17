using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    // Inspector fallback JSON
    public TextAsset dialogueJSON;
    public DialogueManager manager;

    private Bootstrapper boot;

    private void Start()
    {
        boot = GameObject.FindAnyObjectByType<Bootstrapper>();
        //Intercepts the dialogue meant to be loaded on start.
        if (boot.combatLeadsToDialogue)
        {
            dialogueJSON = boot.dialogueAfterBattle;
            boot.PurgeDialogues(); //Purge it.
        }
        StartCoroutine(LoadDialogueCoroutine());
    }

    private IEnumerator LoadDialogueCoroutine()
    {
        // Look for dialogue name saved by turn manager
        string dialogueToLoad = PlayerPrefs.GetString("NextDialogueToLoad", "");
        Debug.Log("DialogueTrigger: Attempting to load dialogue " + dialogueToLoad);

        Dialogue dialogueToStart = null;

        if (!string.IsNullOrEmpty(dialogueToLoad))
        {
            // Try loading dialogue from Resources
            TextAsset loadedDialogue = Resources.Load<TextAsset>($"Dialogues/{dialogueToLoad}");
            if (loadedDialogue != null)
            {
                Debug.Log("DialogueTrigger: Successfully loaded " + dialogueToLoad + " JSON from Resources/Dialogues/");
                dialogueToStart = manager.LoadDialogueFromJSON(loadedDialogue);
            }
            else
            {
                Debug.LogWarning("DialogueTrigger: " + dialogueToLoad + " not found in Resources. Trying Addressables...");

                // Try Addressables
                AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(dialogueToLoad);
                yield return handle;

                if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
                {
                    Debug.Log("DialogueTrigger: Loaded " + dialogueToLoad + " from Addressables.");
                    dialogueToStart = manager.LoadDialogueFromJSON(handle.Result);
                }
                else
                {
                    Debug.LogError("DialogueTrigger: Could not load dialogue " + dialogueToLoad + " from Addressables.");
                }
            }
        }
        else
        {
            Debug.LogWarning("DialogueTrigger: No dialogue key found in PlayerPrefs.");
        }

        // Fallback to assigned Dialogue JSON if no dialogue was specified by Turn Manager
        if (dialogueToStart == null && dialogueJSON != null)
        {
            Debug.Log("DialogueTrigger: Using fallback dialogue JSON assigned in inspector.");
            dialogueToStart = manager.LoadDialogueFromJSON(dialogueJSON);
        }

        // If nothing is still assigned, do nothing
        if (dialogueToStart == null)
        {
            Debug.LogError("DialogueTrigger: No dialogue could be loaded! Please check PlayerPrefs key and fallback assignment.");
            yield break;
        }

        // Start Dialogue
        manager.StartDialogue(dialogueToStart);

        // Delete Key to cleanup
        PlayerPrefs.DeleteKey("NextDialogueToLoad");
    }
}
